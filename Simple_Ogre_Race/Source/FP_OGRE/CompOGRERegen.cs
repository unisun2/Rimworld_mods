using System;
using System.Linq;
using Verse;

namespace FP_OGRE;

public class CompOGRERegen : ThingComp
{
    public int healatonce = 3;
    public int tickCounter;

    public CompProperties_OGRE_Regen Props => (CompProperties_OGRE_Regen)props;

    protected int rateInTicks => Props.rateInTicks;

    public override void CompTick()
    {
        tickCounter++;
        if (tickCounter < rateInTicks)
        {
            return;
        }

        var pawn = parent as Pawn;
        if (pawn?.health != null)
        {
            var firstHediffOfDef = pawn.health.hediffSet.GetFirstHediffOfDef(RimWorld.HediffDefOf.FoodPoisoning);
            if (firstHediffOfDef != null)
            {
                firstHediffOfDef.Severity = Math.Max(0f, firstHediffOfDef.Severity - 1f);
            }

            if (pawn.health.hediffSet.GetInjuriesTendable() != null &&
                pawn.health.hediffSet.GetInjuriesTendable().Any())
            {
                for (var i = 0; i < pawn.health.hediffSet.hediffs.Count; i++)
                {
                    if (pawn.health.hediffSet.hediffs[i] is not Hediff_Injury hediff_Injury)
                    {
                        continue;
                    }

                    hediff_Injury.Severity = Math.Max(0f, hediff_Injury.Severity - 1f);
                    healatonce--;
                    if (healatonce <= 0)
                    {
                        break;
                    }
                }

                healatonce = 4;
            }
            else
            {
                var hediff_Injury2 = FindPermanentInjury(pawn);
                if (hediff_Injury2 != null)
                {
                    hediff_Injury2.Severity /= 1.5f;
                    if (hediff_Injury2.Severity < 0.1)
                    {
                        hediff_Injury2.Severity = 0f;
                    }
                }
            }
        }

        tickCounter = 0;
    }

    private Hediff_Injury FindPermanentInjury(Pawn pawn)
    {
        Hediff_Injury hediff_Injury = null;
        var hediffs = pawn.health.hediffSet.hediffs;
        for (var i = 0; i < hediffs.Count; i++)
        {
            if (hediffs[i] is Hediff_Injury { Visible: true } hediff_Injury2 && hediff_Injury2.IsPermanent() &&
                hediff_Injury2.def.everCurableByItem && hediff_Injury2.def.isBad &&
                (hediff_Injury == null || hediff_Injury2.Severity > hediff_Injury.Severity))
            {
                hediff_Injury = hediff_Injury2;
            }
        }

        return hediff_Injury;
    }
}
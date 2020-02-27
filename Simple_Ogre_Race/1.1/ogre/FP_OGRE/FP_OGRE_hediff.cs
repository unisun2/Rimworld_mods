using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using HarmonyLib;
using Verse;
using RimWorld;
using System.Collections;
using System.Linq;

namespace FP_OGRE
{
    public class CompProperties_OGRE_Regen : CompProperties
    {
        public int rateInTicks = 800;

        public CompProperties_OGRE_Regen()
        {
            this.compClass = typeof(CompOGRERegen);
        }
    }

    public class CompOGRERegen : ThingComp
    {
        public int tickCounter = 0;
        public int healatonce = 3;

        public CompProperties_OGRE_Regen Props
        {
            get
            {
                return (CompProperties_OGRE_Regen)this.props;
            }
        }

        protected int rateInTicks
        {
            get
            {
                return this.Props.rateInTicks;
            }
        }


        public override void CompTick()
        {
            tickCounter++;


            if (tickCounter >= rateInTicks)
            {
                Pawn pawn = this.parent as Pawn;

                if (pawn.health != null)
                {
                    //Hediff fp = HediffDefOf.FoodPoisoning;
                    //if(pawn.health.hediffSet.hediffs.Contains(HediffDefOf.FoodPoisoning)

                    Hediff fp = pawn.health.hediffSet.GetFirstHediffOfDef(RimWorld.HediffDefOf.FoodPoisoning);
                    if (fp != null)
                        fp.Severity = Math.Max(0f, fp.Severity - 1);

                    if (pawn.health.hediffSet.GetInjuriesTendable() != null && pawn.health.hediffSet.GetInjuriesTendable().Count<Hediff_Injury>() > 0)
                    {
                        /*foreach (Hediff_Injury injury in pawn.health.hediffSet.GetInjuriesTendable())
                        {
                            injury.Severity = Math.Max(0f, injury.Severity - 1);
                            healatonce--;
                            if (healatonce <= 0) break;
                        }
                        healatonce = 3;
						*/

						for (int i = 0; i < pawn.health.hediffSet.hediffs.Count; i++)
						{
                            Hediff_Injury inj = pawn.health.hediffSet.hediffs[i] as Hediff_Injury;

                            if (inj == null) continue;
                            //if (inj.Severity < 1) continue;

                            inj.Severity = Math.Max(0f, inj.Severity - 1);
                            healatonce--;
                            if (healatonce <= 0) break;
						}
						healatonce = 4;
                    }
                    else
                    {
                        Hediff_Injury hediff_Injury = this.FindPermanentInjury(pawn);
                        if (hediff_Injury != null)
                        {
                            hediff_Injury.Severity = (float)(hediff_Injury.Severity / 1.5f);
                            if (hediff_Injury.Severity < 0.1) hediff_Injury.Severity = 0;
                        }
                    }

                }
                tickCounter = 0;
            }
        }



        private Hediff_Injury FindPermanentInjury(Pawn pawn)
        {
            Hediff_Injury hediff_Injury = null;
            List<Hediff> hediffs = pawn.health.hediffSet.hediffs;
            for (int i = 0; i < hediffs.Count; i++)
            {
                Hediff_Injury hediff_Injury2 = hediffs[i] as Hediff_Injury;
                if (hediff_Injury2 != null && hediff_Injury2.Visible && hediff_Injury2.IsPermanent() && hediff_Injury2.def.everCurableByItem && hediff_Injury2.def.isBad)
                {
                    if (hediff_Injury == null || hediff_Injury2.Severity > hediff_Injury.Severity)
                    {
                        hediff_Injury = hediff_Injury2;
                    }
                }
            }
            return hediff_Injury;
        }

    }

}

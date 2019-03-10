using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace PLAHF
{
    public class ThingDef_experimentBullet : ThingDef
    {
        //public HediffDef HediffToAdd = HediffDefOf.PLAHF_blindnesspoison;   // default
    }


    public class Projectile_experimentBullet : Bullet
    {
    
        #region Overrides
        protected override void Impact(Thing hitThing)
        {
            base.Impact(hitThing);

            if (hitThing != null && hitThing is Pawn hitPawn )
            {
                if (0.1 > hitPawn.GetStatValue(StatDefOf.ToxicSensitivity, true))   // no sensitivity? no plague.
                {
                    return;
                }
                int RRR = Rand.Range(0, 8);

                HediffDef HediffToAdd;

                switch (RRR)
                {
                    case 0:
                        HediffToAdd = HediffDefOf.PLAHF_blindingpoison; break;
                    case 1:
                        HediffToAdd = HediffDefOf.PLAHF_sleepingpoison; break;
                    case 2:
                        HediffToAdd = HediffDefOf.PLAHF_chokingpoison; break;
                    case 3:
                        HediffToAdd = RimWorld.HediffDefOf.ToxicBuildup; break;
                    case 4:
                        HediffToAdd = RimWorld.HediffDefOf.Hangover; break;
                    case 5:
                        HediffToAdd = RimWorld.HediffDefOf.Malaria; break;
                    case 6:
                        HediffToAdd = RimWorld.HediffDefOf.Flu; break;
                    case 7:
                        HediffToAdd = RimWorld.HediffDefOf.AlcoholHigh; break;
                    case 8:
                        HediffToAdd = RimWorld.HediffDefOf.Plague; break;
                    default:
                        HediffToAdd = RimWorld.HediffDefOf.AlcoholHigh; break;
                }


                Hediff plagueOnPawn = hitPawn?.health?.hediffSet?.GetFirstHediffOfDef(HediffToAdd);
                var randomSeverity = Rand.Range(0.05f, 0.3f);
                if (plagueOnPawn != null)
                {
                    plagueOnPawn.Severity += randomSeverity;
                }
                else
                {
                    Hediff hediff = HediffMaker.MakeHediff(HediffToAdd, hitPawn, null);
                    hediff.Severity = randomSeverity;
                    hitPawn.health.AddHediff(hediff, null, null);
                }
            }
        }
        #endregion Overrides
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace PFPM_Poweredpanel
{
    class PFPM_Poweredpanel_building : Building
    {
        public CompPowerTrader powerComp;

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            this.powerComp = base.GetComp<CompPowerTrader>();
        }

        public override void TickRare()
        {
            base.TickRare();

            if (powerComp.PowerOn)
            {
                foreach (Thing thing in this.Map.listerThings.AllThings.FindAll((Thing x) => x is Pawn pawn))
                {
                    if (this.Position.InHorDistOf(thing.Position, 3f))
                    {
                        Hediff hediff = HediffMaker.MakeHediff(HediffDefOf.PFPM_faster, (Pawn)thing, null);
                        hediff.Severity = 0.1f;
                        ((Pawn)thing).health.AddHediff(hediff, null, null, null);
                    }

                }
            }
           
        }
    }

    class PFPM_Poweredpanelftl_building : Building
    {
        public CompPowerTrader powerComp;

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            this.powerComp = base.GetComp<CompPowerTrader>();
        }

        public override void TickRare()
        {
            base.TickRare();

            if (powerComp.PowerOn)
            {
                foreach (Thing thing in this.Map.listerThings.AllThings.FindAll((Thing x) => x is Pawn pawn))
                {
                    if (this.Position.InHorDistOf(thing.Position, 3f))
                    {
                        Hediff hediff = HediffMaker.MakeHediff(HediffDefOf.PFPM_fasterthanlight, (Pawn)thing, null);
                        hediff.Severity = 0.1f;
                        ((Pawn)thing).health.AddHediff(hediff, null, null, null);
                    }

                }
            }

        }
    }
}
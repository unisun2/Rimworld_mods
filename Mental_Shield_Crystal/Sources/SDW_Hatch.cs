using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace CSMentalShield
{
    class CSMentalShield_building : Building
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
                    if (thing.def.race.Humanlike)
                    {
                        if (this.Position.InHorDistOf(thing.Position, 20f) && thing.Faction == Faction.OfPlayer)
                        {
                            Hediff hediff = HediffMaker.MakeHediff(HediffDefOf.Psychic_silence, (Pawn)thing, null);
                            hediff.Severity = 0.1f;
                            ((Pawn)thing).health.AddHediff(hediff, null, null, null);
                        }
                    }
                    
                }
            }
           
        }
    }





}
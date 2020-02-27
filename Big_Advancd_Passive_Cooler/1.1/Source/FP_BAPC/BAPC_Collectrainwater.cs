using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using Verse;

namespace BAPC
{
    class BAPC_Collectrainwater : ThingComp
    {

        protected CompRefuelable refuelableComp;

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            this.refuelableComp = this.parent.GetComp<CompRefuelable>();
        }

        private CompProperties_BAPCCR Props
        {
            get
            {
                return (CompProperties_BAPCCR)this.props;
            }
        }

        public override void CompTick()
        {
            base.CompTick();

            if (this.parent.Spawned && this.parent.Map.weatherManager.RainRate > 0.4f && !this.parent.Map.roofGrid.Roofed(this.parent.Position))
            {
                refuelableComp.Refuel(0.03f);

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace Humanpowergeneratormod
{

    public class HPGMP : CompPowerTrader
    {
        //protected CompMannable mannableComp;
        protected HPGMcyclecomp hpgmcyclecomp;

        protected CompBreakdownable breakdownableComp;

        protected virtual float DesiredPowerOutput
        {
            get
            {
                return -base.Props.basePowerConsumption;
            }
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            //this.mannableComp = this.parent.GetComp<CompMannable>();
            this.breakdownableComp = this.parent.GetComp<CompBreakdownable>();
            this.hpgmcyclecomp = this.parent.GetComp<HPGMcyclecomp>();
            if (base.Props.basePowerConsumption < 0f && !this.parent.IsBrokenDown() && FlickUtility.WantsToBeOn(this.parent))
            {
                base.PowerOn = true;
            }
        }

        public override void CompTick()
        {
            base.CompTick();
            this.UpdateDesiredPowerOutput();
        }

        public void UpdateDesiredPowerOutput()
        {
            if ((this.breakdownableComp != null && this.breakdownableComp.BrokenDown) || !hpgmcyclecomp.IsOn)
            {
                //(this.mannableComp != null && !this.mannableComp.MannedNow)
                base.PowerOutput = 0f;
            }
            else
            {

                base.PowerOutput = this.DesiredPowerOutput * hpgmcyclecomp.statValue;
            }
        }
    }
}

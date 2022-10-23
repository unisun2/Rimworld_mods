using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace Humanpowergeneratormod
{

    public class HPGMP : CompPowerPlant
    {
        //protected CompMannable mannableComp;
        protected HPGMcyclecomp hpgmcyclecomp;

        protected CompBreakdownable breakdownableComp55;

        protected override float DesiredPowerOutput
        {
            get
            {
                //Verse.Log.Message("isrunning : " + hpgmcyclecomp.IsRunning + " ..." + hpgmcyclecomp.statValue);
                return 500 * (hpgmcyclecomp.IsRunning / 100f) * (hpgmcyclecomp.statValue);
            }
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            //this.mannableComp = this.parent.GetComp<CompMannable>();
            this.breakdownableComp55 = this.parent.GetComp<CompBreakdownable>();
            this.hpgmcyclecomp = this.parent.GetComp<HPGMcyclecomp>();
            if (!this.parent.IsBrokenDown() && FlickUtility.WantsToBeOn(this.parent))
            {
                base.PowerOn = true;
            }
        }

        public override void CompTick()
        {
            base.CompTick();
            this.UpdateDesiredPowerOutput();
        }
    }
}

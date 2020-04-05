using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace BoulderTrapMod
{
    public class CompProperties_BTM : CompProperties
    {
        public CompProperties_BTM()
        {
            this.compClass = typeof(BTM_comp);
        }
    }

    public class BTM_comp : ThingComp
    {
        public CompProperties_BTM Props
        {
            get
            {
                return (CompProperties_BTM)this.props;
            }
        }




        protected CompRefuelable refuelableComp;


        public override void Initialize(CompProperties props)
        {
            base.Initialize(props);
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            this.refuelableComp = this.parent.GetComp<CompRefuelable>();
        }

        public override void CompTick()
        {
            base.CompTick();

            if (this.parent.Spawned && !(this.refuelableComp.IsFull))
            {
                this.parent.Destroy(DestroyMode.Vanish);

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;

namespace BombInWall
{
    class CompProperties_FPBIW : CompProperties
    {
        public CompProperties_FPBIW()
        {
            this.compClass = typeof(FPBIW_Comp);
        }
    }

    class FPBIW_Comp : ThingComp
    {
        public CompPowerTrader comppower;
        public CompExplosive compexplosive;

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            this.compexplosive = this.parent.GetComp<CompExplosive>();
            this.comppower = this.parent.GetComp<CompPowerTrader>();
        }

        private CompProperties_FPBIW Props
        {
            get
            {
                return (CompProperties_FPBIW)this.props;
            }
        }

        [DebuggerHidden]
        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            foreach (Gizmo c in base.CompGetGizmosExtra())
            {
                yield return c;
            }
            if (this.comppower.PowerOn && this.parent.Faction == Faction.OfPlayer)
            {
                yield return new Command_Action
                {
                    action = delegate
                    {
                        this.compexplosive.StartWick();
                    },
                    defaultDesc = "CommandFPBIWSelfDestruct".Translate(),
                    icon = ContentFinder<Texture2D>.Get("UI/Commands/command_SDW_selfdestruct2", true),
                    defaultLabel = "CommandFPBIWSelfDestructLabel".Translate()
                };
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;
using System.Diagnostics;
using UnityEngine;
using Verse.Sound;

namespace BombInWall
{
    class hiddenwall
    {
    }
    public class CompProperties_BIWHidingWall : CompProperties
    {

        public CompProperties_BIWHidingWall()
        {
            this.compClass = typeof(BIWHidingWall_Comp);
        }
    }
    public class BIWHidingWall_Comp : ThingComp
    {
        public CompPowerTrader comppower;

        public CompProperties_BIWHidingWall Props
        {
            get
            {
                return (CompProperties_BIWHidingWall)this.props;
            }
        }

        public override void Initialize(CompProperties props)
        {
            base.Initialize(props);
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            this.comppower = this.parent.GetComp<CompPowerTrader>();


        }

        [DebuggerHidden]
        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            foreach (Gizmo c in base.CompGetGizmosExtra())
            {
                yield return c;
            }
            if (this.parent.Faction == Faction.OfPlayer && this.comppower.PowerOn)
            {
                yield return new Command_Action
                {
                    defaultLabel = "burrow".Translate(),
                    defaultDesc = "burrow wall".Translate(),
                    icon = ContentFinder<Texture2D>.Get("UI/Commands/SDWburrow", true),
                    iconAngle = 0,
                    iconOffset = Vector2.zero,
                    iconDrawScale = GenUI.IconDrawScale(parent.def),
                    action = delegate
                    {
                        this.burrowWall();
                    }
                };
            }
        }

        private void burrowWall()
        {
            SoundDefOf.DropPod_Open.PlayOneShot(new TargetInfo(parent.Position, parent.Map, false));
            Map map = parent.Map;
            IntVec3 loc = parent.Position;
            float HPp = (float)parent.HitPoints / (float)parent.MaxHitPoints;
            ThingDef thatstuff = this.parent.Stuff;

            Thing thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named("SDW_Hatch"), thatstuff), loc, map, WipeMode.Vanish);
            thing.SetFaction(Faction.OfPlayer, null);
            thing.HitPoints = (int)Math.Ceiling(thing.MaxHitPoints * HPp);


        }

    }
}

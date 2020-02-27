using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace BombInWall
{

    class SDW_Hatch : Building
    {
        public CompPowerTrader powerComp;


        public bool CanUnburrowNow
        {
            get
            {
                return (!base.Spawned || !base.Map.gameConditionManager.ConditionIsActive(GameConditionDefOf.SolarFlare)) && this.powerComp.PowerOn;
            }
        }


        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            this.powerComp = base.GetComp<CompPowerTrader>();

        }

        [DebuggerHidden]
        public override IEnumerable<Gizmo> GetGizmos()
        {
            foreach (Gizmo c in base.GetGizmos())
            {
                yield return c;
            }
            if (this.CanUnburrowNow)
            {
                yield return new Command_Action
                {
                    defaultLabel = "unburrow".Translate(),
                    defaultDesc = "unburrow wall".Translate(),
                    icon = ContentFinder<Texture2D>.Get("UI/Commands/SDWunburrow", true),
                    iconAngle = 0,
                    iconOffset = Vector2.zero,
                    iconDrawScale = GenUI.IconDrawScale(this.def),
                    action = delegate
                    {
                        this.UnburrowWall();
                    }
                };
            }

        }


        private void UnburrowWall()
        {
            SoundDefOf.DropPod_Open.PlayOneShot(new TargetInfo(this.Position, this.Map, false));
            Map map = base.Map;
            IntVec3 loc = this.Position;
            float HPp = (float)this.HitPoints / (float)this.MaxHitPoints;

            Thing thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named("FPBIWWWG"), this.Stuff), loc, map, WipeMode.Vanish);
            thing.SetFaction(Faction.OfPlayer, null);
            thing.HitPoints = (int)Math.Ceiling(thing.MaxHitPoints * HPp);

        }



    }





}
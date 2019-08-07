using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace FP_GTM
{
    public class CompProperties_FP_GTM_B : CompProperties
    {
        public CompProperties_FP_GTM_B()
        {
            this.compClass = typeof(FP_GTM_Comp);
        }
    }
    public class FP_GTM_Comp : ThingComp
    {
        public CompRefuelable refuelableComp;
        public String insideman = "";

        public CompProperties_FP_GTM_B Props
        {
            get
            {
                return (CompProperties_FP_GTM_B)this.props;
            }
        }

        public override void Initialize(CompProperties props)
        {
            base.Initialize(props);
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            this.refuelableComp = this.parent.GetComp<CompRefuelable>();

        }

        [DebuggerHidden]
        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            foreach (Gizmo c in base.CompGetGizmosExtra())
            {
                yield return c;
            }
            if (this.parent.Faction == Faction.OfPlayer)
            {
                yield return new Command_Action
                {
                    defaultLabel = "burrow".Translate(),
                    defaultDesc = "burrow Turret.".Translate(),
                    icon = ContentFinder<Texture2D>.Get("UI/Commands/GTMburrow", true),
                    iconAngle = 0,
                    iconOffset = Vector2.zero,
                    iconDrawScale = GenUI.IconDrawScale(parent.def),
                    action = delegate
                    {
                        this.burrowTurret();
                    }
                };
            }
        }



        private void burrowTurret()
        {
            SoundDefOf.DropPod_Open.PlayOneShot(new TargetInfo(parent.Position, parent.Map, false));
            Map map = parent.Map;
            String name = parent.def.defName;
            IntVec3 loc = parent.Position;
            float HPp = (float)parent.HitPoints / (float)parent.MaxHitPoints;
            float FUEL = 0;
            if(refuelableComp != null)
            {
                FUEL = refuelableComp.Fuel;
            }
            bool needStuff = true;
            ThingDef thatstuff = parent.Stuff;
            if (thatstuff == null)
            {
                thatstuff = ThingDefOf.Steel;
                needStuff = false;
            }
                
            if(parent.def.size.x > 3)
            {
                Thing thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named("GTM_Hatch_XXBig"), thatstuff), loc, map, WipeMode.Vanish);
                thing.SetFaction(Faction.OfPlayer, null);
                thing.HitPoints = (int)Math.Ceiling(thing.MaxHitPoints * HPp);

                ((GTM_Hatch)thing).insideman = name;
                if (refuelableComp != null)
                {
                    ((GTM_Hatch)thing).insidefuel = FUEL;
                }
                ((GTM_Hatch)thing).insideStuff = needStuff;
            }
            else if(parent.def.size.x > 2)
            {
                Thing thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named("GTM_Hatch_XBig"), thatstuff), loc, map, WipeMode.Vanish);
                thing.SetFaction(Faction.OfPlayer, null);
                thing.HitPoints = (int)Math.Ceiling(thing.MaxHitPoints * HPp);

                ((GTM_Hatch)thing).insideman = name;
                if (refuelableComp != null)
                {
                    ((GTM_Hatch)thing).insidefuel = FUEL;
                }
                ((GTM_Hatch)thing).insideStuff = needStuff;
            }
            else if (parent.def.Size.x > 1)
            {
                Thing thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named("GTM_Hatch_Big"), thatstuff), loc, map, WipeMode.Vanish);
                thing.SetFaction(Faction.OfPlayer, null);
                thing.HitPoints = (int)Math.Ceiling(thing.MaxHitPoints * HPp);

                ((GTM_Hatch)thing).insideman = name;
                if (refuelableComp != null)
                {
                    ((GTM_Hatch)thing).insidefuel = FUEL;
                }
                ((GTM_Hatch)thing).insideStuff = needStuff;
            }
            else
            {
                Thing thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named("GTM_Hatch"), thatstuff), loc, map, WipeMode.Vanish);
                thing.SetFaction(Faction.OfPlayer, null);
                thing.HitPoints = (int)Math.Ceiling(thing.MaxHitPoints * HPp);
                ((GTM_Hatch)thing).insideman = name;
                if (refuelableComp != null)
                {
                    ((GTM_Hatch)thing).insidefuel = FUEL;
                }
                ((GTM_Hatch)thing).insideStuff = needStuff;
            }

            


        }

    }
}
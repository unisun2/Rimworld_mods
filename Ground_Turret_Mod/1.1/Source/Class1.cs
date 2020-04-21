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
        public String Customhatch = "";
        public String CustomxPath = "";

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
            String InsideManName;
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

            if (this.Props.Customhatch != "")
            {
                InsideManName = this.Props.Customhatch;
            }
            else if (parent.def.size.x > 3)
            {
                InsideManName = "GTM_Hatch_XXBig";
            }
            else if (parent.def.size.x > 2)
            {
                InsideManName = "GTM_Hatch_XBig";
            }
            else if (parent.def.size.x > 2)
            {
                InsideManName = "GTM_Hatch_Big";
            }
            else
            {
                InsideManName = "GTM_Hatch";
            }

            Thing thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named(InsideManName), thatstuff), loc, map, WipeMode.Vanish);
            thing.SetFaction(Faction.OfPlayer, null);
            thing.HitPoints = (int)Math.Ceiling(thing.MaxHitPoints * HPp);
            if (thing.HitPoints < thing.MaxHitPoints)
            {
                thing.Map.listerBuildingsRepairable.Notify_BuildingTookDamage((Building)thing);
            }

            if (this.Props.CustomxPath != "")
            {
                ((GTM_Hatch)thing).customxpath = this.Props.CustomxPath;
            }
            ((GTM_Hatch)thing).insideman = name;
            if (refuelableComp != null)
            {
                ((GTM_Hatch)thing).insidefuel = FUEL;
            }
            ((GTM_Hatch)thing).insideStuff = needStuff;
            try
            {
                ((Action)(() =>
                {
                    if (ModCompatibilityCheck.TurretExtensionsIsActive)
                    {
                        TurretExtensions.CompUpgradable comp = this.parent.GetComp<TurretExtensions.CompUpgradable>();
                        if (comp != null)
                        {
                            if (comp.upgraded)
                            {
                                ((GTM_Hatch)thing).upgradedbyturretextensions = true;

                                if (comp.Props.statFactors.GetStatFactorFromList(StatDefOf.MaxHitPoints) != 1f)
                                {
                                    //Log.Message("GetStatFactorFromList : " + comp.Props.statFactors.GetStatFactorFromList(StatDefOf.MaxHitPoints));
                                    ((GTM_Hatch)thing).TE_HP_Factor = comp.Props.statFactors.GetStatFactorFromList(StatDefOf.MaxHitPoints);
                                }
                                if (comp.Props.statOffsets.GetStatFactorFromList(StatDefOf.MaxHitPoints) > 0)
                                {
                                    //Log.Message("GetStatFactorFromList : " + comp.Props.statFactors.GetStatFactorFromList(StatDefOf.MaxHitPoints));
                                    ((GTM_Hatch)thing).TE_HP_Offset = (int)(comp.Props.statOffsets.GetStatFactorFromList(StatDefOf.MaxHitPoints));
                                }

                            }

                        }
                    }
                }))();
            }
            catch (TypeLoadException ex)
            {
                //Log.Message("error in burrowTurret XP");
            }


            


        }

    }
}
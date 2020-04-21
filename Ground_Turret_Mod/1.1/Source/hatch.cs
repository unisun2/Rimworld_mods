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
    class GTM_Hatch : Building
    {
        public CompPowerTrader powerComp;
        public String insideman = "";
        public float insidefuel = -1;
        public bool insideStuff = false;

        public String customxpath = "";
        public Material customimg = null;
        public bool upgradedbyturretextensions;
        public float TE_HP_Factor = 1;
        public int TE_HP_Offset = 0;


        public bool CanUnburrowNow
        {
            get
            {
                return (!base.Spawned || !base.Map.gameConditionManager.ConditionIsActive(GameConditionDefOf.SolarFlare)) && this.powerComp.PowerOn && insideman != "";
            }
        }


        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            this.powerComp = base.GetComp<CompPowerTrader>();
            //if(customxpath != null)
            //{
            //    customimg = MaterialPool.MatFrom(this.customxpath);
            //}
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<String>(ref this.insideman, "insideman", "", false);
            Scribe_Values.Look<float>(ref this.insidefuel, "insidefuel", 0f, false);
            Scribe_Values.Look<bool>(ref this.insideStuff, "insideStuff", false, false);
            Scribe_Values.Look<String>(ref this.customxpath, "customxpath", "", false);
            Scribe_Values.Look<bool>(ref this.upgradedbyturretextensions, "upgradedbyturretextensions", false, false);
            Scribe_Values.Look<float>(ref this.TE_HP_Factor, "TE_HP_Factor", 1f, false);
            Scribe_Values.Look<int>(ref this.TE_HP_Offset, "TE_HP_Offset", 0, false);
        }

        public override void Draw()
        {
            if(customxpath != "")
            {
                if(customimg == null)
                {
                    customimg = MaterialPool.MatFrom(this.customxpath);
                }

                Mesh mesh = MeshPool.GridPlane(this.def.graphicData.drawSize);

                Graphics.DrawMesh(mesh, this.DrawPos, Quaternion.identity, customimg, 0);

            }
            else
            {
                base.Draw();
            }
            
        }

        public override string GetInspectString()
        {

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(base.GetInspectString());

            string newDesc = "";

            newDesc = "\nInside : " + ThingDef.Named(insideman).label;

            stringBuilder.Append(newDesc);

            return stringBuilder.ToString();

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
                    defaultLabel = "Unburrow".Translate(),
                    defaultDesc = "Unburrow Turret.".Translate(),
                    icon = ContentFinder<Texture2D>.Get("UI/Commands/GTMunburrow", true),
                    iconAngle = 0,
                    iconOffset = Vector2.zero,
                    iconDrawScale = GenUI.IconDrawScale(this.def),
                    action = delegate
                    {
                        this.UnburrowTurret();
                    }
                };
            }
            
        }


        private void UnburrowTurret()
        {
            SoundDefOf.DropPod_Open.PlayOneShot(new TargetInfo(this.Position, this.Map, false));
            Map map = base.Map;
            IntVec3 loc = this.Position;
            float HPp = (float)this.HitPoints / (float)this.MaxHitPoints;
            Thing thing;

            if (insideStuff)
            {
                thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named(insideman), this.Stuff), loc, map, WipeMode.Vanish);
            }
            else
            {
                thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named(insideman), null), loc, map, WipeMode.Vanish);
            }

            thing.SetFaction(Faction.OfPlayer, null);
            thing.HitPoints = (int)Math.Ceiling(thing.MaxHitPoints * HPp);
            if (thing.HitPoints < thing.MaxHitPoints)
            {
                thing.Map.listerBuildingsRepairable.Notify_BuildingTookDamage((Building)thing);
            }
            if (insidefuel >= 0)
            {
                CompRefuelable refuelableComp = ((ThingWithComps)thing).GetComp<CompRefuelable>();
                refuelableComp.ConsumeFuel(9999);
                refuelableComp.Refuel((insidefuel / refuelableComp.Props.FuelMultiplierCurrentDifficulty));
            }
            try
            {
                ((Action)(() =>
                {
                    if (upgradedbyturretextensions)
                    {
                        TurretExtensions.CompUpgradable compUP = ((ThingWithComps)thing).GetComp<TurretExtensions.CompUpgradable>();
                        thing.HitPoints = (int)(Math.Ceiling((thing.MaxHitPoints + TE_HP_Offset) * TE_HP_Factor * HPp));
                        compUP.upgraded = true;

                    }
                }))();
            }
            catch (TypeLoadException ex)
            {
                //Log.Message("error in unburrowTurret XP");
            }



            /*if (insideStuff)
            {
                
                Thing thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named(insideman), this.Stuff), loc, map, WipeMode.Vanish);
                thing.SetFaction(Faction.OfPlayer, null);
                thing.HitPoints = (int)Math.Ceiling(thing.MaxHitPoints * HPp);
                if (thing.HitPoints < thing.MaxHitPoints)
                {
                    thing.Map.listerBuildingsRepairable.Notify_BuildingTookDamage((Building)thing);
                }
                if (insidefuel >= 0)
                {
                    CompRefuelable refuelableComp = ((ThingWithComps)thing).GetComp<CompRefuelable>();
                    refuelableComp.ConsumeFuel(9999);
                    refuelableComp.Refuel((insidefuel / refuelableComp.Props.FuelMultiplierCurrentDifficulty));
                }
                try
                {
                    ((Action)(() =>
                    {
                        if (upgradedbyturretextensions)
                        {
                            TurretExtensions.CompUpgradable compUP = ((ThingWithComps)thing).GetComp<TurretExtensions.CompUpgradable>();
                            thing.HitPoints = (int)(Math.Ceiling((thing.MaxHitPoints + TE_HP_Offset) * TE_HP_Factor * HPp));
                            compUP.upgraded = true;
                            
                        }
                    }))();
                }
                catch (TypeLoadException ex)
                {
                    //Log.Message("error in unburrowTurret XP");
                }
                
            }
            else
            {
                Thing thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named(insideman), null), loc, map, WipeMode.Vanish);
                thing.SetFaction(Faction.OfPlayer, null);
                thing.HitPoints = (int)Math.Ceiling(thing.MaxHitPoints * HPp);
                if (thing.HitPoints < thing.MaxHitPoints)
                {
                    thing.Map.listerBuildingsRepairable.Notify_BuildingTookDamage((Building)thing);
                }
                if (insidefuel >= 0)
                {
                    CompRefuelable refuelableComp = ((ThingWithComps)thing).GetComp<CompRefuelable>();
                    refuelableComp.ConsumeFuel(9999);
                    refuelableComp.Refuel((insidefuel / refuelableComp.Props.FuelMultiplierCurrentDifficulty));
                }
                try
                {
                    ((Action)(() =>
                    {
                        if (upgradedbyturretextensions)
                        {
                            TurretExtensions.CompUpgradable compUP = ((ThingWithComps)thing).GetComp<TurretExtensions.CompUpgradable>();
                            thing.HitPoints = (int)(Math.Ceiling((thing.MaxHitPoints + TE_HP_Offset) * TE_HP_Factor * HPp));
                            compUP.upgraded = true;
                            
                        }
                    }))();
                }
                catch (TypeLoadException ex)
                {
                    Log.Message("error in unburrowTurret XP");
                }

            }

            */
                
        }

        

    }





}

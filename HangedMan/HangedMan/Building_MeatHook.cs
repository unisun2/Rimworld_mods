using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using UnityEngine;
using System.Reflection;
using Verse.AI;
using RimWorld.Planet;
using Verse.Sound;


namespace FPDBDHook
{
    public class Building_MeatHook : Building
    {
        public int pawncount = 0;
        public Pawn hangedman = null;
        public int killcount = 0;
        
        public CompForbiddable forbiddable;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref pawncount, "FPDBDHookpawncount", defaultValue: 0);
            Scribe_Values.Look<int>(ref killcount, "FPDBDHookkillcount", defaultValue: 0);
            Scribe_Values.Look<Pawn>(ref hangedman, "FPDBDHookhangedman", defaultValue: null);
        }

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);

            forbiddable = GetComp<CompForbiddable>();

        }

        public bool Accepts(){
            if(pawncount > 0 && hangedman != null){
                if(hangedman.Spawned)
                    return false;
            }
            return true;
        }

        public bool TryAcceptThing(Thing thing, bool allowSpecialEffects = true)
        {
            HookSoundDef.FPDBDHooksound.PlayOneShot(new TargetInfo(this.Position, base.Map, false));
            Pawn pawn = thing as Pawn;
            if(pawn !=null)
            {
                this.pawncount = 1;
                this.hangedman = pawn;
                if(pawn.RaceProps.Humanlike)
                    pawn.needs.mood.thoughts.memories.TryGainMemory(HookThoughtDef.FPDBDHooked, null);
                Hediff hediff = HediffMaker.MakeHediff(HookHediffDef.FPDBDHookhediff, pawn, null);
                pawn.health.AddHediff(hediff);
                pawn.jobs.posture = PawnPosture.Standing;
                pawn.Rotation = Rot4.South;
                if(pawn.RaceProps.Humanlike){
                    foreach (Pawn p in this.Map.mapPawns.SpawnedPawnsInFaction(Faction))
                        {
                            if (p.needs != null && p.needs.mood != null && p.needs.mood.thoughts != null)
                            {
                                p.needs.mood.thoughts.memories.TryGainMemory(HookThoughtDef.KnowFPDBDHookedHumanlike, null);
                                p.needs.mood.thoughts.memories.TryGainMemory(HookThoughtDef.KnowFPDBDHookedHumanlikeCannibal, null);
                                p.needs.mood.thoughts.memories.TryGainMemory(HookThoughtDef.KnowFPDBDHookedHumanlikeBloodlust, null);
                            }
                        }
                }


            }
            return true;
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
	        foreach (Gizmo gizmo in base.GetGizmos())
	        {
		        yield return gizmo;
	        }
	        if (base.Faction == Faction.OfPlayer && this.pawncount > 0)
	        {
		        Command_Action command_Action = new Command_Action();
		        command_Action.action = unhook;
		        command_Action.defaultLabel = "FPDBDUnhook".Translate();
		        command_Action.defaultDesc = "FPDBDUnhook".Translate();
		        if (this.pawncount == 0)
		        {
			        command_Action.Disable("FPDBDUnhookFailEmpty".Translate());
		        }
		        command_Action.hotKey = KeyBindingDefOf.Misc8;
		        command_Action.icon = ContentFinder<Texture2D>.Get("UI/Commands/PodEject");
		        yield return command_Action;
	        }
        }

        public void unhook(){
            this.hangedman = null;
            this.pawncount = 0;
        }


        public override void TickRare()
        {
            bool victimcheck = true;
            base.TickRare();
            if (this.Spawned && this.hangedman != null && this.pawncount > 0)
            {
                List<Thing> thingList = base.Position.GetThingList(base.Map);
                for (int i = 0; i < thingList.Count; i++)
                {
                    Pawn pawn = thingList[i] as Pawn;
                    if (pawn == this.hangedman)
                    {
                        Hediff hediff = HediffMaker.MakeHediff(HookHediffDef.FPDBDHookhediff, pawn, null);
                        pawn.health.AddHediff(hediff);
                        victimcheck = false;
                        break;
                    }
                }
                if (victimcheck){
                    this.hangedman = null;
                    this.pawncount = 0;
                }
            }
        }

        public override string GetInspectString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(base.GetInspectString());

            String hangedmanname = (this.hangedman != null)? (hangedman.Name).ToString() : "None";

            stringBuilder.AppendInNewLine("FPDBDHookKillcounts".Translate() + " : " + this.killcount.ToString());
            stringBuilder.AppendInNewLine("FPDBDHookVictim".Translate() + " : " + hangedmanname);

            return stringBuilder.ToString();
        }



        public static Building_MeatHook FindHookFor(Pawn p, Pawn traveler, bool ignoreOtherReservations = false)
        {
            IEnumerable<ThingDef> enumerable = from def in DefDatabase<ThingDef>.AllDefs
                                               where typeof(Building_MeatHook).IsAssignableFrom(def.thingClass)
                                               select def;

            foreach (ThingDef singleDef in enumerable)
            {
                Building_MeatHook building_MeatHook = (Building_MeatHook)GenClosest.ClosestThingReachable(p.Position, p.Map, ThingRequest.ForDef(singleDef), PathEndMode.InteractionCell, TraverseParms.For(traveler, Danger.Deadly, TraverseMode.ByPawn, false), 9999f, delegate(Thing x)
                {
                    bool result;
                    if (!((Building_MeatHook)x).Accepts())
                    {
                        Pawn traveler2 = traveler;
                        LocalTargetInfo target = x;
                        bool ignoreOtherReservations2 = ignoreOtherReservations;
                        result = traveler2.CanReserve(target, 1, -1, null, ignoreOtherReservations2);
                    }
                    else
                    {
                        result = false;
                    }
                    return result;
                }, null, 0, -1, false, RegionType.Set_Passable, false);
                if (building_MeatHook != null && !building_MeatHook.forbiddable.Forbidden)
                {
                    return building_MeatHook;
                }
            }
            return null;
        }
    }
}

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


namespace FPDBDHook
{
    public class Building_MeatHook : Building
    {
        public int pawncount = 0;

        public int killcount = 0;
        
        public CompForbiddable forbiddable;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref pawncount, "pawncount", defaultValue: 0);
            Scribe_Values.Look<int>(ref killcount, "killcount", defaultValue: 0);
        }

        public bool HasAnyContents => pawncount > 0;

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);

            forbiddable = GetComp<CompForbiddable>();

        }


        public override void Tick()
        {
            base.Tick();
            if (base.Spawned)
            {
                List<Thing> thingList = base.Position.GetThingList(base.Map);
                for (int i = 0; i < thingList.Count; i++)
                {
                    Pawn pawn = thingList[i] as Pawn;
                    if (pawn != null && pawn.GetPosture() == PawnPosture.LayingInBed && pawn.IsPrisoner)
                    {
                        Hediff hediff = HediffMaker.MakeHediff(HangedManDefOf.HangedManHediff_Hooked, pawn, null);
                        pawn.health.AddHediff(hediff);
                    }
                }
            }
        }

        public override string GetInspectString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(base.GetInspectString());

            stringBuilder.AppendInNewLine("Killcounts".Translate() + ": " + this.killcount.ToString());

            return stringBuilder.ToString();
        }



        public static Building_MeatHook FindBioReactorFor(Pawn p, Pawn traveler, bool ignoreOtherReservations = false)
        {
            IEnumerable<ThingDef> enumerable = from def in DefDatabase<ThingDef>.AllDefs
                                               where typeof(Building_MeatHook).IsAssignableFrom(def.thingClass)
                                               select def;

            foreach (ThingDef singleDef in enumerable)
            {
                Building_MeatHook building_MeatHook = (Building_MeatHook)GenClosest.ClosestThingReachable(p.Position, p.Map, ThingRequest.ForDef(singleDef), PathEndMode.InteractionCell, TraverseParms.For(traveler, Danger.Deadly, TraverseMode.ByPawn, false), 9999f, delegate(Thing x)
                {
                    bool result;
                    if (!((Building_MeatHook)x).HasAnyContents)
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
                    return building_BioReactor;
                }
            }
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.Sound;
using UnityEngine;
using HarmonyLib;
using RimWorld.Planet;

namespace FPDBDHook
{
    [StaticConstructorOnStartup]
    internal static class harmony_patches
    {
        
        static harmony_patches(){

            Log.Message("Initializing FPDBD_Hook_patches...");
            Harmony harmonyInstance = new Harmony("Flashpoint55.FPDBDHook");

            harmonyInstance.Patch(AccessTools.Method(typeof(RimWorld.TaleUtility), "Notify_PawnDied"), new HarmonyMethod(typeof(harmony_patches), "Prefix_AddHumanlikeOrders"));

            harmonyInstance.Patch(AccessTools.Method(typeof(FloatMenuMakerMap), "AddHumanlikeOrders"), new HarmonyMethod(typeof(harmony_patches), "Pre_Notify_PawnDied"));


        }


        // TaleUtility.Notify_PawnDied(this, dinfo);
        public static void Pre_Notify_PawnDied(ref Pawn __victim, ref DamageInfo? dinfo)
        {
            if (__victim.Spawned && __victim.Map != null)
            {
                List<Thing> thingList = __victim.Position.GetThingList(__victim.Map);
                for (int i = 0; i < thingList.Count; i++)
                {
                    Pawn pawn = thingList[i] as Pawn;

                    Building_MeatHook building_meathook = thingList[i] as Building_MeatHook;
                    if(building_meathook != null){
                        building_meathook.killcount++;
                    }

                }
            }
        }

        public static bool Prefix_AddHumanlikeOrders(Vector3 clickPos, Pawn pawn, List<FloatMenuOption> opts)
        {
            if (pawn.health.capacities.CapableOf(PawnCapacityDefOf.Manipulation))
            {
                foreach (LocalTargetInfo localTargetInfo3 in GenUI.TargetsAt(clickPos, TargetingParameters.ForRescue(pawn), true))
                {
                    LocalTargetInfo localTargetInfo4 = localTargetInfo3;
                    Pawn victim = (Pawn)localTargetInfo4.Thing;
                    if (victim.Downed && pawn.CanReserveAndReach(victim, PathEndMode.OnCell, Danger.Deadly, 1, -1, null, true) && Building_BioReactor.FindBioReactorFor(victim, pawn, true) != null)
                    {
                        string text4 = "CarryToBioReactor".Translate(localTargetInfo4.Thing.LabelCap, localTargetInfo4.Thing);
                        JobDef jDef = Bio_JobDefOf.CarryToBioReactor;
                        Action action3 = delegate()
                        {
                            Building_BioReactor building_BioReactor = Building_BioReactor.FindBioReactorFor(victim, pawn, false);
                            if (building_BioReactor == null)
                            {
                                building_BioReactor = Building_BioReactor.FindBioReactorFor(victim, pawn, true);
                            }
                            if (building_BioReactor == null)
                            {
                                Messages.Message("CannotCarryToBioReactor".Translate() + ": " + "NoBioReactor".Translate(), victim, MessageTypeDefOf.RejectInput, false);
                                return;
                            }
                            Job job = new Job(jDef, victim, building_BioReactor);
                            job.count = 1;
                            pawn.jobs.TryTakeOrderedJob(job, JobTag.Misc);
                        };
                        string label = text4;
                        Action action2 = action3;
                        Pawn revalidateClickTarget = victim;
                        opts.Add(FloatMenuUtility.DecoratePrioritizedTask(new FloatMenuOption(label, action2, MenuOptionPriority.Default, null, revalidateClickTarget, 0f, null, null), pawn, victim, "ReservedBy"));
                    }
                }
            }
            return true;
        }

    }
}

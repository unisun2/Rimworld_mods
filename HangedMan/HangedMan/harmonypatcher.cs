using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;
using UnityEngine;

namespace HangedMan
{
    [StaticConstructorOnStartup]
    internal static class harmony_patches
    {
        Log.Message("Initializing RPG_Style_Level_Up_Mod_patches...");
        Harmony harmonyInstance = new Harmony("Flashpoint55.HangedMan");

        harmonyInstance.Patch(AccessTools.Method(typeof(RimWorld.TaleUtility), "Notify_PawnDied"), new HarmonyMethod(typeof(harmony_patches), "LearnPrefix"));

        // TaleUtility.Notify_PawnDied(this, dinfo);
        public static void Post_Notify_PawnDied(ref Pawn __victim, ref DamageInfo? dinfo)
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

    }
}

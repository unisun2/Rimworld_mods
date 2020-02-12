using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;
using Harmony;
using System.Reflection;
using UnityEngine;

namespace FP_RSLUM
{
    [StaticConstructorOnStartup]
    internal static class harmony_patches
    {
        public static readonly Texture2D DistributeIMG = ContentFinder<Texture2D>.Get("UI/Icons/Distribute");
        static harmony_patches()
        {
            Log.Message("Initializing RPG_Style_Level_Up_Mod_patches...");
            HarmonyInstance harmonyInstance = HarmonyInstance.Create("Flashpoint55.FP_RSLUM");
            harmonyInstance.Patch(AccessTools.Method(typeof(RimWorld.SkillRecord), "Learn"), new HarmonyMethod(typeof(harmony_patches), "LearnPrefix"));
            //harmonyInstance.Patch(AccessTools.Method(typeof(RimWorld.MassUtility), "Capacity"), new HarmonyMethod(typeof(harmony_patches), "CapacityPostfix"));
            harmonyInstance.Patch(AccessTools.Method(typeof(Verse.Pawn), "PreApplyDamage"), new HarmonyMethod(typeof(harmony_patches), "PreApplyDamagePrefix"));
            harmonyInstance.Patch(AccessTools.Method(typeof(RimWorld.StatWorker_MeleeDPS), "GetMeleeDamage"), null, new HarmonyMethod(typeof(harmony_patches), "GetMeleeDamagePostfix"));
            var original = AccessTools.Method(typeof(MassUtility), nameof(MassUtility.Capacity));
            var postfix = AccessTools.Method(typeof(harmony_patches), nameof(harmony_patches.CapacityPostfix));
            harmonyInstance.Patch(original, null, new HarmonyMethod(postfix));
        }

        static FieldInfo pawninfo = AccessTools.Field(typeof(SkillRecord), "pawn");
        static FieldInfo pawninfo2 = AccessTools.Field(typeof(MassUtility), "pawn");

        [HarmonyPrefix]
        static bool LearnPrefix(SkillRecord __instance, float xp, bool direct)
        {
            if(xp > 0)
            {
                Pawn pawn = pawninfo.GetValue(__instance) as Pawn;

                PawnLvComp pawnlvcomp = pawn.TryGetComp<PawnLvComp>();
                pawnlvcomp.exp += (int)(xp * 100);
                //Log.Message(pawn.Name + xp.ToString() + " " + ((int)(xp * 100)).ToString());
            }

            return true;
        }

        [HarmonyPostfix]
        public static void CapacityPostfix(Pawn p, ref float __result)
        {
            if (ModCompatibilityCheck.CombatExtendedIsActive)
            {
                return;
            }
            if (__result > 0)
            {
                //Pawn p = pawninfo2.GetValue(__instance) as Pawn;
                //Log.Message(p.Name + __result.ToString());
                PawnLvComp pawnlvcomp = p.TryGetComp<PawnLvComp>();
                __result *= (float)(1f + (0.01 * pawnlvcomp.STR));
                //Log.Message(p.Name + __result.ToString());
            }

        }

        [HarmonyPrefix]
        public static bool PreApplyDamagePrefix(ref DamageInfo dinfo, out bool absorbed)
        {
            absorbed = false;
            Pawn pawn = dinfo.IntendedTarget as Pawn;
            if(pawn != null)
            {
                float oriAmount = dinfo.Amount;
                //Log.Message(pawn.Name + " " + dinfo.Amount);
                PawnLvComp pawnlvcomp = pawn.TryGetComp<PawnLvComp>();
                float ff = oriAmount * ((float)Math.Max(1 - (0.003 * pawnlvcomp.CON), 0.5f));
                dinfo.SetAmount(ff);
                //Log.Message(pawn.Name + " " + dinfo.Amount);
            }
            return true;
        }

        [HarmonyPostfix]
        public static void GetMeleeDamagePostfix(StatRequest req, ref float __result)
        {
            Pawn pawn = req.Thing as Pawn;
            //Log.Message(__result.ToString());
            if (pawn != null)
            {
                //Log.Message(__result.ToString() + "i");
                PawnLvComp pawnlvcomp = pawn.TryGetComp<PawnLvComp>();
                __result *= (float)(1.0f + (0.01 * pawnlvcomp.STR));
                //Log.Message(__result.ToString() + "in postfix");
            }

        }
    }
}

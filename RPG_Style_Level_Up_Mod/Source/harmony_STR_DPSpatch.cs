using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony;
using Verse;
using RimWorld;
using UnityEngine;
using System.Reflection;

namespace FP_RSLUM
{
    [StaticConstructorOnStartup]
    internal static class harmony_STR_DPSpatch
    {
        static harmony_STR_DPSpatch()
        {
            HarmonyInstance harmonyInstance = HarmonyInstance.Create("Flashpoint55.FP_RSLUM_STR_DPS");
            harmonyInstance.Patch(AccessTools.Method(typeof(RimWorld.StatWorker_MeleeDPS), "GetMeleeDamage"), new HarmonyMethod(typeof(harmony_STR_DPSpatch), "GetMeleeDamagePostfix"));
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

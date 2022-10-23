using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;
using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace FP_RSLUM
{
    [StaticConstructorOnStartup]
    internal static class harmony_patches
    {
        public static readonly Texture2D DistributeIMG = ContentFinder<Texture2D>.Get("UI/Icons/Distribute");
        public static readonly Texture2D LVUP_rerollIMG = ContentFinder<Texture2D>.Get("UI/Icons/LVUP_reroll");
        static harmony_patches()
        {
            Log.Message("Initializing RPG_Style_Level_Up_Mod_patches...");
            Harmony harmonyInstance = new Harmony("Flashpoint55.FP_RSLUM");
            harmonyInstance.Patch(AccessTools.Method(typeof(RimWorld.SkillRecord), "Learn"), new HarmonyMethod(typeof(harmony_patches), "LearnPrefix"));
            //harmonyInstance.Patch(AccessTools.Method(typeof(RimWorld.MassUtility), "Capacity"), new HarmonyMethod(typeof(harmony_patches), "CapacityPostfix"));
            //harmonyInstance.Patch(AccessTools.Method(typeof(Verse.Pawn), "PreApplyDamage"), new HarmonyMethod(typeof(harmony_patches), "PreApplyDamagePrefix"));

            //harmonyInstance.Patch(AccessTools.Method(typeof(Verse.VerbProperties), "AdjustedMeleeDamageAmount"),null, 
            //    new HarmonyMethod(typeof(harmony_patches), "AdjustedMeleeDamageAmountPostfix", new[] { typeof(Tool), typeof(Pawn), typeof(Thing), typeof(HediffComp_VerbGiver) }));
            harmonyInstance.Patch(AccessTools.Method(typeof(Verse.VerbProperties), "GetDamageFactorFor", new Type[] { typeof(Tool), typeof(Pawn), typeof(HediffComp_VerbGiver) }),
                null, new HarmonyMethod(typeof(harmony_patches), "GetDamageFactorForPostFix"));


            //var meleeharmony = AccessTools.Method(typeof(VerbProperties), nameof(VerbProperties.AdjustedMeleeDamageAmount));
            //var meleepostfix = AccessTools.Method(typeof(harmony_patches), nameof(harmony_patches.AdjustedMeleeDamageAmountPostfix));


            harmonyInstance.Patch(AccessTools.Method(typeof(RimWorld.MassUtility), "Capacity"), null, new HarmonyMethod(typeof(harmony_patches), "CapacityPostfix"));

            harmonyInstance.Patch(AccessTools.Method(typeof(RimWorld.RecordsUtility), "Notify_PawnKilled"), null, new HarmonyMethod(typeof(harmony_patches), "Notify_PawnKilledPostfix"));

        }

        static FieldInfo pawninfo = AccessTools.Field(typeof(SkillRecord), "pawn");
        static FieldInfo pawninfo2 = AccessTools.Field(typeof(MassUtility), "pawn");

        [HarmonyPrefix]
        static bool LearnPrefix(SkillRecord __instance, float xp, bool direct)
        {
            if (xp > 0)
            {
                Pawn pawn = pawninfo.GetValue(__instance) as Pawn;

                PawnLvComp pawnlvcomp = pawn.TryGetComp<PawnLvComp>();
                if (pawnlvcomp != null)
                {
                    pawnlvcomp.exp += (int)(xp * FP_RSLUM_setting.ColonistPercent * (1f + (0.01 * pawnlvcomp.INT)));

                    if ((pawnlvcomp.exp > pawnlvcomp.need_exp) && (pawnlvcomp.level < FP_RSLUM_setting.MaxLevel || FP_RSLUM_setting.MaxLevel == 0))
                    {
                        pawnlvcomp.levelup();
                    }
                }

                //Log.Message(pawn.Name + xp.ToString() + " " + ((int)(xp * 100)).ToString());
            }

            return true;
        }

        [HarmonyPostfix]
        public static void CapacityPostfix(Pawn p, ref float __result)
        {
            if (ModCompatibilityCheck.CombatExtendedIsActive || ModCompatibilityCheck.CarryCapacityFixIsActive)
            {
                return;
            }
            if (__result > 0)
            {
                //Pawn p = pawninfo2.GetValue(__instance) as Pawn;
                //Log.Message(p.Name + __result.ToString());
                PawnLvComp pawnlvcomp = p.TryGetComp<PawnLvComp>();
                if (pawnlvcomp != null)
                    __result *= (float)(1f + (0.01 * pawnlvcomp.STR));
                //Log.Message(p.Name + __result.ToString());
            }

        }
        /*
        [HarmonyPrefix]
        public static bool PreApplyDamagePrefix(ref DamageInfo dinfo, out bool absorbed)
        {
            absorbed = false;
            Pawn pawn = dinfo.IntendedTarget as Pawn;
            if (pawn != null)
            {
                float oriAmount = dinfo.Amount;
                float ff = oriAmount;
                //Log.Message(pawn.Name + " " + dinfo.Amount);
                PawnLvComp pawnlvcomp = pawn.TryGetComp<PawnLvComp>();
                if (pawnlvcomp != null)
                    ff = oriAmount * ((float)Math.Max(1 - (0.003 * pawnlvcomp.CON), 0.5f));
                dinfo.SetAmount(ff);
                //Log.Message(pawn.Name + " " + dinfo.Amount);
            }
            return true;
        }*/

        [HarmonyPostfix]
        public static void GetDamageFactorForPostFix(Tool tool, Pawn attacker, ref float __result)
        {
            //Verse.Log.Message(attacker.Name + " " + __result.ToString());
            if (attacker != null)
            {
                PawnLvComp pawnlvcomp = attacker.TryGetComp<PawnLvComp>();
                if (pawnlvcomp != null)
                    __result *= (float)(1.0f + (0.003 * pawnlvcomp.STR));
            }
            //Verse.Log.Message(attacker.Name + " " + __result.ToString());
        }

        [HarmonyPostfix]
        public static void Notify_PawnKilledPostfix(Pawn killed, Pawn killer)
        {
            if (killer != null)
            {
                PawnLvComp pawnlvcomp = killer.TryGetComp<PawnLvComp>();
                if (pawnlvcomp != null)
                {
                    pawnlvcomp.exp += (int)killed.kindDef.combatPower * FP_RSLUM_setting.KillExpMult;
                    //Log.Message((killed.kindDef.combatPower * FP_RSLUM_setting.KillExpMult).ToString());
                    if ((pawnlvcomp.exp > pawnlvcomp.need_exp) && (pawnlvcomp.level < FP_RSLUM_setting.MaxLevel || FP_RSLUM_setting.MaxLevel == 0))
                    {
                        pawnlvcomp.levelup();
                    }


                }

            }
        }

    }
}

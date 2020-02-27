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
            harmonyInstance.Patch(AccessTools.Method(typeof(Verse.Pawn), "PreApplyDamage"), new HarmonyMethod(typeof(harmony_patches), "PreApplyDamagePrefix"));

            //harmonyInstance.Patch(AccessTools.Method(typeof(Verse.VerbProperties), "AdjustedMeleeDamageAmount"),null, 
            //    new HarmonyMethod(typeof(harmony_patches), "AdjustedMeleeDamageAmountPostfix", new[] { typeof(Tool), typeof(Pawn), typeof(Thing), typeof(HediffComp_VerbGiver) }));
            harmonyInstance.Patch(AccessTools.Method(typeof(RimWorld.StatWorker_MeleeDPS), "GetMeleeDamage"), null, new HarmonyMethod(typeof(harmony_patches), "GetMeleeDamagePostfix"));


            //var meleeharmony = AccessTools.Method(typeof(VerbProperties), nameof(VerbProperties.AdjustedMeleeDamageAmount));
            //var meleepostfix = AccessTools.Method(typeof(harmony_patches), nameof(harmony_patches.AdjustedMeleeDamageAmountPostfix));


            harmonyInstance.Patch(AccessTools.Method(typeof(RimWorld.MassUtility), "Capacity"), null, new HarmonyMethod(typeof(harmony_patches), "CapacityPostfix"));

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
                if (pawnlvcomp != null)
                    __result *= (float)(1.0f + (0.01 * pawnlvcomp.STR));
                //Log.Message(__result.ToString() + "in postfix");
            }

        }

        [HarmonyPostfix]
        public static void AdjustedMeleeDamageAmountPostfix(Tool tool, Pawn attacker, Thing equipment, HediffComp_VerbGiver hediffCompSource, ref float __result)
        {
            PawnLvComp pawnlvcomp = attacker.TryGetComp<PawnLvComp>();
            Log.Message(__result.ToString());
            if (pawnlvcomp != null)
            {
                //Log.Message(__result.ToString() + "i");

                __result *= (float)(1.0f + (0.01 * pawnlvcomp.STR));
                //Log.Message(__result.ToString() + "in postfix");
            }

        }

        /*[HarmonyPostfix]
        public static void GatheredPostfix(CompHasGatherableBodyResource __instance, Pawn doer)
        {
            Log.Message(doer.Name.ToStringFull);

            if (!Rand.Chance(doer.GetStatValue(StatDefOf.AnimalGatherYield, true)))
            {
                Vector3 loc = (doer.DrawPos + __instance.parent.DrawPos) / 2f;
                MoteMaker.ThrowText(loc, __instance.parent.Map, "TextMote_ProductWasted".Translate(), 3.65f);
            }
            else
            {
                PawnLvComp pawnlvcomp = __instance.parent.TryGetComp<PawnLvComp>();
                if(pawnlvcomp != null)
                {
                    int Re_Am = (int)pawninfo_ResourceAmount.GetValue(__instance);
                    ThingDef Re_Def = pawninfo_ResourceDef.GetValue(__instance) as ThingDef;
                    int i = (int)(Re_Am * (1 + 0.02 * pawnlvcomp.CHA));
                    while (i > 0)
                    {
                        int num = Mathf.Clamp(i, 1, Re_Def.stackLimit);
                        i -= num;
                        Thing thing = ThingMaker.MakeThing(Re_Def, null);
                        thing.stackCount = num;
                        GenPlace.TryPlaceThing(thing, doer.Position, doer.Map, ThingPlaceMode.Near, null, null);
                    }
                }
                
            }
        }*/
    }
}

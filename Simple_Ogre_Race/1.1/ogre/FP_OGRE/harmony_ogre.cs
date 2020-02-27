using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using HarmonyLib;

namespace FP_OGRE
{
    [StaticConstructorOnStartup]
    public static class harmony_ogre
    {
        private static readonly Type patchType = typeof(harmony_ogre);
        static harmony_ogre()
        {
            Harmony harmonyInstance = new Harmony("FP.FP_OGRE");

            harmonyInstance.Patch(AccessTools.Method(typeof(PawnGenerator), "GenerateInitialHediffs", null, null), null, new HarmonyMethod(patchType, "GenerateInitialHediffsPostFix", null), null);
            Verse.Log.Message("FP_OGRE_patched");
        }

        
        [HarmonyPostfix]
        public static void GenerateInitialHediffsPostFix(Pawn pawn, PawnGenerationRequest request)
        {
            if (pawn.def.defName == "FP_OgreRace")
            {
                Hediff hediff = HediffMaker.MakeHediff(HediffDefOf.FP_OGRE_hediff, pawn, null);
                hediff.Severity = 0.1f;
                pawn.health.AddHediff(hediff, null, null, null);
            }
        }


    }
    
}

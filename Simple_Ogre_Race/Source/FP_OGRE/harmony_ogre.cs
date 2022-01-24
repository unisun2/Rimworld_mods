using HarmonyLib;
using Verse;

namespace FP_OGRE;

[StaticConstructorOnStartup]
public static class harmony_ogre
{
    static harmony_ogre()
    {
        var patchType = typeof(harmony_ogre);
        var harmony = new Harmony("FP.FP_OGRE");
        harmony.Patch(AccessTools.Method(typeof(PawnGenerator), "GenerateInitialHediffs"), null,
            new HarmonyMethod(patchType, "GenerateInitialHediffsPostFix"));
        Log.Message("FP_OGRE_patched");
    }

    [HarmonyPostfix]
    public static void GenerateInitialHediffsPostFix(Pawn pawn, PawnGenerationRequest request)
    {
        if (pawn.def.defName != "FP_OgreRace")
        {
            return;
        }

        var hediff = HediffMaker.MakeHediff(HediffDefOf.FP_OGRE_hediff, pawn);
        hediff.Severity = 0.1f;
        pawn.health.AddHediff(hediff);
    }
}
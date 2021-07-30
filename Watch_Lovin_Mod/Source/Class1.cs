using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using HarmonyLib;
using System.Reflection;
using Verse.AI;

namespace FP_WSL
{
    [DefOf]
    public static class thoughtdefof
    {
        public static ThoughtDef FP_WatchSomeLov;
    }


    [StaticConstructorOnStartup]
    internal static class Class1
    {
        static Class1()
        {
            Harmony harmonyInstance = new Harmony("Flashpoint55.FP_WSLmod");
            harmonyInstance.Patch(AccessTools.Method(typeof(RimWorld.JobDriver_Lovin), "GenerateRandomMinTicksToNextLovin"),
                new HarmonyMethod(typeof(Class1), "GenerateRandomMinTicksToNextLovinPrefix"));
        }

        static FieldInfo PartnerIndinfo = AccessTools.Field(typeof(JobDriver_Lovin), "PartnerInd");

        [HarmonyPrefix]
        static bool GenerateRandomMinTicksToNextLovinPrefix(JobDriver_Lovin __instance, Pawn pawn)
        {
            TargetIndex PartnerInd = (TargetIndex)PartnerIndinfo.GetValue(__instance);
            Pawn partner = (Pawn)(Thing)__instance.job.GetTarget(PartnerInd);
            if (partner == null)
            {
                Log.Message("partner null error");
            }else
                Log.Message("partner = " + partner.Name);


            foreach (Thing Wpawn in GenRadial.RadialDistinctThingsAround(pawn.Position, pawn.Map, 6f, useCenter: true))
            {

                Pawn wpawn = Wpawn as Pawn;
                if (wpawn != null)
                {
                    if(wpawn != pawn && wpawn != partner && wpawn.gender != Gender.None) //
                    {
                        if (wpawn.health.capacities.CapableOf(PawnCapacityDefOf.Sight))
                        {
                            wpawn.needs?.mood?.thoughts?.memories?.TryGainMemory(thoughtdefof.FP_WatchSomeLov);
                        }
                    }
                }
            }
            return true;
        }



    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Verse;
using RimWorld;

namespace WG_GOM
{
    [StaticConstructorOnStartup]
    internal static class harmonypatch
    {
        static harmonypatch()
        {
            Log.Message("More Quest For The Quest God! patched.");
            Harmony harmonyInstance = new Harmony("Flashpoint55.WG_GOM");
            harmonyInstance.Patch(AccessTools.Method(typeof(RimWorld.IncidentWorker_GiveQuest), "TryExecuteWorker"), new HarmonyMethod(typeof(harmonypatch), "TryExecuteWorkerPrefix"));

        }

        [HarmonyPrefix]
        static bool TryExecuteWorkerPrefix(IncidentParms parms, IncidentDef ___def)
        {
            for(int i = 0; i < WG_GOM_setting.questnum; i++)
            {
                QuestUtility.SendLetterQuestAvailable(QuestUtility.GenerateQuestAndMakeAvailable(NaturalRandomQuestChooser.ChooseNaturalRandomQuest(parms.points, parms.target), parms.points));
            }
            return true;
        }

    }
}

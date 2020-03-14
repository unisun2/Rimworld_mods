using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using RimWorld;
using RimWorld.QuestGen;
using Verse;
using Verse.Sound;

namespace WG_GOM
{
    public class IncidentWorker_GetOutMod : IncidentWorker
    {

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            return true;
        }

        public void Try_exe_this(IncidentDef localDef, IncidentParms parms)
        {
            if (localDef == null) return;

            if(localDef.Worker.CanFireNow(parms))
                localDef.Worker.TryExecute(parms);

        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            IncidentDef localDef;
            // vanila

            //IncidentDef localDef = DefDatabase<IncidentDef>.GetNamed("Quest_TradeRequest", false);
            //Try_exe_this(localDef, parms);

            //localDef = DefDatabase<IncidentDef>.GetNamed("Quest_BanditCamp", false);
            //Try_exe_this(localDef, parms);

            //localDef = DefDatabase<IncidentDef>.GetNamed("Quest_ItemStash", false);
            //Try_exe_this(localDef, parms);

            //localDef = DefDatabase<IncidentDef>.GetNamed("Quest_DownedRefugee", false);
            //Try_exe_this(localDef, parms);

            //localDef = DefDatabase<IncidentDef>.GetNamed("Quest_BanditCamp", false);
            // Try_exe_this(localDef, parms);

            // Go Explore!

            localDef = DefDatabase<IncidentDef>.GetNamed("LostCityLGE", false);
            Try_exe_this(localDef, parms);

            localDef = DefDatabase<IncidentDef>.GetNamed("AmbrosiaAnimalsLGE", false);
            Try_exe_this(localDef, parms);

            localDef = DefDatabase<IncidentDef>.GetNamed("PrisonCampLGE", false);
            Try_exe_this(localDef, parms);

            localDef = DefDatabase<IncidentDef>.GetNamed("NewSettlementLGE", false);
            if(Rand.Range(0f,1f) > 0.9)
                Try_exe_this(localDef, parms);

            localDef = DefDatabase<IncidentDef>.GetNamed("QuestResearchRequestSW", false);
            Try_exe_this(localDef, parms);

            localDef = DefDatabase<IncidentDef>.GetNamed("InterceptedMessageLGE", false);
            Try_exe_this(localDef, parms);

            //Sparkling Worlds Addon - More Events [1.0] - Standalone Addon

            localDef = DefDatabase<IncidentDef>.GetNamed("ShipCrashSW", false);
            Try_exe_this(localDef, parms);

            localDef = DefDatabase<IncidentDef>.GetNamed("ThrumboSightingSW", false);
            Try_exe_this(localDef, parms);

            localDef = DefDatabase<IncidentDef>.GetNamed("QuestDoctorRequestSW", false);
            Try_exe_this(localDef, parms);

            localDef = DefDatabase<IncidentDef>.GetNamed("TradeFairSW", false);
            Try_exe_this(localDef, parms);

            localDef = DefDatabase<IncidentDef>.GetNamed("HuntingLodgeOppSW", false);
            Try_exe_this(localDef, parms);

            // vanlia faction - medieval

            localDef = DefDatabase<IncidentDef>.GetNamed("VFEM_Quest_MedievalTournament", false);
            Try_exe_this(localDef, parms);

            localDef = DefDatabase<IncidentDef>.GetNamed("VFEM_Quest_CastleRuins", false);
            Try_exe_this(localDef, parms);

            // more faction interaction

            localDef = DefDatabase<IncidentDef>.GetNamed("MFI_BumperCropRequest", false);
            Try_exe_this(localDef, parms);

			// give quest - 1.1 ver!

			

			List<DebugMenuOption> list = new List<DebugMenuOption>();
			float localPoints = parms.points ;
			Slate testSlate = default(Slate);
			Action<QuestScriptDef> generate = delegate (QuestScriptDef script)
			{
				List<DebugMenuOption> list2 = new List<DebugMenuOption>();
				foreach (float item in DebugActionsUtility.PointsOptions(extended: false))
				{
					localPoints = item;
					string text = item.ToString("F0");
					testSlate = new Slate();
					testSlate.Set("points", localPoints);
					if (script != null)
					{
						if (script.IsRootDecree)
						{
							testSlate.Set("asker", PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_FreeColonists.RandomElement());
						}
						if (script == QuestScriptDefOf.LongRangeMineralScannerLump)
						{
							testSlate.Set("targetMineable", ThingDefOf.MineableGold);
							testSlate.Set("worker", PawnsFinder.AllMaps_FreeColonists.FirstOrDefault());
						}
						if (!script.CanRun(testSlate))
						{
							text += " [not now]";
						}
					}
					list2.Add(new DebugMenuOption(text, DebugMenuOptionMode.Action, delegate
					{
						int num = 0;
						bool flag = script == null;
						for (int i = 0; i < count; i++)
						{
							if (flag)
							{
								script = NaturalRandomQuestChooser.ChooseNaturalRandomQuest(localPoints, Find.CurrentMap);
								Find.CurrentMap.StoryState.RecordRandomQuestFired(script);
							}
							if (script.IsRootDecree)
							{
								Pawn pawn = testSlate.Get<Pawn>("asker");
								if (pawn.royalty.AllTitlesForReading.NullOrEmpty())
								{
									pawn.royalty.SetTitle(Faction.Empire, RoyalTitleDefOf.Knight, grantRewards: false);
									Messages.Message("Dev: Gave " + RoyalTitleDefOf.Knight.label + " title to " + pawn.LabelCap, pawn, MessageTypeDefOf.NeutralEvent, historical: false);
								}
							}
							if (count != 1 && !script.CanRun(testSlate))
							{
								num++;
							}
							else if (!logDescOnly)
							{
								QuestUtility.SendLetterQuestAvailable(QuestUtility.GenerateQuestAndMakeAvailable(script, testSlate));
							}
							else
							{
								Quest quest = QuestUtility.GenerateQuestAndMakeAvailable(script, testSlate);
								Log.Message(quest.name + " (" + localPoints + " points)\n--------------\n" + quest.description + "\n--------------");
								Find.QuestManager.Remove(quest);
							}
						}
						if (num != 0)
						{
							Messages.Message("Dev: Generated only " + (count - num) + " quests.", MessageTypeDefOf.RejectInput, historical: false);
						}
					}));
				}
				Find.WindowStack.Add(new Dialog_DebugOptionListLister(list2));
			};
			list.Add(new DebugMenuOption("*Natural random", DebugMenuOptionMode.Action, delegate
			{
				generate(null);
			}));
			foreach (QuestScriptDef item2 in DefDatabase<QuestScriptDef>.AllDefs.Where((QuestScriptDef x) => x.IsRootAny))
			{
				QuestScriptDef localRuleDef = item2;
				string defName = localRuleDef.defName;
				list.Add(new DebugMenuOption(defName, DebugMenuOptionMode.Action, delegate
				{
					generate(localRuleDef);
				}));
			}
			Find.WindowStack.Add(new Dialog_DebugOptionListLister(list.OrderBy((DebugMenuOption op) => op.label)));






			//localDef = DefDatabase<QuestScriptDef>.GetNamed("HuntingLodgeOppSW", false);


			return true;
        }

        

    }

}
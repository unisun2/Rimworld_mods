using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using HarmonyLib;
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
            //parms.target = World;
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

			AccessTools.Method(typeof(Verse.DebugActionsQuests), "GenerateQuests").Invoke(this, new object[] {1, false });


			//localDef = DefDatabase<QuestScriptDef>.GetNamed("HuntingLodgeOppSW", false);


			return true;
        }

        

    }

    
    public class IncidentWorker_GetOutMod_quest : IncidentWorker
    {

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            return true;
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            

            // give quest - 1.1 ver!

            AccessTools.Method(typeof(Verse.DebugActionsQuests), "GenerateQuests").Invoke(this, new object[] { 8, false });

            return true;
        }



    }
}
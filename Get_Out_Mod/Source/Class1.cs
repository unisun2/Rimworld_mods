using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace WG_GOM
{
    public class IncidentWorker_GetOutMod : IncidentWorker
    {
        //IncidentParms coreIncidentParms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.Misc, Find.World);
        /*
        IncidentDef q1 = IncidentDefOf.Quest_TradeRequest;
        IncidentDef q2 = IncidentDefOf.Bandit;
        IncidentDef q3 = IncidentDefOf.Quest_TradeRequest;
        IncidentDef q4 = IncidentDefOf.Quest_TradeRequest;
        IncidentDef q5 = IncidentDefOf.Quest_TradeRequest;

        IncidentWorker_QuestTradeRequest q1 = new IncidentWorker_QuestTradeRequest();
        IncidentWorker_QuestBanditCamp q2 = new IncidentWorker_QuestBanditCamp();
        IncidentWorker_QuestItemStash q3 = new IncidentWorker_QuestItemStash();
        IncidentWorker_QuestDownedRefugee q4 = new IncidentWorker_QuestDownedRefugee();
        IncidentWorker_QuestPrisonerRescue q5 = new IncidentWorker_QuestPrisonerRescue();
        */

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
            // vanila

            IncidentDef localDef = DefDatabase<IncidentDef>.GetNamed("Quest_TradeRequest", false);
            Try_exe_this(localDef, parms);

            localDef = DefDatabase<IncidentDef>.GetNamed("Quest_BanditCamp", false);
            Try_exe_this(localDef, parms);

            localDef = DefDatabase<IncidentDef>.GetNamed("Quest_ItemStash", false);
            Try_exe_this(localDef, parms);

            localDef = DefDatabase<IncidentDef>.GetNamed("Quest_DownedRefugee", false);
            Try_exe_this(localDef, parms);

            localDef = DefDatabase<IncidentDef>.GetNamed("Quest_BanditCamp", false);
            Try_exe_this(localDef, parms);

            // Go Explore!

            localDef = DefDatabase<IncidentDef>.GetNamed("LostCityLGE", false);
            Try_exe_this(localDef, parms);

            localDef = DefDatabase<IncidentDef>.GetNamed("AmbrosiaAnimalsLGE", false);
            Try_exe_this(localDef, parms);

            localDef = DefDatabase<IncidentDef>.GetNamed("PrisonCampLGE", false);
            Try_exe_this(localDef, parms);

            localDef = DefDatabase<IncidentDef>.GetNamed("NewSettlementLGE", false);
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

            localDef = DefDatabase<IncidentDef>.GetNamed("PsychicEmitterActivationSW", false);
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


            return true;
        }

        

    }

}
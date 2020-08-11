using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using Verse.Sound;

namespace WG_BBM
{
    public class IncidentWorker_BigRaidandFriend : IncidentWorker
    {
        static Faction friend;
        static Faction enemyf;

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            friend = null;
            enemyf = null;
            Map map = Find.CurrentMap;

            //Log.Message("CanFireNowSub " + wgbbm_settings.Getenemypoints() + " " + wgbbm_settings.Getfriendspoints1() + " " + wgbbm_settings.Getfriendspoints2());

            List<Faction> factionlist = new List<Faction>();

            foreach (Faction f in Find.FactionManager.AllFactions)
                factionlist.Add(f);

            factionlist.Shuffle();

            foreach (Faction f in factionlist)  // search enemy
            {
                if (!f.IsPlayer && !f.defeated && !f.def.hidden && f.def.allowedArrivalTemperatureRange.Includes(map.mapTemperature.OutdoorTemp))
                {
                    if (f.HostileTo(Faction.OfPlayer))
                    {
                        enemyf = f;
                        //Log.Message("f : " + enemyf.Name + " is enemy!");

                        foreach (Faction ff in factionlist) // search friends
                        {
                            if (f != ff && !ff.IsPlayer && !f.defeated && !f.def.hidden && ff.def.allowedArrivalTemperatureRange.Includes(map.mapTemperature.OutdoorTemp))
                            {
                                if (ff.HostileTo(f) && ff.PlayerRelationKind == FactionRelationKind.Ally)
                                {
                                    friend = ff;
                                    //Log.Message("ff : " + ff.Name + " is friend!");
                                    return true;
                                        
                                }

                            }


                        }
                    }

                }

            }
            //Log.Message("failed. f : " + friendslist.Count);
            return false;

        }

        public void Try_exe_this(IncidentDef localDef, IncidentParms parms)
        {
            if (localDef == null) return;

            if(localDef.Worker.CanFireNow(parms))
                localDef.Worker.TryExecute(parms);

        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            // start raid!

           // Log.Message((parms.points).ToString());

            IncidentParms parms1 = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, Find.CurrentMap);

            parms1.points = parms.points * WG_BBM_setting.enemypersent;

            //Log.Message((parms1.points).ToString());


            IncidentParms parms2 = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, Find.CurrentMap);

            parms2.points = parms.points * WG_BBM_setting.friendpersent;

            //Log.Message((parms2.points).ToString());


            //Log.Message((parms3.points).ToString());

            parms1.faction = enemyf;
            parms2.faction = friend;

            IncidentDef incidentDef1 = IncidentDefOf.RaidEnemy;
            IncidentDef incidentDef2 = IncidentDefOf.RaidFriendly;

            parms1.raidStrategy = RaidStrategyDefOf.ImmediateAttack;
            parms2.raidStrategy = RaidStrategyDefOf.ImmediateAttack;
            parms1.raidArrivalMode = PawnsArrivalModeDefOf.EdgeWalkIn;
            parms2.raidArrivalMode = PawnsArrivalModeDefOf.EdgeWalkIn;

            incidentDef1.Worker.TryExecute(parms1);
            incidentDef2.Worker.TryExecute(parms2);
            
            /*
            foreach (Faction f in friendslist)
            {
                Log.Message("friend : " + f.Name);
            }
            
            Log.Message("enemy : " + enemyf);
            */

            return true;
        }



    }

}
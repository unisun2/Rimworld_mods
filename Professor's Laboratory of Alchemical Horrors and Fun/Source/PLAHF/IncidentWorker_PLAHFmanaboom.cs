using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;
using UnityEngine;

namespace PLAHF
{
    class IncidentWorker_PLAHFmanaboom : IncidentWorker
    {
        
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            return true;
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Faction faction = Find.FactionManager.FirstFactionOfDef(FactionDef.Named("PLAHF_faction"));
            Map map = (Map)parms.target;
            //List<TargetInfo> list = new List<TargetInfo>();
            float shrapnelDirection = Rand.Range(0f, 360f);
            Thing lookhere;

            
                IntVec3 intVec;
                if (!CellFinderLoose.TryFindSkyfallerCell(RimWorld.ThingDefOf.DropPodIncoming, map, out intVec, 14, default(IntVec3), -1, false, true, true, true, true, false, null))
                {
                return false;
                }
                Building_TacticalManaBomb building_tacticalmanabomb = (Building_TacticalManaBomb)ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("Building_TacticalManaBomb", true));
                building_tacticalmanabomb.SetFaction(faction, null);
                building_tacticalmanabomb.spawnpoints = (float)(parms.points * 0.8);
                lookhere = building_tacticalmanabomb;

                Skyfaller skyfaller = SkyfallerMaker.MakeSkyfaller(RimWorld.ThingDefOf.DropPodIncoming, building_tacticalmanabomb);
                skyfaller.shrapnelDirection = shrapnelDirection;
                GenSpawn.Spawn(skyfaller, intVec, map, WipeMode.Vanish);
            


           

            Find.LetterStack.ReceiveLetter("LetterLabelPLAHFTMBAttack".Translate(), "LetterPLAHFTMBAttack".Translate(), LetterDefOf.ThreatBig,lookhere, null, null);
            

            Find.TickManager.slower.SignalForceNormalSpeedShort();
            Find.StoryWatcher.statsRecord.numRaidsEnemy++;
            return true;
        }
    }


}

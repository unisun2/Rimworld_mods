using System.Linq;
using RimWorld;
using Verse;

namespace WG_BBM;

public class IncidentWorker_BigRaidandFriend : IncidentWorker
{
    private static Faction friend;

    private static Faction enemyFaction;

    protected override bool CanFireNowSub(IncidentParms parms)
    {
        friend = null;
        enemyFaction = null;
        var currentMap = Find.CurrentMap;
        var allValidFactions = Find.FactionManager.AllFactions.Where(faction =>
            !faction.IsPlayer && !faction.defeated && !faction.temporary && !faction.Hidden &&
            !faction.def.allowedArrivalTemperatureRange.Includes(currentMap.mapTemperature.OutdoorTemp));

        enemyFaction = allValidFactions.FirstOrDefault(faction => faction.HostileTo(Faction.OfPlayer));
        if (enemyFaction == null)
        {
            return false;
        }

        friend = allValidFactions.FirstOrDefault(friends =>
            friends.HostileTo(enemyFaction) && friends.PlayerRelationKind == FactionRelationKind.Ally);
        if (friend == null)
        {
            return false;
        }

        return true;
    }

    public void Try_exe_this(IncidentDef localDef, IncidentParms parms)
    {
        if (localDef != null && localDef.Worker.CanFireNow(parms))
        {
            localDef.Worker.TryExecute(parms);
        }
    }

    protected override bool TryExecuteWorker(IncidentParms parms)
    {
        var incidentParms = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, Find.CurrentMap);
        incidentParms.points = parms.points * WG_BBM_setting.enemypersent;
        var incidentParms2 = StorytellerUtility.DefaultParmsNow(IncidentCategoryDefOf.ThreatBig, Find.CurrentMap);
        incidentParms2.points = parms.points * WG_BBM_setting.friendpersent;
        incidentParms.faction = enemyFaction;
        incidentParms2.faction = friend;
        var raidEnemy = IncidentDefOf.RaidEnemy;
        var raidFriendly = IncidentDefOf.RaidFriendly;
        incidentParms.raidStrategy = RaidStrategyDefOf.ImmediateAttack;
        incidentParms2.raidStrategy = RaidStrategyDefOf.ImmediateAttack;
        incidentParms.raidArrivalMode = PawnsArrivalModeDefOf.EdgeWalkIn;
        incidentParms2.raidArrivalMode = PawnsArrivalModeDefOf.EdgeWalkIn;
        raidEnemy.Worker.TryExecute(incidentParms);
        raidFriendly.Worker.TryExecute(incidentParms2);
        return true;
    }
}
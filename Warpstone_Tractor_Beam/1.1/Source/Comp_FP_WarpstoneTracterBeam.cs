using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace FP_Warp_TB
{
    class Comp_FP_WarpstoneTracterBeam : ThingComp
    {
        protected float daysWorkingSinceLastFinding;

	protected CompForbiddable forbiddable;

	public CompProperties_FP_WarpstoneTracterBeam Props => (CompProperties_FP_WarpstoneTracterBeam)props;

	public bool CanUseNow
	{
		get
		{
			if (!parent.Spawned)
			{
				return false;
			}
			if (RoofUtility.IsAnyCellUnderRoof(parent))
			{
				return false;
			}
			if (forbiddable != null && forbiddable.Forbidden)
			{
				return false;
			}
			return parent.Faction == Faction.OfPlayer;
		}
	}

	public override void PostExposeData()
	{
		base.PostExposeData();
		Scribe_Values.Look(ref daysWorkingSinceLastFinding, "daysWorkingSinceLastFinding", 0f);
	}

	public override void PostSpawnSetup(bool respawningAfterLoad)
	{
		base.PostSpawnSetup(respawningAfterLoad);
		forbiddable = parent.GetComp<CompForbiddable>();
	}

	public void Used(Pawn worker)
	{
		if (!CanUseNow)
		{
			Log.Error("Used while CanUseNow is false.");
		}
		float num = 1f;
		if (Props.scanSpeedStat != null)
		{
			num = worker.GetStatValue(Props.scanSpeedStat);
		}
		daysWorkingSinceLastFinding += num / 60000f;
		if (TickDoesFind(num))
		{
			DoFind(worker);
			daysWorkingSinceLastFinding = 0f;
		}
	}

	protected virtual bool TickDoesFind(float scanSpeed)
	{
		if (Find.TickManager.TicksGame % 59 == 0 && (Rand.MTBEventOccurs(Props.scanFindMtbDays / scanSpeed, 60000f, 59f) || (Props.scanFindGuaranteedDays > 0f && daysWorkingSinceLastFinding >= Props.scanFindGuaranteedDays)))
		{
			return true;
		}
		return false;
	}

	protected abstract void DoFind(Pawn worker);

	public override IEnumerable<Gizmo> CompGetGizmosExtra()
	{
		if (Prefs.DevMode)
		{
			Command_Action command_Action = new Command_Action();
			command_Action.defaultLabel = "Dev: Find now";
			command_Action.action = delegate
			{
				DoFind(PawnsFinder.AllMaps_FreeColonists.RandomElement());
			};
			yield return command_Action;
		}
	}
    }

}

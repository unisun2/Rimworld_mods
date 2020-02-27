using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;

namespace FP_RSLUM
{
    class StatPart_CON_ComfyTemperature_max : StatPart
	{
		public override void TransformValue(StatRequest req, ref float val)
		{
			if (req.HasThing)
			{
				Pawn pawn = req.Thing as Pawn;
				PawnLvComp pawnlvcomp = pawn.TryGetComp<PawnLvComp>();
				if (pawnlvcomp != null)
				{
					val += (float)(0.3 * Math.Max(0, pawnlvcomp.CON));
				}
			}
		}

		public override string ExplanationPart(StatRequest req)
		{
			if (req.HasThing)
			{
				Pawn pawn = req.Thing as Pawn;
				if (pawn != null)
				{
					PawnLvComp pawnlvcomp = pawn.TryGetComp<PawnLvComp>();
					if (pawnlvcomp != null)
						return "StatsReport_STAT_CON".Translate() + ": +" + (0.3f * pawnlvcomp.CON).ToStringTemperature();
				}
			}
			return "";
		}
	}
}

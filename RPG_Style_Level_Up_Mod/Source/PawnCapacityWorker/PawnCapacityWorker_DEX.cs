using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;

namespace FP_RSLUM
{
	public class PawnCapacityWorker_DEX : PawnCapacityWorker
	{
		public override float CalculateCapacityLevel(HediffSet diffSet, List<PawnCapacityUtility.CapacityImpactor> impactors = null)
		{
			Pawn pawn = diffSet.pawn;
			PawnLvComp pawnlvcomp = pawn.TryGetComp<PawnLvComp>();
			return (float)(1.00f + (0.01 * pawnlvcomp.DEX));
		}

		public override bool CanHaveCapacity(BodyDef body)
		{
			return true;
		}
	}
}

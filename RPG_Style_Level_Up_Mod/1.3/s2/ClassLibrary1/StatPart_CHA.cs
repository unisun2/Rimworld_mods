using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;
namespace FP_RSLUM
{
    class StatPart_CHA : StatPart
    {
        public float weight;
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing)
            {
                Pawn pawn = req.Thing as Pawn;
                PawnLvComp pawnlvcomp = pawn.TryGetComp<PawnLvComp>();
                if (pawnlvcomp != null)
                {
                    val *= (1.00f + (float)(0.01 * pawnlvcomp.CHA) * weight);
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
                        return "StatsReport_STAT_CHA".Translate() + ": x" + (1.00f + (float)(0.01 * pawnlvcomp.CHA) * weight).ToStringPercent();
                }
            }
            return null;
        }
    }
}

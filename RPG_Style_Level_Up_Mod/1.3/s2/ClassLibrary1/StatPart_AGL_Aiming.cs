using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;
namespace FP_RSLUM
{
    class StatPart_AGL_Aiming : StatPart
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            if (req.HasThing)
            {
                Pawn pawn = req.Thing as Pawn;
                PawnLvComp pawnlvcomp = pawn.TryGetComp<PawnLvComp>();
                if (pawnlvcomp != null)
                {
                    val *= (float)Math.Max((1 - (0.003 * pawnlvcomp.AGL)), 0.5f);
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
                        return "StatsReport_STAT_AGL_Aiming".Translate() + ": x" + ((float)Math.Max((1 - (0.003 * pawnlvcomp.AGL)), 0.5f)).ToStringPercent();
                }
            }
            return "";
        }
    }
}

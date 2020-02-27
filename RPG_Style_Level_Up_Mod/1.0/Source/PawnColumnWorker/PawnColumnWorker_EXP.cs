using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;

namespace FP_RSLUM
{
    class PawnColumnWorker_EXP : PawnColumnWorker
    {
        private static NumericStringComparer comparer = new NumericStringComparer();

        protected virtual int Width
        {
            get
            {
                return this.def.width;
            }
        }

        public override void DoCell(Rect rect, Pawn pawn, PawnTable table)
        {
            Rect rect2 = new Rect(rect.x, rect.y, rect.width - 30, Mathf.Min(rect.height, 30f));
            String textFor = this.GetTextFor(pawn);
            if (textFor != null)
            {
                Text.Font = GameFont.Small;
                Text.Anchor = TextAnchor.MiddleCenter;
                Text.WordWrap = false;
                Widgets.Label(rect2, textFor);
                Text.WordWrap = true;
                Text.Anchor = TextAnchor.UpperLeft;
                String tip = this.GetTip(pawn);
                if (!tip.NullOrEmpty())
                {
                    TooltipHandler.TipRegion(rect2, tip);
                }
            }
        }

        protected String GetTip(Pawn pawn)
        {
            return "PawnColumnWorker_EXP_Tip_Desc".Translate();
        }
        protected string GetTextFor(Pawn pawn)
        {
            PawnLvComp pawnlvcomp = pawn.TryGetComp<PawnLvComp>();
            if (pawnlvcomp != null)
            {
                return ((int)(pawnlvcomp.exp / 100)).ToString();
            }
            else
            {
                // WHAT?
                Log.Message("error in PawnColumnWorker_EXP GetTextFor.");
                return "err";
            }
        }

        public override int Compare(Pawn a, Pawn b)
        //    => a.ageTracker.AgeBiologicalYearsFloat.CompareTo(b.ageTracker.AgeBiologicalYearsFloat);
        {
            PawnLvComp pawnlvcompa = a.TryGetComp<PawnLvComp>();
            PawnLvComp pawnlvcompb = b.TryGetComp<PawnLvComp>();
            if (pawnlvcompa != null && pawnlvcompb != null)
            {
                return pawnlvcompa.exp.CompareTo(pawnlvcompb.exp);
            }
            else
            {
                // WHAT?
                Log.Message("error in PawnColumnWorker_EXP Compare. no pawnlvcomp.");
                return 0;
            }
        }

        public override int GetMinWidth(PawnTable table)
            => base.GetMinWidth(table) + 20;
    }
}

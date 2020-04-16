using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;

namespace FP_RSLUM
{
    class PawnColumnWorker_Level : PawnColumnWorker
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
            PawnLvComp pawnlvcomp = pawn.TryGetComp<PawnLvComp>();
            if (DebugSettings.godMode && pawnlvcomp != null)
            {
                this.DoStatRerollButton(new Rect(rect.x, rect.yMin, 30f, 30f), pawn);
                Rect rect2 = new Rect(rect.x + 50, rect.y, rect.width - 50, Mathf.Min(rect.height, 30f));
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
            else if(pawnlvcomp != null)
            {
                Rect rect2 = new Rect(rect.x, rect.y, rect.width, Mathf.Min(rect.height, 30f));
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
            
        }

        protected String GetTip(Pawn pawn)
        {
            return "PawnColumnWorker_Level_Tip_Desc".Translate();
        }

        //protected override string GetTextFor(Pawn pawn)
        //    => Math.Round(pawn.ageTracker.AgeBiologicalYearsFloat, 2).ToString("0.00");

        protected string GetTextFor(Pawn pawn)
        {
            PawnLvComp pawnlvcomp = pawn.TryGetComp<PawnLvComp>();
            if(pawnlvcomp != null)
            {
                return pawnlvcomp.level.ToString();
            }
            else
            {
                // WHAT?
                Log.Message("error in PawnColumnWorker_Level GetTextFor. no pawnlvcomp.");
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
                return pawnlvcompa.level.CompareTo(pawnlvcompb.level);
            }
            else
            {
                // WHAT?
                Log.Message("error in PawnColumnWorker_Level Compare. no pawnlvcomp.");
                return 0;
            }
        }

        public override int GetMinWidth(PawnTable table)
        {
            return base.GetMinWidth(table) + 50;
        }


        public void DoStatRerollButton(Rect rect, Pawn pawn)
        {
            TooltipHandler.TipRegion(rect, Translator.Translate("LvTab_reroll"));
            if (Widgets.ButtonImage(rect, harmony_patches.LVUP_rerollIMG, Color.white, GenUI.SubtleMouseoverColor))
            {
                PawnLvComp pawnlvcomp = pawn.TryGetComp<PawnLvComp>();
                if (pawnlvcomp != null)
                {
                    pawnlvcomp.StatPoint = pawnlvcomp.level;
                    if (FP_RSLUM_setting.FlatStartingStat)
                    {
                        pawnlvcomp.STR = 0;
                        pawnlvcomp.DEX = 0;
                        pawnlvcomp.AGL = 0;
                        pawnlvcomp.CON = 0;
                        pawnlvcomp.INT = 0;
                        pawnlvcomp.CHA = 0;
                    }
                    else
                    {
                        pawnlvcomp.STR = Rand.Range(FP_RSLUM_setting.Startingstat_min, FP_RSLUM_setting.Startingstat_max);
                        pawnlvcomp.DEX = Rand.Range(FP_RSLUM_setting.Startingstat_min, FP_RSLUM_setting.Startingstat_max);
                        pawnlvcomp.AGL = Rand.Range(FP_RSLUM_setting.Startingstat_min, FP_RSLUM_setting.Startingstat_max);
                        pawnlvcomp.CON = Rand.Range(FP_RSLUM_setting.Startingstat_min, FP_RSLUM_setting.Startingstat_max);
                        pawnlvcomp.INT = Rand.Range(FP_RSLUM_setting.Startingstat_min, FP_RSLUM_setting.Startingstat_max);
                        pawnlvcomp.CHA = Rand.Range(FP_RSLUM_setting.Startingstat_min, FP_RSLUM_setting.Startingstat_max);
                    }
                    
                }
            }
        }
    }
}

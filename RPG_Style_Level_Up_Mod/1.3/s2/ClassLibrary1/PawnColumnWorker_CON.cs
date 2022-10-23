using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;
using static Verse.Widgets;

namespace FP_RSLUM
{
    class PawnColumnWorker_CON : PawnColumnWorker
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
            if (pawnlvcomp != null)
            {
                if (pawnlvcomp.StatPoint > 0)
                {
                    this.DoStatUpButton(new Rect(rect.x, rect.yMin, 30f, 30f), pawn);
                    Rect rect2 = new Rect(rect.x + 35, rect.y, rect.width - 35, Mathf.Min(rect.height, 30f));
                    String textFor = this.GetTextFor(pawn);
                    if (textFor != null)
                    {
                        Text.Font = GameFont.Small;
                        Text.Anchor = TextAnchor.MiddleLeft;
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
                else
                {
                    Vector2 topLeft = new Vector2(rect.x, rect.yMin);
                    statCheckbox(topLeft, ref pawnlvcomp.CONauto, 30f, def.paintable, pawnlvcomp);
                    Rect rect2 = new Rect(rect.x + 35, rect.y, rect.width - 35, Mathf.Min(rect.height, 30f));
                    String textFor = this.GetTextFor(pawn);
                    if (textFor != null)
                    {
                        Text.Font = GameFont.Small;
                        Text.Anchor = TextAnchor.MiddleLeft;
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
        }
        public void statCheckbox(Vector2 topLeft, ref bool checkOn, float size, bool paintable, PawnLvComp pawnlvcomp)
        {
            Rect rect = new Rect(topLeft.x, topLeft.y, size, size);
            GUI.DrawTexture(image: (!checkOn) ? (CheckboxOffTex) : (CheckboxOnTex), position: new Rect(topLeft.x, topLeft.y, size, size));
            DraggableResult draggableResult = ButtonInvisibleDraggable(rect);
            MouseoverSounds.DoRegion(rect);
            bool flag = false;
            if (draggableResult == DraggableResult.Pressed)
            {
                checkOn = !checkOn;
                flag = true;
                pawnlvcomp.AGLauto = false;
                pawnlvcomp.CHAauto = false;
                //pawnlvcomp.CONauto = false;
                pawnlvcomp.DEXauto = false;
                pawnlvcomp.INTauto = false;
                pawnlvcomp.STRauto = false;
            }
            if (flag)
            {
                if (checkOn)
                {
                    SoundDefOf.Checkbox_TurnedOn.PlayOneShotOnCamera();
                }
                else
                {
                    SoundDefOf.Checkbox_TurnedOff.PlayOneShotOnCamera();
                }
            }
        }

        protected String GetTip(Pawn pawn)
        {
            return "PawnColumnWorker_CON_Tip_Desc".Translate();
        }



        //protected override CONing GetTextFor(Pawn pawn)
        //    => Math.Round(pawn.ageTracker.AgeBiologicalYearsFloat, 2).ToCONing("0.00");

        protected String GetTextFor(Pawn pawn)
        {
            PawnLvComp pawnlvcomp = pawn.TryGetComp<PawnLvComp>();
            if (pawnlvcomp != null)
            {
                return pawnlvcomp.CON.ToString();
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
                return pawnlvcompa.CON.CompareTo(pawnlvcompb.CON);
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

        public void DoStatUpButton(Rect rect, Pawn pawn)
        {
            TooltipHandler.TipRegion(rect, Translator.Translate("LvTab_Distribute"));
            if (Widgets.ButtonImage(rect, harmony_patches.DistributeIMG, Color.white, GenUI.SubtleMouseoverColor))
            {
                PawnLvComp pawnlvcomp = pawn.TryGetComp<PawnLvComp>();
                if (pawnlvcomp != null)
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (pawnlvcomp.StatPoint > 0)
                            {
                                pawnlvcomp.StatPoint -= 1;
                                pawnlvcomp.CON += 1;
                            }
                        }
                    }
                    else if (Input.GetKey(KeyCode.LeftControl))
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            if (pawnlvcomp.StatPoint > 0)
                            {
                                pawnlvcomp.StatPoint -= 1;
                                pawnlvcomp.CON += 1;
                            }
                        }
                    }
                    else if (Input.GetKey(KeyCode.LeftAlt))
                    {
                        if (Input.GetKey(KeyCode.LeftShift))
                        {
                            int tempv = Math.Min(pawnlvcomp.CON - FP_RSLUM_setting.Startingstat_min, 10);
                            if (pawnlvcomp.CON > FP_RSLUM_setting.Startingstat_min)
                            {
                                pawnlvcomp.StatPoint += tempv;
                                pawnlvcomp.CON -= tempv;
                            }
                        }
                        else if (Input.GetKey(KeyCode.LeftControl))
                        {
                            int tempv = Math.Min(pawnlvcomp.CON - FP_RSLUM_setting.Startingstat_min, 100);
                            if (pawnlvcomp.CON > FP_RSLUM_setting.Startingstat_min)
                            {
                                pawnlvcomp.StatPoint += tempv;
                                pawnlvcomp.CON -= tempv;
                            }
                        }
                        else
                        {
                            if (pawnlvcomp.CON > FP_RSLUM_setting.Startingstat_min)
                            {
                                pawnlvcomp.StatPoint += 1;
                                pawnlvcomp.CON -= 1;
                            }
                        }
                       
                    }
                    else
                    {
                        if (pawnlvcomp.StatPoint > 0)
                        {
                            pawnlvcomp.StatPoint -= 1;
                            pawnlvcomp.CON += 1;
                        }
                    }
                }
            }
        }
    }
}

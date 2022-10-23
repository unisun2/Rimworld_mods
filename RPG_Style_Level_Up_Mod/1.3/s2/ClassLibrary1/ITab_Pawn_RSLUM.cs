using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using UnityEngine;

namespace FP_RSLUM
{
    class ITab_Pawn_RSLUM : ITab
    {
        public override bool IsVisible
        {
            get
            {
                return true;
            }
        }

        public ITab_Pawn_RSLUM()
        {
            labelKey = "TabRSLUM".Translate();
            tutorTag = "LevelUp";
        }

        protected override void FillTab()
        {
            Rect rect = new Rect(0f, 0f, size.x, size.y).ContractedBy(17f);
            rect.yMin += 10f;

            PawnLvComp pawnLvComp = base.SelPawn.GetComp<PawnLvComp>();

            Text.Font = GameFont.Small;

            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.Begin(rect);

            listing_Standard.Label("PawnColumnWorker_Level_Tip_Desc".Translate() + " : " + pawnLvComp.level);
            listing_Standard.GapLine();
            listing_Standard.Label("StatsReport_STAT_STR".Translate() + " : " + pawnLvComp.STR);
            listing_Standard.Label("StatsReport_STAT_DEX".Translate() + " : " + pawnLvComp.DEX);
            listing_Standard.Label("StatsReport_STAT_AGL".Translate() + " : " + pawnLvComp.AGL);
            listing_Standard.Label("StatsReport_STAT_CON".Translate() + " : " + pawnLvComp.CON);
            listing_Standard.Label("StatsReport_STAT_INT".Translate() + " : " + pawnLvComp.INT);
            listing_Standard.Label("StatsReport_STAT_CHA".Translate() + " : " + pawnLvComp.CHA);
            
            listing_Standard.End();

        }

        protected override void UpdateSize()
        {
            base.UpdateSize();
            size = new Vector2(300f, 300f);
        }
    }
}

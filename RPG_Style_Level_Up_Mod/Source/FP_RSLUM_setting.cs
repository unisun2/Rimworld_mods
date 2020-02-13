using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace FP_RSLUM
{
    class FP_RSLUM_setting : ModSettings
    {

        public int ColonistPercent = 75;
        public int AnimalEXPPerTick = 1;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<int>(ref ColonistPercent, "ColonistPercent", 75);
            Scribe_Values.Look<int>(ref AnimalEXPPerTick, "AnimalEXPPerTick", 50);
        }
        
        public void DoSettingsWindowContents(Rect canvas)
        {
            Listing_Standard _Listing_Standard = new Listing_Standard();
            _Listing_Standard.ColumnWidth = 250f;
            _Listing_Standard.Begin(canvas);
            //listing_Standard.set_ColumnWidth(rect.get_width() - 4f);

            _Listing_Standard.Label("Setting will only apply after a Game Restart.");
            _Listing_Standard.Gap(12f);

            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label("Residents will get this % of their skill experience. : " + ColonistPercent + "%");
            ColonistPercent = (int)(1000 * _Listing_Standard.Slider((float)(ColonistPercent) / 100f, 0.01f, 0.99f));

            _Listing_Standard.Label("Sandbags are 65%");



            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label("Stuff Cost: " + StuffCost);
            _Listing_Standard.IntAdjuster(ref StuffCost, 1, 1);
            _Listing_Standard.Label("Wall is 5 stuff");

            _Listing_Standard.End();
        }
    }

}
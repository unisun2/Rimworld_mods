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

        public static int ColonistPercent = 75;
        public static int AnimalEXPPerTick = 1;
		public static bool FlatStartingStat = false;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<int>(ref ColonistPercent, "ColonistPercent", 80);
            Scribe_Values.Look<int>(ref AnimalEXPPerTick, "AnimalEXPPerTick", 500);
            Scribe_Values.Look<bool>(ref FlatStartingStat, "FlatStartingStat", false, true);
        }
        
        public void DoSettingsWindowContents(Rect canvas)
        {
            Listing_Standard _Listing_Standard = new Listing_Standard();
            _Listing_Standard.ColumnWidth = canvas.width;
            _Listing_Standard.Begin(canvas);
            //listing_Standard.set_ColumnWidth(rect.get_width() - 4f);

            _Listing_Standard.CheckboxLabeled(Translator.Translate("FP_RSLUM_setting_FlatStartingStat"), ref FlatStartingStat, null);
            _Listing_Standard.Gap(12f);

            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label(Translator.Translate("FP_RSLUM_setting_ColonistPercent") + " : " + ColonistPercent + "%"); // Residents will get this percent of their skill experience. default = 80

            ColonistPercent = (int)_Listing_Standard.Slider((float)ColonistPercent, 1f, 200f);

            _Listing_Standard.Label(":)");

			_Listing_Standard.GapLine(12f);
            _Listing_Standard.Label(Translator.Translate("FP_RSLUM_setting_AnimalEXPPerTick") + " : " + AnimalEXPPerTick);    // Animals will get this amount of EXP every 10 seconds. default = 500
            AnimalEXPPerTick = (int)_Listing_Standard.Slider((float)AnimalEXPPerTick, 1f, 1200f);


            _Listing_Standard.End();
        }
    }

}
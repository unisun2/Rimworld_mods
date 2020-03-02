using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace FPSRT
{
    class FP_SelfReloadTrap_setting : ModSettings
    {
        public static float trapdamage = 1.0f;
        public static float armorpenetrate = 0.3f;
        public static int traparmingtime = 10;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<float>(ref trapdamage, "FP_SelfReloadTrap_trapdamage", 0.9);
            Scribe_Values.Look<float>(ref armorpenetrate, "FP_SelfReloadTrap_armorpenetrate", 0.3);
            Scribe_Values.Look<int>(ref traparmingtime, "FP_SelfReloadTrap_traparmingtime", 10);
        }

        public void DoSettingsWindowContents(Rect canvas)
        {
            Listing_Standard _Listing_Standard = new Listing_Standard();
            _Listing_Standard.ColumnWidth = canvas.width;
            _Listing_Standard.Begin(canvas);
            //listing_Standard.set_ColumnWidth(rect.get_width() - 4f);

            _Listing_Standard.CheckboxLabeled(Translator.Translate("FP_SelfReloadTrap_trapdamage"), ref trapdamage, null);
            _Listing_Standard.Gap(12f);

            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label(Translator.Translate("FP_SelfReloadTrap_trapdamage") + " : " + trapdamage + " %"); // Residents will get this percent of their skill experience. default = 80

            trapdamage = (float)_Listing_Standard.Slider((float)trapdamage, 0.2f, 3.0f);

            //_Listing_Standard.Label(":)");

            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label(Translator.Translate("FP_RSLUM_setting_AnimalEXPPerTick") + " : " + AnimalEXPPerTick);    // Animals will get this amount of EXP every 10 seconds. default = 500
            armorpenetrate = (int)_Listing_Standard.Slider((float)armorpenetrate, 1f, 1200f);

            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label(Translator.Translate("traparmingtime") + " : " + traparmingtime + " sec"); // Residents will get this percent of their skill experience. default = 80

            Startingstat_min = (int)_Listing_Standard.Slider((float)Startingstat_min, 5, 100);

            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label(Translator.Translate("FP_RSLUM_setting_Startingstat_max") + " : " + Startingstat_max); // Residents will get this percent of their skill experience. default = 80

            Startingstat_max = (int)_Listing_Standard.Slider((float)Startingstat_max, -50f, 200f);


            _Listing_Standard.End();
        }
    }
}

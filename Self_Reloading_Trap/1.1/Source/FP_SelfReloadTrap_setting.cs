using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using RimWorld;

namespace FPSRT
{
    class FP_SelfReloadTrap_setting : ModSettings
    {
        public static int trapdamage = 90;
        public static int armorpenetrate = 30;
        public static int traparmingtime = 10;
        public static int bulidingcost = 300;

        
        /*FP_SelfReloadTrap_setting()
        {
            ThingDef _mytrap = ThingDef.Named("Building_FPSRT");
            _mytrap.costStuffCount = bulidingcost;
        }*/

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<int>(ref trapdamage, "FP_SelfReloadTrap_trapdamage", 90);
            Scribe_Values.Look<int>(ref armorpenetrate, "FP_SelfReloadTrap_armorpenetrate", 30);
            Scribe_Values.Look<int>(ref traparmingtime, "FP_SelfReloadTrap_traparmingtime", 10);
            Scribe_Values.Look<int>(ref bulidingcost, "FP_SelfReloadTrap_bulidingcost", 300);
        }

        public void DoSettingsWindowContents(Rect canvas)
        {
            Listing_Standard _Listing_Standard = new Listing_Standard();
            _Listing_Standard.ColumnWidth = canvas.width;
            _Listing_Standard.Begin(canvas);
            //listing_Standard.set_ColumnWidth(rect.get_width() - 4f);

            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label(Translator.Translate("FP_SelfReloadTrap_trapdamage") + " : " + trapdamage + " %"); // Residents will get this percent of their skill experience. default = 80

            trapdamage = (int)_Listing_Standard.Slider((float)trapdamage, 20f, 300f);

            //_Listing_Standard.Label(":)");

            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label(Translator.Translate("FP_SelfReloadTrap_armorpenetrate") + " : " + armorpenetrate + " %");    // Animals will get this amount of EXP every 10 seconds. default = 500
            armorpenetrate = (int)_Listing_Standard.Slider((float)armorpenetrate, 1f, 200);

            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label(Translator.Translate("traparmingtime") + " : " + traparmingtime + " sec"); // Residents will get this percent of their skill experience. default = 80

            traparmingtime = (int)_Listing_Standard.Slider((float)traparmingtime, 5, 100);

            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label(Translator.Translate("* need restart"));
            _Listing_Standard.Label(Translator.Translate("bulidingcost") + " : " + bulidingcost + ""); // Residents will get this percent of their skill experience. default = 80

            bulidingcost = (int)_Listing_Standard.Slider((float)bulidingcost, 150, 2000);


            _Listing_Standard.End();
        }
    }
}

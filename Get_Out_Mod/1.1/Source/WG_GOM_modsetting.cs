using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using UnityEngine;


namespace WG_GOM
{
    class WG_GOM_mod : Verse.Mod
    {
        public static WG_GOM_setting Settings;

        public WG_GOM_mod(ModContentPack content)
            : base(content)
        {
            WG_GOM_mod.Settings = GetSettings<WG_GOM_setting>();
        }

        public override string SettingsCategory()
        {
            return "Get_Out_Mod";
            //return base.SettingsCategory();
        }


        public override void DoSettingsWindowContents(Rect inRect)
        {
            Settings.DoSettingsWindowContents(inRect);
            //base.DoSettingsWindowContents(inRect);
        }

    }
    class WG_GOM_setting : ModSettings
    {
        public static int questnum = 1;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<int>(ref questnum, "WG_GOM_questnum", 1);

        }

        public void DoSettingsWindowContents(Rect canvas)
        {
            Listing_Standard _Listing_Standard = new Listing_Standard();
            _Listing_Standard.ColumnWidth = canvas.width;
            _Listing_Standard.Begin(canvas);
            //listing_Standard.set_ColumnWidth(rect.get_width() - 4f);


            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label(Translator.Translate("Number of Extra quest") + " : " + questnum); // Residents will get this percent of their skill experience. default = 80

            questnum = (int)_Listing_Standard.Slider((float)questnum, 0f, 20);

            


            _Listing_Standard.End();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using UnityEngine;


namespace WG_BBM
{
    class WG_GOM_mod : Verse.Mod
    {
        public static WG_BBM_setting Settings;

        public WG_GOM_mod(ModContentPack content)
            : base(content)
        {
            WG_GOM_mod.Settings = GetSettings<WG_BBM_setting>();
        }

        public override string SettingsCategory()
        {
            return "Big battle mod";
            //return base.SettingsCategory();
        }


        public override void DoSettingsWindowContents(Rect inRect)
        {
            Settings.DoSettingsWindowContents(inRect);
            //base.DoSettingsWindowContents(inRect);
        }

    }
    class WG_BBM_setting : ModSettings
    {
        public static float enemypersent = 2;
        public static float friendpersent = 1;


        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<float>(ref enemypersent, "WG_BBM_enemypersent", 2);
            Scribe_Values.Look<float>(ref friendpersent, "WG_BBM_friendpersent", 1);

        }

        public void DoSettingsWindowContents(Rect canvas)
        {
            Listing_Standard _Listing_Standard = new Listing_Standard();
            _Listing_Standard.ColumnWidth = canvas.width;
            _Listing_Standard.Begin(canvas);
            //listing_Standard.set_ColumnWidth(rect.get_width() - 4f);


            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label(Translator.Translate("Enemy's strength") + " : " + enemypersent * 100f + " % "); // 

            enemypersent = (float)_Listing_Standard.Slider((float)enemypersent, 1f, 3);

            _Listing_Standard.GapLine(12f);
            _Listing_Standard.Label(Translator.Translate("Friend's strength") + " : " + friendpersent * 100f + " % "); // 

            friendpersent = (float)_Listing_Standard.Slider((float)friendpersent, 0.1f, 2);



            _Listing_Standard.End();
        }
    }
}

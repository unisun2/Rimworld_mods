using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace FP_RSLUM
{
    class FP_RSLUM_mod : Verse.Mod
    {

        public static FP_RSLUM_setting Settings;

        public FP_RSLUM_mod(ModContentPack content) : base(content)
        {
            FP_RSLUM_mod.Settings = GetSettings<ModSettings_Embrasures>();
        }

        public override string SettingsCategory()
        {
            return "RPG_Style_Level_Up_Mod";
            //return base.SettingsCategory();
        }


        public override void DoSettingsWindowContents(Rect inRect)
        {
            Settings.DoSettingsWindowContents(inRect);
            //base.DoSettingsWindowContents(inRect);
        }
    }
}
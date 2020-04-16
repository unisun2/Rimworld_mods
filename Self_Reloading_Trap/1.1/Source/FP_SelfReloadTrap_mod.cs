using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;
using UnityEngine;
namespace FPSRT
{
    class FP_SelfReloadTrap_mod : Verse.Mod
    {
        public static FP_SelfReloadTrap_setting Settings;

        public FP_SelfReloadTrap_mod(ModContentPack content)
            : base(content)
        {
            FP_SelfReloadTrap_mod.Settings = GetSettings<FP_SelfReloadTrap_setting>();
            //ThingDef _mytrap = ThingDef.Named("Building_FPSRT");
            //_mytrap.costStuffCount = FP_SelfReloadTrap_setting.bulidingcost;
        }

        public override string SettingsCategory()
        {
            return "Self_Reloading_Trap_Mod";
            //return base.SettingsCategory();
        }


        public override void DoSettingsWindowContents(Rect inRect)
        {
            Settings.DoSettingsWindowContents(inRect);
            //base.DoSettingsWindowContents(inRect);
        }
    }
}

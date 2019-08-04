using System;
using HugsLib;
using HugsLib.Settings;
using Verse;

namespace SRFPSRT
{

    public class SRFPSRT_settings : ModBase
    {
        public override string ModIdentifier
        {
            get { return "SRFPSRT"; }
        }

        private static SettingHandle<int> buildcost;
        private static SettingHandle<float> trapdamage;
        private static SettingHandle<float> armorpenetrate;
        private static SettingHandle<int> traparmingtime;

        public override void DefsLoaded()
        {
            buildcost = Settings.GetHandle<int>("buildcost", "build cost. Setting will only apply after a Game Restart. min : 10, max : 3000", 
                "", 400, Validators.IntRangeValidator(10, 3000));
            trapdamage = Settings.GetHandle<float>("trapdamage", "damage of trap ratio to original. min : 0.1, max : 3",
                "", 1f, Validators.FloatRangeValidator(0.1f, 3f));
            armorpenetrate = Settings.GetHandle<float>("armorpenetrate", "armor penetration Ratio to original. min : 0, max : 3",
                "", 1f, Validators.FloatRangeValidator(0f, 3f));
            
            traparmingtime = Settings.GetHandle<int>("traparmingtime", "Time to recharge. min : 5, max : 999",
             "", 10, Validators.IntRangeValidator(5, 999));

            ThingDef _mytrap = ThingDef.Named("Building_SRFPSRTunarmed");
            _mytrap.costStuffCount = buildcost;
        }

        public static int Getbuildcost()
        {
            return buildcost.Value;
        }
        public static float Gettrapdamage()
        {
            return trapdamage.Value;
        }
        public static float Getarmorpenetrate()
        {
            return armorpenetrate.Value;
        }
        public static int Gettraparmingtime()
        {
            return traparmingtime.Value;
        }
    }
}

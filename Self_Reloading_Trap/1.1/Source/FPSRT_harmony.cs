using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld;
using Verse;


namespace FPSRT
{
    [StaticConstructorOnStartup]
    class FPSRT_harmony
    {
        static FPSRT_harmony()
        {
            Log.Message("FP_self reloading trap patched.");
            SRT_costpatch();
        }

        static void SRT_costpatch()
        {
            ThingDef mytrap = ThingDef.Named("Building_FPSRT");
            mytrap.costStuffCount = FP_SelfReloadTrap_setting.bulidingcost;

        }

    }
}
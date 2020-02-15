using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace FP_RSLUM
{
    public static class ModCompatibilityCheck
    {
        public static bool CombatExtendedIsActive
            => ModsConfig.ActiveModsInLoadOrder.Any(m => m.Name == "Combat Extended");

        public static bool StaticQualityPlusIsActive
            => ModsConfig.ActiveModsInLoadOrder.Any(m => m.Name == "Static Quality Plus 1.2");
    }
}

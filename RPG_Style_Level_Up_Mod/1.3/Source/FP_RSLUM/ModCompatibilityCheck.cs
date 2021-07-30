using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace FP_RSLUM
{
    [StaticConstructorOnStartup]
    public static class ModCompatibilityCheck
    {
        public static bool CombatExtendedIsActive = ModsConfig.ActiveModsInLoadOrder.Any(m => m.Name == "Combat Extended");
        public static bool CarryCapacityFixIsActive = ModsConfig.ActiveModsInLoadOrder.Any(m => m.Name == "Carry Capacity Fix");
        
    }
}

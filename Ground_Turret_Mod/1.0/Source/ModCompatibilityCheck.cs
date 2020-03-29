using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace FP_GTM
{
    class ModCompatibilityCheck
    {
        public static bool CombatExtendedIsActive
            => ModsConfig.ActiveModsInLoadOrder.Any(m => m.Name == "Combat Extended"); 

        public static bool TurretExtensionsIsActive
            => ModsConfig.ActiveModsInLoadOrder.Any(m => m.Name == "[XND] Turret Extensions");


    }
}

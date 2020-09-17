using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace FP_Warp_TB
{
    class CompProperties_FP_WarpstoneTracterBeam : CompProperties
    {
        public float scanFindMtbDays = 20f;

        public float scanFindGuaranteedDays = 30f;

        public StatDef scanSpeedStat;

        public override IEnumerable<string> ConfigErrors(ThingDef parentDef)
        {
            if (scanFindMtbDays <= 0f)
            {
                yield return "scanFindMtbDays not set";
            }
        }
    }
}

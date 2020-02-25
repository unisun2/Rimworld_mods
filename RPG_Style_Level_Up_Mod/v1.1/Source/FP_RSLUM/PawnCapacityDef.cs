using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;

namespace FP_RSLUM
{

    [DefOf]
    public static class PawnCapacityDefOf
    {
        public static PawnCapacityDef RSLUM_STR;

        public static PawnCapacityDef RSLUM_DEX;

        public static PawnCapacityDef RSLUM_AGL;

        public static PawnCapacityDef RSLUM_CON;

        public static PawnCapacityDef RSLUM_INT;

        public static PawnCapacityDef RSLUM_CHA;
        static PawnCapacityDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(StatDefOf));
        }
    }



    [DefOf]
    public static class HediffDefOf
    {
        public static HediffDef RSLUM_LVUP;
    }
}

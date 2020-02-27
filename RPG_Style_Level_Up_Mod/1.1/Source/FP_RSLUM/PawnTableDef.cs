using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;
using UnityEngine;

namespace FP_RSLUM
{
    [DefOf]
    public static class PawnTableDefOf
    {
        public static PawnTableDef FP_RSLUM_MainTable;

        static PawnTableDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(PawnTableDefOf));
        }
    }
}
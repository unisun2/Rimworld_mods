using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;

namespace FP_RSLUM
{
    class PawnTable_LevelMain : PawnTable
    {
        public PawnTable_LevelMain(PawnTableDef def, Func<IEnumerable<Pawn>> pawnsGetter, int uiWidth, int uiHeight)
            : base(def, pawnsGetter, uiWidth, uiHeight)
        {
        }

        public PawnTableDef PawnTableDef { get; protected set; }

        protected override IEnumerable<Pawn> PrimarySortFunction(IEnumerable<Pawn> input)
        {
            return from p in input
                   orderby p.def.label
                   select p;
        }
    }
}

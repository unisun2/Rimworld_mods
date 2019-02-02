using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using Verse;
using RimWorld;

namespace to_my_side
{

    public class CompUseEffect_TMSLV1 : CompUseEffect
    {
        public override void DoEffect(Pawn usedBy)
        {
            base.DoEffect(usedBy);
            Hediff hediff = HediffMaker.MakeHediff(HediffDefOf.TMS_MoveSpeed1, usedBy, null);
            usedBy.health.AddHediff(hediff);

        }
    }
}
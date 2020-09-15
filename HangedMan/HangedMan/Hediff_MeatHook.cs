using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace FPDBDHook
{
    class Hediff_MeatHook : HediffWithComps
    {
        public override float PainOffset
        {
            get
            {
                if (CurStage != null)
                {
                    return 0.5f;
                }
                return 0f;
            }
        }

        public override float BleedRate
        {
            get
            {
                if (pawn.Dead)
                {
                    return 0f;
                }
                if (!(pawn.RaceProps.IsFlesh))
                {
                    return 0f;
                }
                return 0.2f;
            }
        }

    }
}

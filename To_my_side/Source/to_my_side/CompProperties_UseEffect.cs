using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;
using Verse.AI;
using Verse.Sound;
using UnityEngine;

namespace to_my_side
{

    public class CompUseEffect_Artifact : CompUseEffect
    {
        public override void DoEffect(Pawn usedBy)
        {
            base.DoEffect(usedBy);
            SoundDefOf.PsychicPulseGlobal.PlayOneShotOnCamera(usedBy.MapHeld);
            usedBy.records.Increment(RecordDefOf.ArtifactsActivated);
        }
    }



    public class CompTargetEffect_TMS_bufftwo : CompTargetEffect_TMS_buffone
    {
       
        public override void DoEffectOn(Pawn user, Thing target)
        {
            Pawn pawn = (Pawn)target;
            if (pawn.Dead)
            {
                return;
            }
            else if (pawn.RaceProps.Humanlike)   // no human!
            {
                pawn.health.AddHediff(RimWorld.HediffDefOf.Hangover);
                pawn.health.AddHediff(RimWorld.HediffDefOf.FoodPoisoning);
                return;
            }

            plusLvHediff(ref pawn, 3);

        }
    }

    public class CompTargetEffect_TMS_buffthree : CompTargetEffect_TMS_buffone
    {

        public override void DoEffectOn(Pawn user, Thing target)
        {
            Pawn pawn = (Pawn)target;
            if (pawn.Dead)
            {
                return;
            }
            else if (pawn.RaceProps.Humanlike)   // no human!
            {
                pawn.health.AddHediff(RimWorld.HediffDefOf.Hangover);
                pawn.health.AddHediff(RimWorld.HediffDefOf.FoodPoisoning);
                return;
            }

            plusLvHediff(ref pawn, 5);

        }
    }

}
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
            else if (pawn.RaceProps.Humanlike)   // 
            {
                pawn.health.AddHediff(RimWorld.HediffDefOf.Hangover);
                pawn.health.AddHediff(RimWorld.HediffDefOf.FoodPoisoning);
               //plusLvHediff(ref pawn, 3);
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
            else if (pawn.RaceProps.Humanlike)   // 
            {
                pawn.health.AddHediff(RimWorld.HediffDefOf.Hangover);
                pawn.health.AddHediff(RimWorld.HediffDefOf.FoodPoisoning);
                //plusLvHediff(ref pawn, 5);
                return;
            }

            plusLvHediff(ref pawn, 5);

        }
    }

    public class CompTargetEffect_TMS_bufffour : CompTargetEffect_TMS_buffone
    {

        public override void DoEffectOn(Pawn user, Thing target)
        {
            Pawn pawn = (Pawn)target;
            if (pawn.Dead)
            {
                return;
            }
            else if (pawn.RaceProps.Humanlike)   // 
            {
                pawn.health.AddHediff(RimWorld.HediffDefOf.Hangover);
                pawn.health.AddHediff(RimWorld.HediffDefOf.FoodPoisoning);
                //plusLvHediff(ref pawn, 7);
                return;
            }

            plusLvHediff(ref pawn, 7);

        }
    }

    

    public class CompTargetEffect_TMS_buffoneman : CompTargetEffect_TMS_buffone
    {

        public override void DoEffectOn(Pawn user, Thing target)
        {
            Pawn pawn = (Pawn)target;
            if (pawn.Dead)
            {
                return;
            }
            else if (pawn.RaceProps.Humanlike)   // 
            {
                pawn.health.AddHediff(RimWorld.HediffDefOf.Hangover);
                pawn.health.AddHediff(RimWorld.HediffDefOf.FoodPoisoning);
                plusLvHediff(ref pawn, 1);
                return;
            }

            plusLvHediff(ref pawn, 1);

        }
    }

    public class CompTargetEffect_TMS_bufftwoman : CompTargetEffect_TMS_buffone
    {

        public override void DoEffectOn(Pawn user, Thing target)
        {
            Pawn pawn = (Pawn)target;
            if (pawn.Dead)
            {
                return;
            }
            else if (pawn.RaceProps.Humanlike)   // 
            {
                pawn.health.AddHediff(RimWorld.HediffDefOf.Hangover);
                pawn.health.AddHediff(RimWorld.HediffDefOf.FoodPoisoning);
                plusLvHediff(ref pawn, 3);
                return;
            }

            plusLvHediff(ref pawn, 3);

        }
    }

    public class CompTargetEffect_TMS_buffthreeman : CompTargetEffect_TMS_buffone
    {

        public override void DoEffectOn(Pawn user, Thing target)
        {
            Pawn pawn = (Pawn)target;
            if (pawn.Dead)
            {
                return;
            }
            else if (pawn.RaceProps.Humanlike)   // 
            {
                pawn.health.AddHediff(RimWorld.HediffDefOf.Hangover);
                pawn.health.AddHediff(RimWorld.HediffDefOf.FoodPoisoning);
                plusLvHediff(ref pawn, 5);
                return;
            }

            plusLvHediff(ref pawn, 5);

        }
    }

    public class CompTargetEffect_TMS_bufffourman : CompTargetEffect_TMS_buffone
    {

        public override void DoEffectOn(Pawn user, Thing target)
        {
            Pawn pawn = (Pawn)target;
            if (pawn.Dead)
            {
                return;
            }
            else if (pawn.RaceProps.Humanlike)   // 
            {
                pawn.health.AddHediff(RimWorld.HediffDefOf.Hangover);
                pawn.health.AddHediff(RimWorld.HediffDefOf.FoodPoisoning);
                plusLvHediff(ref pawn, 7);
                return;
            }

            plusLvHediff(ref pawn, 7);

        }
    }


}
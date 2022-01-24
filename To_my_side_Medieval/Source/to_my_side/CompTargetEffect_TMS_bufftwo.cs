using Verse;

namespace to_my_side;

public class CompTargetEffect_TMS_bufftwo : CompTargetEffect_TMS_buffone
{
    public override void DoEffectOn(Pawn user, Thing target)
    {
        var pawn = (Pawn)target;
        if (pawn.Dead)
        {
            return;
        }

        if (pawn.RaceProps.Humanlike)
        {
            pawn.health.AddHediff(RimWorld.HediffDefOf.Hangover);
            pawn.health.AddHediff(RimWorld.HediffDefOf.FoodPoisoning);
        }
        else
        {
            plusLvHediff(ref pawn, 3);
        }
    }
}
using RimWorld;
using Verse;
using Verse.Sound;

namespace to_my_side;

public class CompUseEffect_Artifact : CompUseEffect
{
    public override void DoEffect(Pawn usedBy)
    {
        base.DoEffect(usedBy);
        SoundDefOf.PsychicPulseGlobal.PlayOneShotOnCamera(usedBy.MapHeld);
        usedBy.records.Increment(RecordDefOf.ArtifactsActivated);
    }
}
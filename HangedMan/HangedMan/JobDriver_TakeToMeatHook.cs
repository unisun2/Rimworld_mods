using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;



namespace FPDBDHook
{
	public class JobDriver_TakeToMeatHook : JobDriver
	{
		private const TargetIndex TakeeIndex = TargetIndex.A;

	    private const TargetIndex HookIndex = TargetIndex.B;


		protected Pawn Takee => (Pawn)job.GetTarget(TargetIndex.A).Thing;

		public Building_MeatHook DropHook => (Building_MeatHook)job.GetTarget(TargetIndex.B).Thing;

		public override bool TryMakePreToilReservations(bool errorOnFailed)
		{
			return pawn.Reserve(DropHook, job, DropHook.pawncount, 0, null, errorOnFailed);
		}

	    protected override IEnumerable<Toil> MakeNewToils()
	    {
		    this.FailOnDestroyedOrNull(TargetIndex.A);
		    this.FailOnDestroyedOrNull(TargetIndex.B);
		    this.FailOnAggroMentalStateAndHostile(TargetIndex.A);

		    yield return Toils_Bed.ClaimBedIfNonMedical(TargetIndex.B, TargetIndex.A);
		    AddFinishAction(delegate
		    {
			    if (job.def.makeTargetPrisoner && Takee.ownership.OwnedBed == DropHook && Takee.Position != RestUtility.GetBedSleepingSlotPosFor(Takee, DropHook))

			    if (pawn.Reserve(Takee, job, 1, -1, null, errorOnFailed))

			    {
				    return pawn.Reserve(DropHook, job, DropHook.SleepingSlotsCount, 0, null, errorOnFailed);
			    }
			    return false;
		    }

		    protected override IEnumerable<Toil> MakeNewToils()
		    {
			    this.FailOnDestroyedOrNull(TargetIndex.A);
			    this.FailOnDestroyedOrNull(TargetIndex.B);
			    this.FailOnAggroMentalStateAndHostile(TargetIndex.A);

			    yield return Toils_Bed.ClaimBedIfNonMedical(TargetIndex.B, TargetIndex.A);
			    AddFinishAction(delegate
			    {
				    if (job.def.makeTargetPrisoner && Takee.ownership.OwnedBed == DropHook && Takee.Position != RestUtility.GetBedSleepingSlotPosFor(Takee, DropHook))
				    {
					    Takee.ownership.UnclaimBed();
				    }
			    });
			    yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.ClosestTouch).FailOnDespawnedNullOrForbidden(TargetIndex.A).FailOnDespawnedNullOrForbidden(TargetIndex.B)
				    .FailOn(() => !pawn.CanReach(DropHook, PathEndMode.OnCell, Danger.Deadly))
				    .FailOnSomeonePhysicallyInteracting(TargetIndex.A);
			    Toil toil = new Toil();
			    toil.initAction = delegate
			    {
				    if (job.def.makeTargetPrisoner)
				    {
					    Pawn pawn = (Pawn)job.targetA.Thing;
					    pawn.GetLord()?.Notify_PawnAttemptArrested(pawn);
					    GenClamor.DoClamor(pawn, 10f, ClamorDefOf.Harm);
					    if (!pawn.IsPrisoner)
					    {
						    QuestUtility.SendQuestTargetSignals(pawn.questTags, "Arrested", pawn.Named("SUBJECT"));
					    }
				    }
			    };
			    yield return toil;
			    Toil toil2 = Toils_Haul.StartCarryThing(TargetIndex.A).FailOnNonMedicalBedNotOwned(TargetIndex.B, TargetIndex.A);
			    yield return toil2;
			    yield return Toils_Goto.GotoThing(TargetIndex.B, PathEndMode.Touch);
			    Toil toil3 = new Toil();
			    toil3.initAction = delegate
			    {
				    CheckMakeTakeePrisoner();
				    if (Takee.playerSettings == null)
				    {
					    Takee.playerSettings = new Pawn_PlayerSettings(Takee);
				    }
			    };
			    yield return toil3;
			    yield return Toils_Reserve.Release(TargetIndex.B);
			    Toil toil4 = new Toil();
			    toil4.initAction = delegate
			    {
				    IntVec3 position = DropHook.Position;
				    pawn.carryTracker.TryDropCarriedThing(position, ThingPlaceMode.Direct, out Thing _);
				    if (!DropHook.Destroyed && (DropHook.OwnersForReading.Contains(Takee) || (DropHook.Medical && DropHook.AnyUnoccupiedSleepingSlot) || Takee.ownership == null))
				    {
					    Takee.jobs.Notify_TuckedIntoBed(DropHook);
					    Takee.mindState.Notify_TuckedIntoBed();
				    }
				    if (Takee.IsPrisonerOfColony)
				    {
					    LessonAutoActivator.TeachOpportunity(ConceptDefOf.PrisonerTab, Takee, OpportunityType.GoodToKnow);
				    }
			    };
			    toil4.defaultCompleteMode = ToilCompleteMode.Instant;
			    yield return toil4;
		    }

		    private void CheckMakeTakeePrisoner()
		    {
			    if (job.def.makeTargetPrisoner)
			    {
				    if (Takee.guest.Released)
				    {
					    Takee.guest.Released = false;
					    Takee.guest.interactionMode = PrisonerInteractionModeDefOf.NoInteraction;
				    }
				    if (!Takee.IsPrisonerOfColony)
				    {
					    Takee.guest.CapturedBy(Faction.OfPlayer, pawn);
				    }
			    }
		    }

	    }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;
using Verse.AI;
using System.Diagnostics;

namespace Humanpowergeneratormod
{
    public class JobDriver_HPGMcycling : JobDriver
    {
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            Pawn pawn = this.pawn;
            LocalTargetInfo targetA = this.job.targetA;
            Job job = this.job;
            return pawn.Reserve(targetA, job, 1, -1, null, errorOnFailed);
        }

        [DebuggerHidden]
        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedNullOrForbidden(TargetIndex.A);
            this.FailOnBurningImmobile(TargetIndex.A);
            this.FailOn(delegate
            {
                HPGMcyclecomp compHPGM = this.job.targetA.Thing.TryGetComp<HPGMcyclecomp>();
                //HPGMcyclecomp compHPGM = null;
                return !compHPGM.CanUseNow;
            });
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.InteractionCell);
            Toil work = new Toil();
            work.tickAction = delegate
            {
                Pawn actor = work.actor;
                Building building = (Building)actor.CurJob.targetA.Thing;
                HPGMcyclecomp comp = building.GetComp<HPGMcyclecomp>();
                comp.Used(actor);
                //actor.skills.Learn(SkillDefOf.Intellectual, 0.035f, false);
                actor.skills.Learn(SkillDefOf.Melee, 0.02f, false);
                actor.GainComfortFromCellIfPossible();
            };
            work.defaultCompleteMode = ToilCompleteMode.Never;
            work.FailOnCannotTouch(TargetIndex.A, PathEndMode.InteractionCell);
            //work.activeSkill = (() => SkillDefOf.Intellectual);
            yield return work;
        }
    }


        public class WorkGiver_HPGMcycling : WorkGiver_Scanner
    {
        public override ThingRequest PotentialWorkThingRequest
        {
            get
            {
                return ThingRequest.ForDef(ThingDefOf.HPGMHumanPowerGenerator);
            }
        }

        public override PathEndMode PathEndMode
        {
            get
            {
                return PathEndMode.InteractionCell;
            }
        }

        public override Danger MaxPathDanger(Pawn pawn)
        {
            return Danger.Deadly;
        }

        public override IEnumerable<Thing> PotentialWorkThingsGlobal(Pawn pawn)
        {
            return pawn.Map.listerBuildings.AllBuildingsColonistOfDef(ThingDefOf.HPGMHumanPowerGenerator).Cast<Thing>();
        }

        public override bool ShouldSkip(Pawn pawn, bool forced = false)
        {
            List<Building> allBuildingsColonist = pawn.Map.listerBuildings.allBuildingsColonist;
            for (int i = 0; i < allBuildingsColonist.Count; i++)
            {
                if (allBuildingsColonist[i].def == ThingDefOf.HPGMHumanPowerGenerator)
                {
                    return false;
                }
            }
            return true;
        }

        public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            Building building = t as Building;
            if (building == null)
            {
                return false;
            }
            if (building.IsForbidden(pawn))
            {
                return false;
            }
            LocalTargetInfo target = building;
            if (!pawn.CanReserve(target, 1, -1, null, forced))
            {
                return false;
            }
            HPGMcyclecomp compHPGM = building.TryGetComp<HPGMcyclecomp>();
            return compHPGM.CanUseNow && !building.IsBurning();
        }

        public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            return new Job(JobDefOf.HPGMcycling, t, 1500, true);
        }
    }
}

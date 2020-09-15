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
    public class JobDriver_TakeToMeatHook2 : JobDriver_UseItem
    {
        protected Thing Thing
	    {
		    get
		    {
			    LocalTargetInfo target = base.job.GetTarget((TargetIndex)1);
			    return ((LocalTargetInfo)(ref target)).get_Thing();
		    }
	    }

        protected Thing Target
	    {
		    get
		    {
			    LocalTargetInfo target = base.job.GetTarget((TargetIndex)2);
			    return ((LocalTargetInfo)(ref target)).get_Thing();
		    }
	    }

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {

            return ReservationUtility.Reserve(base.pawn, LocalTargetInfo.op_Implicit(Thing), base.job, 1, -1, (ReservationLayerDef)null, errorOnFailed) && ReservationUtility.Reserve(base.pawn, LocalTargetInfo.op_Implicit(Target), base.job, 1, -1, (ReservationLayerDef)null, errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            ToilFailConditions.FailOnDestroyedOrNull<

            return base.MakeNewToils();
        }



    }
}

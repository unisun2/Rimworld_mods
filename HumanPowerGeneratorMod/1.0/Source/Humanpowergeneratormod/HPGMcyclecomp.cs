using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;


namespace Humanpowergeneratormod
{
    public class CompProperties_HPGM : CompProperties
    {
        public CompProperties_HPGM()
        {
            this.compClass = typeof(HPGMcyclecomp);
        }
    }




    public class HPGMcyclecomp : ThingComp
    {
        public CompProperties_HPGM Props
        {
            get
            {
                return (CompProperties_HPGM)this.props;
            }
        }

        public bool CanUseNow
        {
            get
            {
                return this.parent.Spawned && this.parent.Faction == Faction.OfPlayer;
            }
        }

        public override void Initialize(CompProperties props)
        {
            base.Initialize(props);
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);

        }
        
        public void Used(Pawn worker)
        {
            //statValue = worker.GetStatValue(StatDefOf.MoveSpeed, true);
            //statValue = worker.health.capacities.GetLevel(PawnCapacityDefOf.Moving);
            statValue = (worker.GetStatValue(StatDefOf.MoveSpeed)) / 4.6f;
            IsRunning = 100;
        }

        public override void CompTick()
        {
            base.CompTick();

            if (IsRunning > 0)
            {
                //Verse.Log.Message(IsRunning.ToString(), false);
                IsRunning--;
                IsOn = true;
            }
            else IsOn = false;
            
        }

        public bool IsOn;
        public int IsRunning = 0;
        public float statValue;



    }
}
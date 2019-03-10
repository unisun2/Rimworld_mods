using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;
using Verse.Sound;
using Verse.AI;
using Verse.AI.Group;

namespace PLAHF
{
    public class Building_TacticalManaBomb : Building
    {
        
        //CellFinder.RandomClosewalkCellNear(base.Position, base.Map, 1, null)
        //public int ticksInday = 60000;
        private static readonly IntRange CountRange = new IntRange(10, 20);

        public int TimerTicks = 60000;

        public int age = -1;

        public float spawnpoints = 100;
        public float pointsLeft;
        private Lord lord;
        private bool warnhalf = true;

        public static List<PawnKindDef> spawnablePawnKinds = new List<PawnKindDef>();


        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref this.age, "age", 0, false);
        }

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            Faction faction = Find.FactionManager.FirstFactionOfDef(FactionDef.Named("PLAHF_faction"));
            base.SpawnSetup(map, respawningAfterLoad);
            if (this.Faction == null)
            {
                this.SetFaction(faction, null);
            }
            if (!respawningAfterLoad)
            {
                this.SpawnPawns_start();
            }
        }

        private void SpawnPawns_start()
        {
            LordJob_AssaultColony lordJob = new LordJob_AssaultColony(this.Faction, true, false, true, false, true);
            this.lord = LordMaker.MakeNewLord(this.Faction, lordJob, this.Map, null);

            pointsLeft = spawnpoints;
            IntVec3 invalid;
            if (!CellFinder.TryFindRandomCellNear(this.Position, this.Map, 7, (IntVec3 c) => c.Standable(this.Map) && this.Map.reachability.CanReach(c, this, PathEndMode.Touch, TraverseParms.For(TraverseMode.PassDoors, Danger.Deadly, false)), out invalid, -1))
            {
                //Log.Message("Building_TacticalManaBomb. SpawnInitialPawns spawnpoints : " + this.pointsLeft + "...." + this, false);
                invalid = IntVec3.Invalid;
        }
            else if(spawnpoints <= 100)
            {
                //Log.Message("Building_TacticalManaBomb. SpawnInitialPawns spawnpoints : " + this.pointsLeft + "...." + this, false);
                return;
            }

    Faction faction = Find.FactionManager.FirstFactionOfDef(FactionDef.Named("PLAHF_faction"));

    
    Pawn pawn;
    int RRR;

            while (pointsLeft > 80)
            {
                RRR = Rand.Range(0, 7);

                if (RRR< 1)
                {
                    pawn = PawnGenerator.GeneratePawn(PawnKindDef.Named("PLAHF_Honorary_member"), faction);
                }
                else if (RRR< 2)
                {
                    pawn = PawnGenerator.GeneratePawn(PawnKindDef.Named("PLAHF_test_subjectB"), faction);
                }
                else if(RRR< 5)
                {
                    pawn = PawnGenerator.GeneratePawn(PawnKindDef.Named("PLAHF_former_Town_Guard"), faction);
                }
                else
                {
                    pawn = PawnGenerator.GeneratePawn(PawnKindDef.Named("PLAHF_Lab_slave"), faction);
                }

                GenSpawn.Spawn(pawn, CellFinder.RandomClosewalkCellNear(base.Position, base.Map, 3, null), base.Map, WipeMode.Vanish);
                this.lord.AddPawn(pawn);
                pointsLeft -= pawn.kindDef.combatPower;
            }

        }
       


        public override void Tick()
        {
            base.Tick();

            this.age++;
            if (this.age >= TimerTicks)          // time out!!
            {
                Map map = base.Map;
                Thing thingslag = ThingMaker.MakeThing(RimWorld.ThingDefOf.ChunkSlagSteel, null);
                GenPlace.TryPlaceThing(thingslag, base.Position, map, ThingPlaceMode.Near, null, null);

                // great masive destruction here
                
                PLAHFmanabooom manabooom = (PLAHFmanabooom)GenSpawn.Spawn((PLAHFmanabooom)ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("PLAHFmanabooom", true)) , this.Position, map, WipeMode.Vanish);
                manabooom.duration = 2222;
                manabooom.instigator = this.Faction.leader;

                manabooom.weaponDef = null;
                manabooom.StartStrike();
                

                /*
                Bombardment bombardment = (Bombardment)GenSpawn.Spawn(RimWorld.ThingDefOf.Bombardment, bombstart, this.Map, WipeMode.Vanish);
                bombardment.duration = 540;
                bombardment.instigator = this.Faction.leader;
                bombardment.weaponDef = null;
                bombardment.StartStrike();
                */

                
                this.Destroy(DestroyMode.Vanish);
            }
            else if (warnhalf)
            {
                if(this.age >= (TimerTicks/2))
                {
                    Find.LetterStack.ReceiveLetter("LetterLabelPLAHFTMBAttackhalf".Translate(), "LetterPLAHFTMBAttackhalf".Translate(), LetterDefOf.ThreatBig, this, null, null);
                    warnhalf = !warnhalf;
                }
            }


        }



        private bool CanSpawnAt(IntVec3 c, Map map)
        {
            if (!c.Standable(map) || c.Fogged(map) || !c.GetRoom(map, RegionType.Set_Passable).PsychologicallyOutdoors || c.GetEdifice(map) != null)
            {
                return false;
            }

            return true;
        }


        public override string GetInspectString()
        {

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(base.GetInspectString());

            float totalProgress = ((float)age / (float)(TimerTicks));

            stringBuilder.Append("\n Detonating... : " + totalProgress.ToStringPercent());

            return stringBuilder.ToString();
        }
        
    }
}

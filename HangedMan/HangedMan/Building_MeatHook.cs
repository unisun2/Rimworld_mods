using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using UnityEngine;

namespace HangedMan
{
    class Building_MeatHook : Building_Bed
    {
        private bool forPrisonersInt = true;

        private bool medicalInt = false;


        public bool ForPrisoners
        {
            get
            {
                return forPrisonersInt;
            }
            set
            {
                forPrisonersInt = true;
            }
        }
        public new bool Medical
        {
            get
            {
                return false;
            }
            set
            {
                medicalInt = false;
            }
        }

        public override Color DrawColorTwo
        {
            get
            {
                return base.DrawColorTwo;
            }
        }

        public override IEnumerable<FloatMenuOption> GetFloatMenuOptions(Pawn myPawn)
        {
            return null;
            // no Medical, no interact
        }


        public override void Tick()
        {
            base.Tick();
            if (base.Spawned && base.ForPrisoners)
            {
                List<Thing> thingList = base.Position.GetThingList(base.Map);
                for (int i = 0; i < thingList.Count; i++)
                {
                    Pawn pawn = thingList[i] as Pawn;
                    if (pawn != null && pawn.GetPosture() == PawnPosture.LayingInBed && pawn.IsPrisoner)
                    {
                        Hediff hediff = HediffMaker.MakeHediff(HangedManDefOf.HangedManHediff_Hooked,pawn, null);
                        pawn.health.AddHediff(hediff);
                    }
                }

            }
            


        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using UnityEngine;
using System.Reflection;

namespace HangedMan
{
    class Building_MeatHook : Building_Bed
    {
        public int pawncount = 0;

        public int killcount = 0;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<int>(ref pawncount, "pawncount", defaultValue: 0);
            Scribe_Values.Look<int>(ref killcount, "killcount", defaultValue: 0);
        }

        //public override IEnumerable<FloatMenuOption> GetFloatMenuOptions(Pawn myPawn)
        //{
        //    return null;
        //    // no Medical, no interact
        //}


        public override void Tick()
        {
            base.Tick();
            if (base.Spawned)
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

        public override string GetInspectString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(base.GetInspectString());

            stringBuilder.AppendInNewLine("Killcounts".Translate() + ": " + this.killcount.ToString());

            return stringBuilder.ToString();
        }
        
    }
}

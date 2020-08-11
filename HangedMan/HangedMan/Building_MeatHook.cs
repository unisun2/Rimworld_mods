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
    public class Building_MeatHook : Building
    {
        public int pawncount = 0;

<<<<<<< Updated upstream
        public int killcount = 0;
=======
>>>>>>> Stashed changes

        public override void ExposeData()
        {
            base.ExposeData();
<<<<<<< Updated upstream

            Scribe_Values.Look<int>(ref pawncount, "pawncount", defaultValue: 0);
            Scribe_Values.Look<int>(ref killcount, "killcount", defaultValue: 0);
        }

        //public override IEnumerable<FloatMenuOption> GetFloatMenuOptions(Pawn myPawn)
        //{
        //    return null;
        //    // no Medical, no interact
        //}
=======


        }


        public override IEnumerable<FloatMenuOption> GetFloatMenuOptions(Pawn myPawn)
        {
            return null;
            // no Medical, no interact
        }
>>>>>>> Stashed changes


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
                        Hediff hediff = HediffMaker.MakeHediff(HangedManDefOf.HangedManHediff_Hooked, pawn, null);
                        pawn.health.AddHediff(hediff);
                    }
                }
            }
        }
<<<<<<< Updated upstream

        public override string GetInspectString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(base.GetInspectString());

            stringBuilder.AppendInNewLine("Killcounts".Translate() + ": " + this.killcount.ToString());

            return stringBuilder.ToString();
        }
        
=======
>>>>>>> Stashed changes
    }
}

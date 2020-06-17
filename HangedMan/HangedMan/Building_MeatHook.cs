using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace HangedMan
{
    class Building_MeatHook : Building_Bed
    {
        private List<Pawn> touchingPawns = new List<Pawn>();

        public override void Tick()
        {
            base.Tick();
            if (base.Spawned)
            {
                List<Thing> thingList = base.Position.GetThingList(base.Map);
                for (int i = 0; i < thingList.Count; i++)
                {
                    Pawn pawn = thingList[i] as Pawn;
                    if (pawn != null && !touchingPawns.Contains(pawn))
                    {
                        touchingPawns.Add(pawn);
                        CheckSpring(pawn);
                    }
                }
                for (int j = 0; j < touchingPawns.Count; j++)
                {
                    Pawn pawn2 = touchingPawns[j];
                    if (!pawn2.Spawned || pawn2.Position != base.Position)
                    {
                        touchingPawns.Remove(pawn2);
                    }
                }
            }
            


        }
    }
}

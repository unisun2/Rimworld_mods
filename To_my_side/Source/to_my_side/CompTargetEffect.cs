using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;
using Verse.AI;
using Verse.Sound;
using UnityEngine;
//using Harmony;
using RimWorld.Planet;

namespace to_my_side
{

    public class CompTargetEffect_TMS_buffone : CompTargetEffect
    {
        public CompTargetEffect_TMS_buffone() {
            Console.WriteLine("Hello World! from CompTargetEffect_TMS_buffone");
        }

        public void plusLvHediff(ref Pawn pawn, int Ranks)
        {
            //pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Berserk, null, true, false, null, false);

            System.Random r = new System.Random();
            Hediff hediff = HediffMaker.MakeHediff(HediffDefOf.TMSLV1[0], pawn, null);
            

            int[] randlist = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            randlist.Shuffle();

            // hediff types number 11
            int pernum = r.Next(0, 100);
            ///         1,  2, 3, 4, 5
            /// LV 1 = 60, 40, 0, 0, 0
            /// LV 2 = 40, 40,20, 0, 0
            /// LV 3 = 20, 30,30,20, 0
            /// LV 4 =  0, 25,40,30, 5
            /// LV 5 =  0, 10,40,40,10
            ///
            /// which one?
            /// hediff = HediffMaker.MakeHediff(HediffDefOf.TMSLV1[randlist[i]], pawn, null); 
            /// hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.TMSLV1[randlist[i]]);
            /// ///

            for (int i = 0; i < Ranks; i++)
            {

                pernum = r.Next(0, 100);

                switch (Ranks)
                {
                    case 1:
                        if (pernum < 60)
                        {
                            hediff = HediffMaker.MakeHediff(HediffDefOf.TMSLV1[randlist[i]], pawn, null);

                        }
                        else
                        {
                            hediff = HediffMaker.MakeHediff(HediffDefOf.TMSLV2[randlist[i]], pawn, null);
                        }
                        break;

                    case 2:
                    case 3:
                        if (pernum < 20)
                        {
                            hediff = HediffMaker.MakeHediff(HediffDefOf.TMSLV1[randlist[i]], pawn, null);
                        }
                        else if (pernum < 50)
                        {
                            hediff = HediffMaker.MakeHediff(HediffDefOf.TMSLV2[randlist[i]], pawn, null);
                        }
                        else if (pernum < 80)
                        {
                            hediff = HediffMaker.MakeHediff(HediffDefOf.TMSLV3[randlist[i]], pawn, null);
                        }
                        else
                        {
                            hediff = HediffMaker.MakeHediff(HediffDefOf.TMSLV4[randlist[i]], pawn, null);
                        }
                        break;

                    case 4:
                    case 5:

                        if (pernum < 10)
                        {
                            hediff = HediffMaker.MakeHediff(HediffDefOf.TMSLV2[randlist[i]], pawn, null);
                        }
                        else if (pernum < 50)
                        {
                            hediff = HediffMaker.MakeHediff(HediffDefOf.TMSLV3[randlist[i]], pawn, null);
                        }
                        else if (pernum < 90)
                        {
                            hediff = HediffMaker.MakeHediff(HediffDefOf.TMSLV4[randlist[i]], pawn, null);
                        }
                        else
                        {
                            hediff = HediffMaker.MakeHediff(HediffDefOf.TMSLV5[randlist[i]], pawn, null);
                        }

                        break;
                    default:
                        return;
                }


                BodyPartRecord part = null;
                pawn.RaceProps.body.GetPartsWithTag(BodyPartTagDefOf.ConsciousnessSource).TryRandomElement(out part);
                pawn.health.AddHediff(hediff, part, null, null);


            }

        }

        public override void DoEffectOn(Pawn user, Thing target)
        {
            Pawn pawn = (Pawn)target;
            if (pawn.Dead)
            {
                return;
            }

            plusLvHediff(ref pawn, 1);

        }
    }
}

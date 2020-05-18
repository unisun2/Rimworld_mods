using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;
using Verse.AI;
using Verse.Sound;
using RimWorld.Planet;

namespace to_my_side
{
    
    public class CompTargetEffect_TMS_buffone : CompTargetEffect
    {

        public CompTargetEffect_TMS_buffone() {

        }

        public void plusLvHediff(ref Pawn pawn, int Ranks)
        {
            // 11
            HediffDef[] TMSLV1 = {HediffDefOf.TMS_armor_Sharp1, HediffDefOf.TMS_armor_Blunt1, HediffDefOf.TMS_armor_Heat1 , HediffDefOf.TMS_HungerRate1 , HediffDefOf.TMS_MeleeDodgeChance1 , HediffDefOf.TMS_MoveSpeed1 ,
            HediffDefOf.TMS_ComfyTemperature1 ,HediffDefOf.TMS_BloodFiltration1,HediffDefOf.TMS_MeleeHitChance1,HediffDefOf.TMS_Manipulation1,HediffDefOf.TMS_Breathing1};
            HediffDef[] TMSLV2 = {HediffDefOf.TMS_armor_Sharp2, HediffDefOf.TMS_armor_Blunt2, HediffDefOf.TMS_armor_Heat2 , HediffDefOf.TMS_HungerRate2 , HediffDefOf.TMS_MeleeDodgeChance2 , HediffDefOf.TMS_MoveSpeed2 ,
            HediffDefOf.TMS_ComfyTemperature2 ,HediffDefOf.TMS_BloodFiltration2,HediffDefOf.TMS_MeleeHitChance2,HediffDefOf.TMS_Manipulation2,HediffDefOf.TMS_Breathing2};
            HediffDef[] TMSLV3 = {HediffDefOf.TMS_armor_Sharp3, HediffDefOf.TMS_armor_Blunt3, HediffDefOf.TMS_armor_Heat3 , HediffDefOf.TMS_HungerRate3 , HediffDefOf.TMS_MeleeDodgeChance3 , HediffDefOf.TMS_MoveSpeed3 ,
            HediffDefOf.TMS_ComfyTemperature3 ,HediffDefOf.TMS_BloodFiltration3,HediffDefOf.TMS_MeleeHitChance3,HediffDefOf.TMS_Manipulation3,HediffDefOf.TMS_Breathing3};
            HediffDef[] TMSLV4 = {HediffDefOf.TMS_armor_Sharp4, HediffDefOf.TMS_armor_Blunt4, HediffDefOf.TMS_armor_Heat4 , HediffDefOf.TMS_HungerRate4 , HediffDefOf.TMS_MeleeDodgeChance4 , HediffDefOf.TMS_MoveSpeed4 ,
            HediffDefOf.TMS_ComfyTemperature4 ,HediffDefOf.TMS_BloodFiltration4,HediffDefOf.TMS_MeleeHitChance4,HediffDefOf.TMS_Manipulation4,HediffDefOf.TMS_Breathing4};
            HediffDef[] TMSLV5 = {HediffDefOf.TMS_armor_Sharp5, HediffDefOf.TMS_armor_Blunt5, HediffDefOf.TMS_armor_Heat5 , HediffDefOf.TMS_HungerRate5 , HediffDefOf.TMS_MeleeDodgeChance5 , HediffDefOf.TMS_MoveSpeed5 ,
            HediffDefOf.TMS_ComfyTemperature5 ,HediffDefOf.TMS_BloodFiltration5,HediffDefOf.TMS_MeleeHitChance5,HediffDefOf.TMS_Manipulation5,HediffDefOf.TMS_Breathing5};

            Hediff hediff = HediffMaker.MakeHediff(TMSLV1[0], pawn, null);


            Hediff lv1, lv2, lv3, lv4, lv5;

            // delete hediffs first!
            //Remove for Reset MyHediffs
            List<Hediff> hediffs = pawn.health.hediffSet.hediffs.ToList();
            foreach (Hediff hediffchk in hediffs)
            {
                //Log.Message("TMS. in foreach... " + hediffchk.LabelCap);
                for (int jj = 0; jj < 11; jj++)
                {
                    lv1 = HediffMaker.MakeHediff(TMSLV1[jj], pawn, null);
                    lv2 = HediffMaker.MakeHediff(TMSLV2[jj], pawn, null);
                    lv3 = HediffMaker.MakeHediff(TMSLV3[jj], pawn, null);
                    lv4 = HediffMaker.MakeHediff(TMSLV4[jj], pawn, null);
                    lv5 = HediffMaker.MakeHediff(TMSLV5[jj], pawn, null);
                    

                    if ((hediffchk.LabelCap == lv1.LabelCap) || (hediffchk.LabelCap == lv2.LabelCap) || (hediffchk.LabelCap == lv3.LabelCap) || (hediffchk.LabelCap == lv4.LabelCap) || (hediffchk.LabelCap == lv5.LabelCap))
                    {

                        //Log.Message("TMS. in foreach...yes. delete it " + hediffchk.LabelCap);
                        pawn.health.hediffSet.hediffs.Remove(hediffchk);
                        hediffchk.PostRemoved();
                        pawn.health.Notify_HediffChanged(null);
                    }
                    else
                    {
                        //Log.Message("TMS. in foreach...no delete " + hediffchk.LabelCap);
                    }

                }
            }
            //hediff = pawn.health.hediffSet.GetFirstHediffOfDef(TMSLV1[randlist[i]]);




            //pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Berserk, null, true, false, null, false);

            System.Random r = new System.Random();
            
            

            int[] randlist = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            randlist.Shuffle();

            // hediff types number 11
            int pernum = r.Next(0, 100);
            ///         1,  2, 3, 4, 5
            /// LV 1 = 40, 30,20,10, 0
            /// LV 2 = 40, 40,20, 0, 0 notused
            /// LV 3 =  9, 40,30,20, 1
            /// LV 4 =  0, 25,40,30, 3 notused
            /// LV 5 =  5, 30,30,30, 5
            /// LV 6 =  0, 20,30,30,20
            /// which one?
            /// hediff = HediffMaker.MakeHediff(HediffDefOf.TMSLV1[randlist[i]], pawn, null); 
            /// hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.TMSLV1[randlist[i]]);
            /// pawn.health.AddHediff(HediffDefOf.ChjAndroidLike);
            /// 
            /// 
            /// ///

            

            for (int i = 0; i < Ranks; i++)
            {

                pernum = r.Next(0, 100);

                switch (Ranks)
                {
                    case 1:
                        if (pernum < 40)
                        {
                            hediff = HediffMaker.MakeHediff(TMSLV1[randlist[i]], pawn, null);

                        }
                        else if(pernum < 70)
                        {
                            hediff = HediffMaker.MakeHediff(TMSLV2[randlist[i]], pawn, null);

                        }
                        else if (pernum < 90)
                        {
                            hediff = HediffMaker.MakeHediff(TMSLV3[randlist[i]], pawn, null);

                        }
                        else {
                            hediff = HediffMaker.MakeHediff(TMSLV4[randlist[i]], pawn, null);
                        }
                        break;

                    case 2:
                    case 3:
                        if (pernum < 9)
                        {
                            hediff = HediffMaker.MakeHediff(TMSLV1[randlist[i]], pawn, null);
                        }
                        else if (pernum < 49)
                        {
                            hediff = HediffMaker.MakeHediff(TMSLV2[randlist[i]], pawn, null);
                        }
                        else if (pernum < 79)
                        {
                            hediff = HediffMaker.MakeHediff(TMSLV3[randlist[i]], pawn, null);
                        }
                        else if (pernum < 99)
                        {
                            hediff = HediffMaker.MakeHediff(TMSLV4[randlist[i]], pawn, null);
                        }
                        else
                        {
                            hediff = HediffMaker.MakeHediff(TMSLV5[randlist[i]], pawn, null);
                        }
                        break;

                    case 4:
                    case 5:

                        if (pernum < 10)
                        {
                            hediff = HediffMaker.MakeHediff(TMSLV1[randlist[i]], pawn, null);
                        }
                        else if (pernum < 40)
                        {
                            hediff = HediffMaker.MakeHediff(TMSLV2[randlist[i]], pawn, null);
                        }
                        else if (pernum < 70)
                        {
                            hediff = HediffMaker.MakeHediff(TMSLV3[randlist[i]], pawn, null);
                        }
                        else if (pernum < 90)
                        {
                            hediff = HediffMaker.MakeHediff(TMSLV4[randlist[i]], pawn, null);
                        }
                        else
                        {
                            hediff = HediffMaker.MakeHediff(TMSLV5[randlist[i]], pawn, null);
                        }

                        break;
                    case 7:

                        if (pernum < 20)
                        {
                            hediff = HediffMaker.MakeHediff(TMSLV2[randlist[i]], pawn, null);
                        }
                        else if (pernum < 50)
                        {
                            hediff = HediffMaker.MakeHediff(TMSLV3[randlist[i]], pawn, null);
                        }
                        else if (pernum < 80)
                        {
                            hediff = HediffMaker.MakeHediff(TMSLV4[randlist[i]], pawn, null);
                        }
                        else
                        {
                            hediff = HediffMaker.MakeHediff(TMSLV5[randlist[i]], pawn, null);
                        }

                        break;
                    default:
                        return;
                }


                //BodyPartRecord part = null;
                //pawn.RaceProps.body.GetPartsWithTag(BodyPartTagDefOf.ConsciousnessSource).TryRandomElement(out part);
                //pawn.health.AddHediff(hediff, part, null, null);
                pawn.health.AddHediff(hediff);

            }

        }

        public override void DoEffectOn(Pawn user, Thing target)
        {
            Pawn pawn = (Pawn)target;
            if (pawn.Dead)
            {
                return;
            }
            else if(pawn.RaceProps.Humanlike)   // 
            {
                pawn.health.AddHediff(RimWorld.HediffDefOf.Hangover);
                pawn.health.AddHediff(RimWorld.HediffDefOf.FoodPoisoning);

                //plusLvHediff(ref pawn, 1);
                return;
            }

            plusLvHediff(ref pawn, 1);

        }

    }
}

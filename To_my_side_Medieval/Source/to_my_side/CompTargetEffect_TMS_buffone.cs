using System;
using System.Linq;
using RimWorld;
using Verse;

namespace to_my_side;

public class CompTargetEffect_TMS_buffone : CompTargetEffect
{
    public void plusLvHediff(ref Pawn pawn, int Ranks)
    {
        var array = new[]
        {
            HediffDefOf.TMS_armor_Sharp1,
            HediffDefOf.TMS_armor_Blunt1,
            HediffDefOf.TMS_armor_Heat1,
            HediffDefOf.TMS_HungerRate1,
            HediffDefOf.TMS_MeleeDodgeChance1,
            HediffDefOf.TMS_MoveSpeed1,
            HediffDefOf.TMS_ComfyTemperature1,
            HediffDefOf.TMS_BloodFiltration1,
            HediffDefOf.TMS_MeleeHitChance1,
            HediffDefOf.TMS_Manipulation1,
            HediffDefOf.TMS_Breathing1
        };
        var array2 = new[]
        {
            HediffDefOf.TMS_armor_Sharp2,
            HediffDefOf.TMS_armor_Blunt2,
            HediffDefOf.TMS_armor_Heat2,
            HediffDefOf.TMS_HungerRate2,
            HediffDefOf.TMS_MeleeDodgeChance2,
            HediffDefOf.TMS_MoveSpeed2,
            HediffDefOf.TMS_ComfyTemperature2,
            HediffDefOf.TMS_BloodFiltration2,
            HediffDefOf.TMS_MeleeHitChance2,
            HediffDefOf.TMS_Manipulation2,
            HediffDefOf.TMS_Breathing2
        };
        var array3 = new[]
        {
            HediffDefOf.TMS_armor_Sharp3,
            HediffDefOf.TMS_armor_Blunt3,
            HediffDefOf.TMS_armor_Heat3,
            HediffDefOf.TMS_HungerRate3,
            HediffDefOf.TMS_MeleeDodgeChance3,
            HediffDefOf.TMS_MoveSpeed3,
            HediffDefOf.TMS_ComfyTemperature3,
            HediffDefOf.TMS_BloodFiltration3,
            HediffDefOf.TMS_MeleeHitChance3,
            HediffDefOf.TMS_Manipulation3,
            HediffDefOf.TMS_Breathing3
        };
        var array4 = new[]
        {
            HediffDefOf.TMS_armor_Sharp4,
            HediffDefOf.TMS_armor_Blunt4,
            HediffDefOf.TMS_armor_Heat4,
            HediffDefOf.TMS_HungerRate4,
            HediffDefOf.TMS_MeleeDodgeChance4,
            HediffDefOf.TMS_MoveSpeed4,
            HediffDefOf.TMS_ComfyTemperature4,
            HediffDefOf.TMS_BloodFiltration4,
            HediffDefOf.TMS_MeleeHitChance4,
            HediffDefOf.TMS_Manipulation4,
            HediffDefOf.TMS_Breathing4
        };
        var array5 = new[]
        {
            HediffDefOf.TMS_armor_Sharp5,
            HediffDefOf.TMS_armor_Blunt5,
            HediffDefOf.TMS_armor_Heat5,
            HediffDefOf.TMS_HungerRate5,
            HediffDefOf.TMS_MeleeDodgeChance5,
            HediffDefOf.TMS_MoveSpeed5,
            HediffDefOf.TMS_ComfyTemperature5,
            HediffDefOf.TMS_BloodFiltration5,
            HediffDefOf.TMS_MeleeHitChance5,
            HediffDefOf.TMS_Manipulation5,
            HediffDefOf.TMS_Breathing5
        };
        var list = pawn.health.hediffSet.hediffs.ToList();
        foreach (var item in list)
        {
            for (var i = 0; i < 11; i++)
            {
                var hediff2 = HediffMaker.MakeHediff(array[i], pawn);
                var hediff3 = HediffMaker.MakeHediff(array2[i], pawn);
                var hediff4 = HediffMaker.MakeHediff(array3[i], pawn);
                var hediff5 = HediffMaker.MakeHediff(array4[i], pawn);
                var hediff6 = HediffMaker.MakeHediff(array5[i], pawn);
                if (item.LabelCap != hediff2.LabelCap && item.LabelCap != hediff3.LabelCap &&
                    item.LabelCap != hediff4.LabelCap && item.LabelCap != hediff5.LabelCap &&
                    item.LabelCap != hediff6.LabelCap)
                {
                    continue;
                }

                pawn.health.hediffSet.hediffs.Remove(item);
                item.PostRemoved();
                pawn.health.Notify_HediffChanged(null);
            }
        }

        var random = new Random();
        var array6 = new[]
        {
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
            10
        };
        array6.Shuffle();
        for (var j = 0; j < Ranks; j++)
        {
            var num = random.Next(0, 100);
            Hediff hediff;
            switch (Ranks)
            {
                default:
                    return;
                case 1:
                    hediff = num >= 40
                        ? num >= 70 ? num >= 90 ? HediffMaker.MakeHediff(array4[array6[j]], pawn) :
                        HediffMaker.MakeHediff(array3[array6[j]], pawn) :
                        HediffMaker.MakeHediff(array2[array6[j]], pawn)
                        : HediffMaker.MakeHediff(array[array6[j]], pawn);
                    break;
                case 2:
                case 3:
                    hediff = num >= 9
                        ? num >= 49 ? num >= 79 ? num >= 99 ? HediffMaker.MakeHediff(array5[array6[j]], pawn) :
                        HediffMaker.MakeHediff(array4[array6[j]], pawn) :
                        HediffMaker.MakeHediff(array3[array6[j]], pawn) :
                        HediffMaker.MakeHediff(array2[array6[j]], pawn)
                        : HediffMaker.MakeHediff(array[array6[j]], pawn);
                    break;
                case 4:
                case 5:
                    hediff = num >= 10
                        ? num >= 40 ? num >= 70 ? num >= 90 ? HediffMaker.MakeHediff(array5[array6[j]], pawn) :
                        HediffMaker.MakeHediff(array4[array6[j]], pawn) :
                        HediffMaker.MakeHediff(array3[array6[j]], pawn) :
                        HediffMaker.MakeHediff(array2[array6[j]], pawn)
                        : HediffMaker.MakeHediff(array[array6[j]], pawn);
                    break;
                case 7:
                    hediff = num >= 20
                        ? num >= 50 ? num >= 80 ? HediffMaker.MakeHediff(array5[array6[j]], pawn) :
                        HediffMaker.MakeHediff(array4[array6[j]], pawn) :
                        HediffMaker.MakeHediff(array3[array6[j]], pawn)
                        : HediffMaker.MakeHediff(array2[array6[j]], pawn);
                    break;
                case 6:
                    return;
            }

            pawn.health.AddHediff(hediff);
        }
    }

    public override void DoEffectOn(Pawn user, Thing target)
    {
        var pawn = (Pawn)target;
        if (pawn.Dead)
        {
            return;
        }

        if (pawn.RaceProps.Humanlike)
        {
            pawn.health.AddHediff(RimWorld.HediffDefOf.Hangover);
            pawn.health.AddHediff(RimWorld.HediffDefOf.FoodPoisoning);
        }
        else
        {
            plusLvHediff(ref pawn, 1);
        }
    }
}
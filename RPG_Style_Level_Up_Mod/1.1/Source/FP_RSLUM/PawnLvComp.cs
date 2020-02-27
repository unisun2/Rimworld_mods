using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace FP_RSLUM
{
    class PawnLvComp : ThingComp
    {
        public int level = -1;
        public int exp = 0;
        public int need_exp = 5000;
        public int StatPoint = 1;
        public int STR = -40;
        public int DEX = -40;
        public int AGL = -40;
        public int CON = -40;
        public int INT = -40;
        public int CHA = -40;

        public int exptick = 0;

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            if (this.level == -1)
            {
                if (FP_RSLUM_setting.FlatStartingStat)
                {
                    this.level = 0;
                    STR = 0;
                    DEX = 0;
                    AGL = 0;
                    CON = 0;
                    INT = 0;
                    CHA = 0;
                }
                else
                {
                    this.level += 1;
                    STR = Rand.Range(FP_RSLUM_setting.Startingstat_min, FP_RSLUM_setting.Startingstat_max);
                    DEX = Rand.Range(FP_RSLUM_setting.Startingstat_min, FP_RSLUM_setting.Startingstat_max);
                    AGL = Rand.Range(FP_RSLUM_setting.Startingstat_min, FP_RSLUM_setting.Startingstat_max);
                    CON = Rand.Range(FP_RSLUM_setting.Startingstat_min, FP_RSLUM_setting.Startingstat_max);
                    INT = Rand.Range(FP_RSLUM_setting.Startingstat_min, FP_RSLUM_setting.Startingstat_max);
                    CHA = Rand.Range(FP_RSLUM_setting.Startingstat_min, FP_RSLUM_setting.Startingstat_max);

                }

            }
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look<int>(ref this.level, "FP_RSLUM_level", 0, true);
            Scribe_Values.Look<int>(ref this.exp, "FP_RSLUM_exp", 0, true);
            Scribe_Values.Look<int>(ref this.need_exp, "FP_RSLUM_need_exp", 10000, true);
            Scribe_Values.Look<int>(ref this.StatPoint, "FP_RSLUM_StatPoint", 1, true);
            Scribe_Values.Look<int>(ref this.STR, "FP_RSLUM_STR", 0, true);
            Scribe_Values.Look<int>(ref this.DEX, "FP_RSLUM_DEX", 0, true);
            Scribe_Values.Look<int>(ref this.AGL, "FP_RSLUM_AGL", 0, true);
            Scribe_Values.Look<int>(ref this.CON, "FP_RSLUM_CON", 0, true);
            Scribe_Values.Look<int>(ref this.INT, "FP_RSLUM_INT", 0, true);
            Scribe_Values.Look<int>(ref this.CHA, "FP_RSLUM_CHA", 0, true);
        }

        public bool canlevelup()
        {
            return exp > need_exp;
        }

        public void levelup()
        {
            bool needhediff = (exp > need_exp);

            while (exp > need_exp)
            {
                this.level += 1;
                this.StatPoint += 1;
                exp -= need_exp;
                need_exp = (int)Math.Ceiling(10000 * Math.Log(level + 1) * (1 + 0.01 * level));
            }
        }

        public override void CompTick()
        {
            base.CompTick();
            if (parent.def.race.Animal)
            {
                exptick++;
                if (exptick > 600)
                {
                    exptick = 0;
                    this.exp += FP_RSLUM_setting.AnimalEXPPerTick;
                }
            }
        }


    }
}

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
				if(FP_RSLUM_setting.FlatStartingStat){
					this.level = 0;
					STR = 0;
					DEX = 0;
					AGL = 0;
					CON = 0;
					INT = 0;
					CHA = 0;
				}
				else{
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
            Scribe_Values.Look<int>(ref this.level, "level", 0, true);
            Scribe_Values.Look<int>(ref this.exp, "exp", 0, true);
            Scribe_Values.Look<int>(ref this.need_exp, "need_exp", 10000, true);
            Scribe_Values.Look<int>(ref this.StatPoint, "StatPoint", 1, true);
            Scribe_Values.Look<int>(ref this.STR, "STR", 0, true);
            Scribe_Values.Look<int>(ref this.DEX, "DEX", 0, true);
            Scribe_Values.Look<int>(ref this.AGL, "AGL", 0, true);
            Scribe_Values.Look<int>(ref this.CON, "CON", 0, true);
            Scribe_Values.Look<int>(ref this.INT, "INT", 0, true);
            Scribe_Values.Look<int>(ref this.CHA, "CHA", 0, true);
        }

        public bool canlevelup()
        {
            return exp > need_exp;
        }

        public void levelup()
        {
            bool needhediff = (exp > need_exp);

            while(exp > need_exp)
            {
                this.level += 1;
                this.StatPoint += 1;
                exp -= need_exp;
                need_exp = (int)Math.Ceiling(10000 * Math.Log(level + 1) * (1 + 0.01 * level));
            }

            if (needhediff)
            {
                Hediff hediff = HediffMaker.MakeHediff(HediffDefOf.RSLUM_LVUP, (Pawn)this.parent, null);
                hediff.Severity = 0.1f;
                ((Pawn)this.parent).health.AddHediff(hediff, null, null, null);
            }
        }

        public override void CompTick()
        {
            base.CompTick();
            if (parent.def.race.Animal)
            {
                exptick++;
                if(exptick > 600)
                {
                    exptick = 0;
                    this.exp += FP_RSLUM_setting.AnimalEXPPerTick;
                }
            }
        }


    }
}

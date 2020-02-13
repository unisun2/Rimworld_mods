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
        public int StatPoint = 0;
        public int STR = -20;
        public int DEX = -20;
        public int AGL = -20;
        public int CON = -20;
        public int INT = -20;
        public int CHA = -20;

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
                int plus;

                for (int i = 0; i < 50; i++)
                {
                    plus = Rand.Range(0, 24);
                    switch (plus)
                    {
                        case 0: STR++; break;
                        case 1: DEX++; break;
                        case 2: AGL++; break;
                        case 3: CON++; break;
                        case 4: INT++; break;
                        case 5: CHA++; break;
                        case 6: STR += 2; break;
                        case 7: DEX += 2; break;
                        case 8: AGL += 2; break;
                        case 9: CON += 2; break;
                        case 10: INT += 2; break;
                        case 11: CHA += 2; break;
                        case 12: STR += 3; break;
                        case 13: DEX += 3; break;
                        case 14: AGL += 3; break;
                        case 15: CON += 3; break;
                        case 16: INT += 3; break;
                        case 17: CHA += 3; break;
			            case 18: STR += 4; break;
                        case 19: DEX += 4; break;
                        case 20: AGL += 4; break;
                        case 21: CON += 4; break;
                        case 22: INT += 4; break;
                        case 23: CHA += 4; break;
                        default: break;
                    }
                }
				}
                
            }
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look<int>(ref this.level, "level", 0, false);
            Scribe_Values.Look<int>(ref this.exp, "exp", 0, false);
            Scribe_Values.Look<int>(ref this.need_exp, "need_exp", 10000, false);
            Scribe_Values.Look<int>(ref this.StatPoint, "StatPoint", 1, false);
            Scribe_Values.Look<int>(ref this.STR, "STR", 0, false);
            Scribe_Values.Look<int>(ref this.DEX, "DEX", 0, false);
            Scribe_Values.Look<int>(ref this.AGL, "AGL", 0, false);
            Scribe_Values.Look<int>(ref this.CON, "CON", 0, false);
            Scribe_Values.Look<int>(ref this.INT, "INT", 0, false);
            Scribe_Values.Look<int>(ref this.CHA, "CHA", 0, false);
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
                exp++;
            }
        }


    }
}

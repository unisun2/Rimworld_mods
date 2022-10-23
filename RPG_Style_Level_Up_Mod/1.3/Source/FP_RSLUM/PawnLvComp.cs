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
        public bool STRauto = false;
        public bool DEXauto = false;
        public bool AGLauto = false;
        public bool CONauto = false;
        public bool INTauto = false;
        public bool CHAauto = false;

        public int exptick = 0;
        public int healtick = 0;

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            int tempcount = 0;

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

                //if (FP_RSLUM_setting.FP_RSLUM_LvAv == -1 && FP_RSLUM_setting.LevelScaling != 0 && Find.World != null)

                if (FP_RSLUM_setting.LevelScaling == 0)
                    FP_RSLUM_setting.FP_RSLUM_LvAv = 0;
                if (FP_RSLUM_setting.FP_RSLUM_LvAv == -1)
                {
                    // old style. Some mod touch Find.World and Crashes occur...
                    if (Find.World != null)
                    {
                        //Verse.Log.Message("old style - g", false);
                        int templv = 0;
                        IEnumerable<Pawn> Pawns = from p in Find.World.worldPawns.AllPawnsAlive
                                                  where p.IsColonist && (p.Faction == Faction.OfPlayer)
                                                  select p;

                        if (Pawns.Count() != 0)
                        {
                            foreach (Pawn pawn in Pawns)
                            {
                                PawnLvComp pawnlvcomp = pawn.TryGetComp<PawnLvComp>();
                                if (pawnlvcomp != null)
                                {
                                    templv += pawnlvcomp.level;
                                    tempcount++;
                                }
                            }
                            if (tempcount == 0)
                                FP_RSLUM_setting.FP_RSLUM_LvAv = 0;
                            else FP_RSLUM_setting.FP_RSLUM_LvAv = (templv / tempcount) * FP_RSLUM_setting.LevelScaling / 100;
                        }
                        else
                        {
                            FP_RSLUM_setting.FP_RSLUM_LvAv = 0;
                        }
                    }
                    // new style.
                    else if (PawnsFinder.AllMaps_FreeColonistsSpawned != null)
                    {
                        //Verse.Log.Message("new style - g", false);
                        int templv = 0;
                        
                        List<Pawn> pawns = new List<Pawn>();

                        foreach (Pawn pawn in PawnsFinder.AllMaps_FreeColonistsSpawned)
                        {
                            PawnLvComp pawnlvcomp = pawn.TryGetComp<PawnLvComp>();
                            if (pawnlvcomp != null)
                            {
                                templv += pawnlvcomp.level;
                                tempcount++;
                            }
                        }

                        if (tempcount == 0)
                            FP_RSLUM_setting.FP_RSLUM_LvAv = 0;
                        else FP_RSLUM_setting.FP_RSLUM_LvAv = (templv / tempcount) * FP_RSLUM_setting.LevelScaling / 100;


                        //Verse.Log.Message(String.Format("LvAv {} , count {}", FP_RSLUM_setting.FP_RSLUM_LvAv, tempcount), false);
                    }
                    else
                    {
                       // Verse.Log.Message("new, old both failed", false);
                        FP_RSLUM_setting.FP_RSLUM_LvAv = 0;
                    }


                }

                if (this.level > 0)
                {
                    //Verse.Log.Message("level up!!" + FP_RSLUM_setting.FP_RSLUM_LvAv, false);
                    this.level = FP_RSLUM_setting.FP_RSLUM_LvAv;

                    int tempstat = FP_RSLUM_setting.FP_RSLUM_LvAv;

                    while (true)
                    {
                        this.STR += FP_RSLUM_setting.thisispi[FP_RSLUM_setting.piindex];
                        tempstat -= FP_RSLUM_setting.thisispi[FP_RSLUM_setting.piindex++];
                        if (FP_RSLUM_setting.piindex == 1000)
                            FP_RSLUM_setting.piindex = 0;
                        if (tempstat <= 0)
                            break;
                        this.DEX += FP_RSLUM_setting.thisispi[FP_RSLUM_setting.piindex];
                        tempstat -= FP_RSLUM_setting.thisispi[FP_RSLUM_setting.piindex++];
                        if (FP_RSLUM_setting.piindex == 1000)
                            FP_RSLUM_setting.piindex = 0;
                        if (tempstat <= 0)
                            break;
                        this.AGL += FP_RSLUM_setting.thisispi[FP_RSLUM_setting.piindex];
                        tempstat -= FP_RSLUM_setting.thisispi[FP_RSLUM_setting.piindex++];
                        if (FP_RSLUM_setting.piindex == 1000)
                            FP_RSLUM_setting.piindex = 0;
                        if (tempstat <= 0)
                            break;
                        this.CON += FP_RSLUM_setting.thisispi[FP_RSLUM_setting.piindex];
                        tempstat -= FP_RSLUM_setting.thisispi[FP_RSLUM_setting.piindex++];
                        if (FP_RSLUM_setting.piindex == 1000)
                            FP_RSLUM_setting.piindex = 0;
                        if (tempstat <= 0)
                            break;
                        this.INT += FP_RSLUM_setting.thisispi[FP_RSLUM_setting.piindex];
                        tempstat -= FP_RSLUM_setting.thisispi[FP_RSLUM_setting.piindex++];
                        if (FP_RSLUM_setting.piindex == 1000)
                            FP_RSLUM_setting.piindex = 0;
                        if (tempstat <= 0)
                            break;
                        this.CHA += FP_RSLUM_setting.thisispi[FP_RSLUM_setting.piindex];
                        tempstat -= FP_RSLUM_setting.thisispi[FP_RSLUM_setting.piindex++];
                        if (FP_RSLUM_setting.piindex == 1000)
                            FP_RSLUM_setting.piindex = 0;
                        if (tempstat <= 0)
                            break;
                    }

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

            Scribe_Values.Look<bool>(ref this.STRauto, "FP_RSLUM_STRauto", false, true);
            Scribe_Values.Look<bool>(ref this.DEXauto, "FP_RSLUM_DEXauto", false, true);
            Scribe_Values.Look<bool>(ref this.AGLauto, "FP_RSLUM_AGLauto", false, true);
            Scribe_Values.Look<bool>(ref this.CONauto, "FP_RSLUM_CONauto", false, true);
            Scribe_Values.Look<bool>(ref this.INTauto, "FP_RSLUM_INTauto", false, true);
            Scribe_Values.Look<bool>(ref this.CHAauto, "FP_RSLUM_CHAauto", false, true);
        }


        public void levelup()
        {

            while (exp > need_exp && ((this.level < FP_RSLUM_setting.MaxLevel) || FP_RSLUM_setting.MaxLevel == 0))
            {
                this.level += 1;
                this.StatPoint += 1;
                exp -= need_exp;
                need_exp = (int)Math.Ceiling(10000 * Math.Log(level + 1) * (1 + 0.01 * level));

                if (this.STRauto)
                {
                    this.StatPoint -= 1;
                    this.STR += 1;
                }
                else if (this.DEXauto)
                {
                    this.StatPoint -= 1;
                    this.DEX += 1;
                }
                else if (this.AGLauto)
                {
                    this.StatPoint -= 1;
                    this.AGL += 1;
                }
                else if (this.CONauto)
                {
                    this.StatPoint -= 1;
                    this.CON += 1;
                }
                else if (this.INTauto)
                {
                    this.StatPoint -= 1;
                    this.INT += 1;
                }
                else if (this.CHAauto)
                {
                    this.StatPoint -= 1;
                    this.CHA += 1;
                }
            }
        }

        public override void CompTick()
        {
            base.CompTick();
            if (parent.Spawned)
            {
                if (parent.def.race.Animal)
                {
                    exptick++;
                    if (exptick > 600)
                    {
                        exptick = 0;
                        this.exp += FP_RSLUM_setting.AnimalEXPPerTick;
                        FP_RSLUM_setting.FP_RSLUM_LvAv = -1; // reset calculated LvAv.. XD
                        if ((this.exp > this.need_exp) && (this.level < FP_RSLUM_setting.MaxLevel || FP_RSLUM_setting.MaxLevel == 0))
                        {
                            this.levelup();
                        }
                    }
                }

                if (this.CON > 200)
                {
                    healtick++;
                    if (healtick > 1200)
                    {
                        healtick = 0;
                        Pawn pawn = this.parent as Pawn;
                        if (pawn.health != null)
                        {
                            if (pawn.health.hediffSet.GetInjuriesTendable() != null && pawn.health.hediffSet.GetInjuriesTendable().Count<Hediff_Injury>() > 0)
                            {
                                foreach (Hediff_Injury injury in pawn.health.hediffSet.GetInjuriesTendable())
                                {
                                    if (injury.Bleeding)
                                    {
                                        //float temp = injury.BleedRate;

                                        injury.Severity = (float)Math.Max(0f, injury.Severity - (0.01 * (this.CON - 100)));
                                        //Log.Message(temp.ToString() + " -> " + injury.BleedRate.ToString());
                                        break;
                                    }
                                }

                            }
                        }
                    }
                }
            }

        }
    }
}

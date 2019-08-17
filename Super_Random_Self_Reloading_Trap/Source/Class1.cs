using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Verse;
using Verse.Sound;
using HugsLib;
using Verse.AI;
using System.Linq;

namespace SRFPSRT
{
    public class Building_SRFPSRT : Building_Trap
    {
        private static readonly FloatRange DamageRandomFactorRange = new FloatRange(0.8f, 1.2f);

        private static readonly float DamageCount = 5f;

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            if (!respawningAfterLoad)
            {
                SoundDefOf.TrapArm.PlayOneShot(new TargetInfo(base.Position, map, false));
            }
            
        }

        protected override void SpringSub(Pawn p)
        {
            SoundDefOf.TrapSpring.PlayOneShot(new TargetInfo(base.Position, base.Map, false));
            if (p == null)
            {
                return;
            }
            float num = this.GetStatValue(StatDefOf.TrapMeleeDamage, true) * Building_SRFPSRT.DamageRandomFactorRange.RandomInRange * SRFPSRT_settings.Gettrapdamage();
            float num2 = num / Building_SRFPSRT.DamageCount;
            float armorPenetration = num2 * 0.015f * SRFPSRT_settings.Getarmorpenetrate();
            int num3 = 0;
            while ((float)num3 < Building_SRFPSRT.DamageCount)
            {
                DamageInfo dinfo = new DamageInfo(DamageDefOf.Stab, num2, armorPenetration, -1f, this, null, null, DamageInfo.SourceCategory.ThingOrUnknown, null);
                DamageWorker.DamageResult damageResult = p.TakeDamage(dinfo);
                if (num3 == 0)
                {
                    BattleLogEntry_DamageTaken battleLogEntry_DamageTaken = new BattleLogEntry_DamageTaken(p, RulePackDefOf.DamageEvent_TrapSpike, null);
                    Find.BattleLog.Add(battleLogEntry_DamageTaken);
                    damageResult.AssociateWithLog(battleLogEntry_DamageTaken);
                }
                num3++;
            }
            Map map = base.Map;
            IntVec3 loc = this.Position;
            Thing thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named("Building_SRFPSRTunarmed"), this.Stuff), loc, map, WipeMode.Vanish);
            thing.SetFaction(Faction.OfPlayer, null);
        }

    }


    public class Building_SRFPSRT_flame : Building_Trap
    {
        private static readonly FloatRange DamageRandomFactorRange = new FloatRange(0.8f, 1.2f);

        private static readonly float DamageCount = 5f;

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            if (!respawningAfterLoad)
            {
                SoundDefOf.TrapArm.PlayOneShot(new TargetInfo(base.Position, map, false));
            }

        }

        protected override void SpringSub(Pawn p)
        {
            SoundDefOf.TrapSpring.PlayOneShot(new TargetInfo(base.Position, base.Map, false));
            if (p == null)
            {
                return;
            }
            float num = this.GetStatValue(StatDefOf.TrapMeleeDamage, true) * DamageRandomFactorRange.RandomInRange * SRFPSRT_settings.Gettrapdamage();
            float num2 = num / DamageCount;
            float armorPenetration = num2 * 0.015f * SRFPSRT_settings.Getarmorpenetrate();
            int num3 = 0;
            while ((float)num3 < DamageCount)
            {
                DamageInfo dinfo = new DamageInfo(DamageDefOf.Flame, num2, armorPenetration, -1f, this, null, null, DamageInfo.SourceCategory.ThingOrUnknown, null);
                DamageWorker.DamageResult damageResult = p.TakeDamage(dinfo);
                if (num3 == 0)
                {
                    BattleLogEntry_DamageTaken battleLogEntry_DamageTaken = new BattleLogEntry_DamageTaken(p, RulePackDefOf.DamageEvent_TrapSpike, null);
                    Find.BattleLog.Add(battleLogEntry_DamageTaken);
                    damageResult.AssociateWithLog(battleLogEntry_DamageTaken);
                }
                num3++;
            }
            Map map = base.Map;
            IntVec3 loc = this.Position;
            Thing thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named("Building_SRFPSRTunarmed"), this.Stuff), loc, map, WipeMode.Vanish);
            thing.SetFaction(Faction.OfPlayer, null);
        }

    }

    public class Building_SRFPSRT_flash : Building_Trap
    {
        private static readonly FloatRange DamageRandomFactorRange = new FloatRange(0.8f, 1.2f);

        private static readonly float DamageCount = 5f;

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            if (!respawningAfterLoad)
            {
                SoundDefOf.TrapArm.PlayOneShot(new TargetInfo(base.Position, map, false));
            }

        }

        protected override void SpringSub(Pawn p)
        {
            IEnumerable<BodyPartRecord> enumerable = from x in p.health.hediffSet.GetNotMissingParts(BodyPartHeight.Undefined, BodyPartDepth.Undefined, null, null)
                                                     where x.def == BodyPartDefOf.Eye
                                                     select x;
            foreach (BodyPartRecord current in enumerable)
            {
                DamageInfo dinfo = new DamageInfo(DamageDefOf.Burn, 5, 0.5f, -1f, this, current, null, DamageInfo.SourceCategory.ThingOrUnknown, null);
                DamageWorker.DamageResult damageResult = p.TakeDamage(dinfo);
            }


            SoundDefOf.TrapSpring.PlayOneShot(new TargetInfo(base.Position, base.Map, false));
            if (p == null)
            {
                return;
            }
            float num = this.GetStatValue(StatDefOf.TrapMeleeDamage, true) * DamageRandomFactorRange.RandomInRange * SRFPSRT_settings.Gettrapdamage();
            float num2 = num / DamageCount;
            float armorPenetration = num2 * 0.015f * SRFPSRT_settings.Getarmorpenetrate();
            int num3 = 0;
            while ((float)num3 < DamageCount)
            {

                DamageInfo dinfo = new DamageInfo(DamageDefOf.Stab, num2, armorPenetration, -1f, this, null, null, DamageInfo.SourceCategory.ThingOrUnknown, null);
                DamageWorker.DamageResult damageResult = p.TakeDamage(dinfo);
                if (num3 == 0)
                {
                    BattleLogEntry_DamageTaken battleLogEntry_DamageTaken = new BattleLogEntry_DamageTaken(p, RulePackDefOf.DamageEvent_TrapSpike, null);
                    Find.BattleLog.Add(battleLogEntry_DamageTaken);
                    damageResult.AssociateWithLog(battleLogEntry_DamageTaken);
                }
                num3++;
            }
            Map map = base.Map;
            IntVec3 loc = this.Position;
            Thing thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named("Building_SRFPSRTunarmed"), this.Stuff), loc, map, WipeMode.Vanish);
            thing.SetFaction(Faction.OfPlayer, null);
        }

    }

    public class Building_SRFPSRT_poison : Building_Trap
    {
        private static readonly FloatRange DamageRandomFactorRange = new FloatRange(0.8f, 1.2f);

        private static readonly float DamageCount = 5f;

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            if (!respawningAfterLoad)
            {
                SoundDefOf.TrapArm.PlayOneShot(new TargetInfo(base.Position, map, false));
            }

        }

        protected override void SpringSub(Pawn p)
        {
            SoundDefOf.TrapSpring.PlayOneShot(new TargetInfo(base.Position, base.Map, false));
            if (p == null)
            {
                return;
            }
            float num = this.GetStatValue(StatDefOf.TrapMeleeDamage, true) * DamageRandomFactorRange.RandomInRange * SRFPSRT_settings.Gettrapdamage();
            float num2 = num / DamageCount;
            float armorPenetration = num2 * 0.015f * SRFPSRT_settings.Getarmorpenetrate();
            int num3 = 0;
            while ((float)num3 < DamageCount)
            {

                p.health.AddHediff(HediffDefOf.FoodPoisoning, null, null, null);
                DamageInfo dinfo = new DamageInfo(DamageDefOf.Stab, num2, armorPenetration, -1f, this, null, null, DamageInfo.SourceCategory.ThingOrUnknown, null);
                DamageWorker.DamageResult damageResult = p.TakeDamage(dinfo);
                if (num3 == 0)
                {
                    BattleLogEntry_DamageTaken battleLogEntry_DamageTaken = new BattleLogEntry_DamageTaken(p, RulePackDefOf.DamageEvent_TrapSpike, null);
                    Find.BattleLog.Add(battleLogEntry_DamageTaken);
                    damageResult.AssociateWithLog(battleLogEntry_DamageTaken);
                }
                num3++;
            }
            Map map = base.Map;
            IntVec3 loc = this.Position;
            Thing thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named("Building_SRFPSRTunarmed"), this.Stuff), loc, map, WipeMode.Vanish);
            thing.SetFaction(Faction.OfPlayer, null);
        }

    }


    public class Building_SRFPSRT_summon : Building_Trap
    {
        private static readonly FloatRange DamageRandomFactorRange = new FloatRange(0.8f, 1.2f);

        private static readonly float DamageCount = 5f;

        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            if (!respawningAfterLoad)
            {
                SoundDefOf.TrapArm.PlayOneShot(new TargetInfo(base.Position, map, false));
            }

        }

        protected override void SpringSub(Pawn p)
        {

            int temp = UnityEngine.Random.Range(0, 5);

            if(temp == 0)
            {
                for(int i = 0; i < 3; i++)
                {
                    Pawn pawn = PawnGenerator.GeneratePawn(PawnKindDef.Named("Rat"), null);
                    pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Berserk, null, true, false, null, false);
                    IntVec3 spawncell = CellFinder.RandomClosewalkCellNear(this.Position, this.Map, 5, (IntVec3 c) => c.Standable(this.Map) &&
                      this.Map.reachability.CanReach(c, this, PathEndMode.Touch, TraverseParms.For(TraverseMode.PassDoors, Danger.Deadly, false)));
                    Thing spawnedCreature = GenSpawn.Spawn(pawn, spawncell, this.Map, WipeMode.Vanish);
                    pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Berserk, null, true, false, null, false);
                }
            }
            else if(temp == 1)
            {
                for(int i = 0; i < 2; i++)
                {
                    Pawn pawn = PawnGenerator.GeneratePawn(PawnKindDef.Named("Cat"), null);
                    pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Berserk, null, true, false, null, false);
                    IntVec3 spawncell = CellFinder.RandomClosewalkCellNear(this.Position, this.Map, 5, (IntVec3 c) => c.Standable(this.Map) &&
                      this.Map.reachability.CanReach(c, this, PathEndMode.Touch, TraverseParms.For(TraverseMode.PassDoors, Danger.Deadly, false)));
                    Thing spawnedCreature = GenSpawn.Spawn(pawn, spawncell, this.Map, WipeMode.Vanish);
                    pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Berserk, null, true, false, null, false);
                }
            }
            else if (temp == 2) 
            {
                Pawn pawn = PawnGenerator.GeneratePawn(PawnKindDef.Named("Cougar"), null);
                pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Berserk, null, true, false, null, false);
                IntVec3 spawncell = CellFinder.RandomClosewalkCellNear(this.Position, this.Map, 5, (IntVec3 c) => c.Standable(this.Map) &&
                  this.Map.reachability.CanReach(c, this, PathEndMode.Touch, TraverseParms.For(TraverseMode.PassDoors, Danger.Deadly, false)));
                Thing spawnedCreature = GenSpawn.Spawn(pawn, spawncell, this.Map, WipeMode.Vanish);
                pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Berserk, null, true, false, null, false);
            }
            else if(temp == 3)
            {
                Pawn pawn = PawnGenerator.GeneratePawn(PawnKindDef.Named("WildBoar"), null);
                pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Berserk, null, true, false, null, false);
                IntVec3 spawncell = CellFinder.RandomClosewalkCellNear(this.Position, this.Map, 5, (IntVec3 c) => c.Standable(this.Map) &&
                  this.Map.reachability.CanReach(c, this, PathEndMode.Touch, TraverseParms.For(TraverseMode.PassDoors, Danger.Deadly, false)));
                Thing spawnedCreature = GenSpawn.Spawn(pawn, spawncell, this.Map, WipeMode.Vanish);
                pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Berserk, null, true, false, null, false);
            }
            else
            {
                Pawn pawn = PawnGenerator.GeneratePawn(PawnKindDef.Named("Bear_Grizzly"), null);
                pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Berserk, null, true, false, null, false);
                IntVec3 spawncell = CellFinder.RandomClosewalkCellNear(this.Position, this.Map, 5, (IntVec3 c) => c.Standable(this.Map) &&
                  this.Map.reachability.CanReach(c, this, PathEndMode.Touch, TraverseParms.For(TraverseMode.PassDoors, Danger.Deadly, false)));
                Thing spawnedCreature = GenSpawn.Spawn(pawn, spawncell, this.Map, WipeMode.Vanish);
                pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Berserk, null, true, false, null, false);
            }

            SoundDefOf.TrapSpring.PlayOneShot(new TargetInfo(base.Position, base.Map, false));
            if (p == null)
            {
                return;
            }
            float num = this.GetStatValue(StatDefOf.TrapMeleeDamage, true) * DamageRandomFactorRange.RandomInRange * SRFPSRT_settings.Gettrapdamage();
            float num2 = num / DamageCount;
            float armorPenetration = num2 * 0.015f * SRFPSRT_settings.Getarmorpenetrate();
            int num3 = 0;
            while ((float)num3 < DamageCount)
            {

                DamageInfo dinfo = new DamageInfo(DamageDefOf.Stab, num2, armorPenetration, -1f, this, null, null, DamageInfo.SourceCategory.ThingOrUnknown, null);
                DamageWorker.DamageResult damageResult = p.TakeDamage(dinfo);
                if (num3 == 0)
                {
                    BattleLogEntry_DamageTaken battleLogEntry_DamageTaken = new BattleLogEntry_DamageTaken(p, RulePackDefOf.DamageEvent_TrapSpike, null);
                    Find.BattleLog.Add(battleLogEntry_DamageTaken);
                    damageResult.AssociateWithLog(battleLogEntry_DamageTaken);
                }
                num3++;
            }
            Map map = base.Map;
            IntVec3 loc = this.Position;
            Thing thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named("Building_SRFPSRTunarmed"), this.Stuff), loc, map, WipeMode.Vanish);
            thing.SetFaction(Faction.OfPlayer, null);
        }

    }



}

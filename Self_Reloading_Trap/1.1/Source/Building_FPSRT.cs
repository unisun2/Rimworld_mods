using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace FPSRT
{
    public class Building_FPSRT : Building_Trap
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
            float num = this.GetStatValue(StatDefOf.TrapMeleeDamage, true) * Building_FPSRT.DamageRandomFactorRange.RandomInRange * (float)FP_SelfReloadTrap_setting.trapdamage / 100f;
            float num2 = num / Building_FPSRT.DamageCount;
            float armorPenetration = num2 * 0.015f * (float)FP_SelfReloadTrap_setting.armorpenetrate / 100f;
            int num3 = 0;
            while ((float)num3 < Building_FPSRT.DamageCount)
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
            Thing thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named("Building_FPSRTunarmed"), this.Stuff), loc, map, WipeMode.Vanish);
            thing.SetFaction(Faction.OfPlayer, null);
        }

    }
}

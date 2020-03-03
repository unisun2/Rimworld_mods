using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Verse;
using Verse.Sound;
using System.Text;

namespace FPSRT
{
    class Building_FPSRTunarmed : Building_Trap
    {
        private static readonly FloatRange DamageRandomFactorRange = new FloatRange(0.1f, 0.2f);

        public int FPSRT_arming = 0;

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
            // do nothing
        }

        public override void Tick()
        {
            base.Tick();
            if (this.Spawned)
            {
                FPSRT_arming++;

                if (FPSRT_arming > (FP_SelfReloadTrap_setting.traparmingtime * 60))
                {
                    Map map = base.Map;
                    IntVec3 loc = this.Position;
                    Thing thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named("Building_FPSRT"), this.Stuff), loc, map, WipeMode.Vanish);
                    thing.SetFaction(Faction.OfPlayer, null);
                }
            }
        }


        public override string GetInspectString()
        {

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(base.GetInspectString());

            string newDesc = "";

            newDesc = "charging... : " + ((int)((FP_SelfReloadTrap_setting.traparmingtime * 60 - FPSRT_arming))).ToStringSecondsFromTicks();

            stringBuilder.Append(newDesc);

            return stringBuilder.ToString();
            
        }
    }
}

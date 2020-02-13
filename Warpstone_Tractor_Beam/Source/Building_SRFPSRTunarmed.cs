using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Verse;
using Verse.Sound;
using System.Text;

namespace SRFPSRT
{
    class Building_SRFPSRTunarmed : Building_Trap
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

                if (FPSRT_arming > (SRFPSRT_settings.Gettraparmingtime() * 60))
                {
                    Map map = base.Map;
                    IntVec3 loc = this.Position;

                    int temp = UnityEngine.Random.Range(0, 5);

                    String itsname = "";

                    switch (temp)
                    {
                        case 0:
                            itsname = "Building_SRFPSRT_Normal";
                            break;
                        case 1:
                            itsname = "Building_SRFPSRT_flame";
                            break;
                        case 2:
                            itsname = "Building_SRFPSRT_flash";
                            break;
                        case 3:
                            itsname = "Building_SRFPSRT_poison";
                            break;
                        default:
                            itsname = "Building_SRFPSRT_summon";
                            break;

                    }
                    Thing thing = GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named(itsname), this.Stuff), loc, map, WipeMode.Vanish);
                    thing.SetFaction(Faction.OfPlayer, null);

                }
            }
        }

        public override string GetInspectString()
        {

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(base.GetInspectString());

            string newDesc = "";

            newDesc = "charging... : " + ((int)(((SRFPSRT_settings.Gettraparmingtime()*60) - FPSRT_arming))).ToStringSecondsFromTicks();

            stringBuilder.Append(newDesc);

            return stringBuilder.ToString();
            
        }
    }
}

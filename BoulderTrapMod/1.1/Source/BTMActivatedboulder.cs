using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

/*
 * In-Game Time	Ticks	Real Time
1.5 Min		60	1s
1 Hour	60 Min	2,500	41s
1 Day	24 Hours	60,000	16m 40s
1 Quadrum	15 Days	900,000	4h 10m 0s
1 Year	4 Quadrums	3,600,000	16h 40m 0s
*/

namespace BoulderTrapMod
{
    public class BTMActivatedboulder : Projectile
    {
        private int spawnTick;

        private int tickCounter = 90;

        private bool checkbuilding = false;

        //private int wallcrashCounter = 30;

        //private Sustainer BTMsustainer;

        private static List<Thing> tmpThings = new List<Thing>();

        private static readonly Material ballimage1 = MaterialPool.MatFrom("Things/BTMbouldera");

        private static readonly Material ballimage2 = MaterialPool.MatFrom("Things/BTMboulderab");

        private static readonly Material ballimage3 = MaterialPool.MatFrom("Things/BTMboulderac");

        private Material next_draw = ballimage1;

        //public ProjectileHitFlags HitFlags = 0;



        public override void Tick()
        {
            base.Tick();
            if (this.Spawned)
            {

                if (tickCounter % 3 == 0)
                {
                    MoteMaker.ThrowDustPuff(this.Position, this.Map, 1f);
                }



                //MoteMaker.ThrowMetaPuff(this.Position.ToVector3(), this.Map);

                //MoteMaker.ThrowHeatGlow(this.Position, this.Map, 1f);

                if (tickCounter % 10 == 0)
                {
                    //Verse.Log.Message("nextdraw", false);
                    if (next_draw == ballimage1)
                        next_draw = ballimage2;
                    else if (next_draw == ballimage2)
                        next_draw = ballimage3;
                    else next_draw = ballimage1;

                    base.HitFlags = 0;

                    GenSpawn.Spawn(ThingMaker.MakeThing(ThingDef.Named("Filth_RubbleRock"), null), this.Position, this.Map);
                }



                tickCounter--;

                if (tickCounter <= 0)
                {
                    checkbuilding = true;
                    this.DamageCloseThings();

                    tickCounter = 12;
                }


                if (checkbuilding)
                {
                    Stopatwall();
                    //Verse.Log.Message("chk", false);
                }
            }

        }

        public override void Draw()
        {
            Mesh mesh = MeshPool.GridPlane(this.def.graphicData.drawSize);
            //Graphics.DrawMesh(mesh, this.DrawPos, this.ExactRotation, this.def.DrawMatSingle, 0);
            base.Comps_PostDraw();

            Graphics.DrawMesh(mesh, this.DrawPos, this.ExactRotation, next_draw, 0);
            //Graphic.Draw(GenThing.TrueCenter(Position, ExactRotation, this.def.graphicData.drawSize, AltitudeLayer.MoteOverhead.AltitudeFor()), ExactRotation, 0f);
        }

        private void Stopatwall()
        {
            bool flag = false;
            Building_Door building_Door = null;
            List<Thing> thingList = base.Position.GetThingList(Map);
            //Verse.Log.Message(thingList.Count.ToString() + " thingcount" , false);
            for (int i = 0; i < thingList.Count; i++)
            {
                Thing thing = thingList[i];
                if (thing.def.category == ThingCategory.Building)
                {
                    //Verse.Log.Message("building!", false);

                    if (thing.def.Fillage == FillCategory.Full)  // wall or door
                    {
                        building_Door = thing as Building_Door;
                        if (building_Door == null || !building_Door.Open)
                        {
                            //충돌
                            flag = true;
                            //Verse.Log.Message("building flagon!", true);
                        }
                    }

                    if (flag)
                    {
                        //Verse.Log.Message("stopatwall", true);
                        this.Impact(thing);
                        return;
                    }
                }
            }

        }




        private void DamageCloseThings()
        {
            int num = GenRadial.NumCellsInRadius(2f);
            for (int i = 0; i < num; i++)
            {
                IntVec3 intVec = this.Position + GenRadial.RadialPattern[i];
                if (this.Map != null)
                {
                    if (intVec.InBounds(this.Map))
                    {
                        Pawn firstPawn = intVec.GetFirstPawn(this.Map);
                        if (firstPawn == null || !firstPawn.Downed || !Rand.Bool)
                        {
                            float damageFactor;
                            //= GenMath.LerpDouble(0f, 4.2f, 1f, 0.2f, intVec.DistanceTo(this.Position));
                            //damageFactor = (UnityEngine.Random.Range(0f, 1f) * this.DamageAmount) * (2 - intVec.DistanceTo(this.Position));
                            damageFactor = Mathf.Max((float)(UnityEngine.Random.Range(0.5f, 1f) * this.DamageAmount * (2 - intVec.DistanceTo(this.Position))), 3f);
                            //Verse.Log.Message(damageFactor.ToString() + "df", false);

                            this.DoDamage(intVec, damageFactor);
                        }
                    }
                }
            }
        }


        private void DoDamage(IntVec3 c, float damageFactor)
        {
            BTMActivatedboulder.tmpThings.Clear();
            BTMActivatedboulder.tmpThings.AddRange(c.GetThingList(base.Map));
            Vector3 vector = c.ToVector3Shifted();
            Vector2 b = new Vector2(vector.x, vector.z);
            //float num = -this.realPosition.AngleTo(b) + 180f;
            for (int i = 0; i < BTMActivatedboulder.tmpThings.Count; i++)
            {
                BattleLogEntry_RangedImpact battleLogEntry_RangedImpact = null;
                switch (BTMActivatedboulder.tmpThings[i].def.category)
                {
                    case ThingCategory.Pawn:
                        {
                            Pawn pawn = (Pawn)BTMActivatedboulder.tmpThings[i];
                            battleLogEntry_RangedImpact = new BattleLogEntry_RangedImpact
                                (this.launcher, pawn, this.intendedTarget.Thing, this.equipmentDef, this.def, this.targetCoverDef);
                            Find.BattleLog.Add(battleLogEntry_RangedImpact);
                            if (pawn.RaceProps.baseHealthScale < 1f)
                            {
                                damageFactor *= pawn.RaceProps.baseHealthScale;
                            }
                            if (pawn.RaceProps.Animal)
                            {
                                damageFactor *= 0.75f;
                            }
                            if (pawn.Downed)
                            {
                                damageFactor *= 0.2f;
                            }
                            break;
                        }
                    case ThingCategory.Item:
                        damageFactor *= 0.68f;
                        break;
                    case ThingCategory.Building:
                        damageFactor *= 0.8f;
                        break;
                    case ThingCategory.Plant:
                        damageFactor *= 1.7f;
                        break;
                }
                //int amount = Mathf.Max(GenMath.RoundRandom(30f * damageFactor), 1);

                int amount = Mathf.RoundToInt(Mathf.Max(damageFactor, 5));
                Thing arg_184_0 = BTMActivatedboulder.tmpThings[i];
                //Verse.Log.Message(amount.ToString() + "da", false);

                arg_184_0.TakeDamage(new DamageInfo(DamageDefOf.Crush, amount, this.ArmorPenetration, 0, launcher, null, this.def, DamageInfo.SourceCategory.ThingOrUnknown, null)).AssociateWithLog(battleLogEntry_RangedImpact);

            }
            BTMActivatedboulder.tmpThings.Clear();
        }



    }
}

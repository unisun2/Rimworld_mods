using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;


namespace PLAHF
{
    public class PLAHFmanabooom : OrbitalStrike
    {
        private const int ImpactAreaRadius = 15;

        private const int ExplosionRadiusMin = 9;

        private const int ExplosionRadiusMax = 16;

        public const int EffectiveRadius = 33;

        public const int RandomFireRadius = 35;

        private const int BombIntervalTicks = 18;

        private const int StartRandomFireEveryTicks = 20;

        private static readonly SimpleCurve DistanceChanceFactor = new SimpleCurve
        {
            {
                new CurvePoint(0f, 1f),
                true
            },
            {
                new CurvePoint(15f, 0.1f),
                true
            }
        };

        public override void StartStrike()
        {
            base.StartStrike();
        }

        public override void Tick()
        {
            base.Tick();
            if (base.Destroyed)
            {
                return;
            }
            if (Find.TickManager.TicksGame % 300 == 0)
            {
                this.CreateBigExplosion();
                //Log.Message("CreateBigExplosion" + Find.TickManager.TicksGame + this, false);
            }
            else if (Find.TickManager.TicksGame % 40 == 0)
            {
                this.CreateRandomExplosion();
                //Log.Message("CreateRandomExplosion" + Find.TickManager.TicksGame + this, false);
            }
            else if (Find.TickManager.TicksGame % 38 == 0)
            {
                this.StartRandomFire();
                //Log.Message("StartRandomFire" + Find.TickManager.TicksGame + this, false);
            }
        }

        private void CreateBigExplosion()
        {
            MoteMaker.MakeBombardmentMote(base.Position, base.Map);
            float num = (float)Rand.Range(40, 55);
            IntVec3 center = base.Position;
            Map map = base.Map;
            float radius = num;
            DamageDef manabomb = DamageDefOf.PLAHFManaBomb;
            Thing instigator = this.instigator;
            ThingDef def = this.def;
            ThingDef weaponDef = this.weaponDef;
            GenExplosion.DoExplosion(center, map, radius, manabomb, instigator, -1, -1f, null, weaponDef, def, null, null, 0f, 1, false, null, 0f, 1, 0.1f, false);
        }

        private void CreateRandomExplosion()
        {
            

            IntVec3 intVec;

            CellFinder.TryFindRandomCellNear(base.Position, base.Map, 40, null, out intVec, -1);

            if (intVec == null)
                intVec = base.Position;

            float num = (float)Rand.Range(9, 50);
            IntVec3 center = intVec;
            Map map = base.Map;
            float radius = num;
            DamageDef manabomb = DamageDefOf.PLAHFManaBomb;

            Thing instigator = this.instigator;
            ThingDef def = this.def;
            ThingDef weaponDef = this.weaponDef;

            GenExplosion.DoExplosion(center, map, radius, manabomb, instigator, -1, -1f, null, weaponDef, def, null, null, 0f, 1, false, null, 0f, 1, 0f, false);

        }

        private void StartRandomFire()
        {
            IntVec3 c;

            CellFinder.TryFindRandomCellNear(base.Position, base.Map, 40, null, out c, -1);

            if (c == null)
                c = base.Position;
            FireUtility.TryStartFireIn(c, base.Map, Rand.Range(0.2f, 0.925f));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace PLAHF
{


    class PLAHF_superunstableThing : ThingWithComps
    {
        public ThingDef weaponDef = null;

        public override void Destroy(DestroyMode mode = DestroyMode.Vanish)
        {
            Map map = base.Map;
			base.Destroy(mode);

            float num = (float)Rand.Range(2, 4);
            IntVec3 center = base.Position;
            float radius = num;
            DamageDef manabomb = DamageDefOf.PLAHFManaBomb;
            ThingDef def = this.def;
            ThingDef weaponDef = this.weaponDef;
            GenExplosion.DoExplosion(center, map, radius, manabomb, null, -1, -1f, null, weaponDef, def, null, null, 0f, 1, false, null, 0f, 1, 0.1f, false);

        }
    }
}

using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Verse;
using RimWorld;
using System.Collections;
using System.Linq;

namespace FP_OGRE
{
    public class CompProperties_OGRE_Regen : CompProperties
    {
        public int rateInTicks = 1000;

        public CompProperties_OGRE_Regen()
        {
            this.compClass = typeof(CompOGRERegen);
        }
    }

    public class CompOGRERegen : ThingComp
    {
        public int tickCounter = 0;

        public CompProperties_OGRE_Regen Props
        {
            get
            {
                return (CompProperties_OGRE_Regen)this.props;
            }
        }

        protected int rateInTicks
        {
            get
            {
                return this.Props.rateInTicks;
            }
        }


        public override void CompTick()
        {
            tickCounter++;

            if (tickCounter >= rateInTicks)
            {
                Pawn pawn = this.parent as Pawn;

                if (pawn.health != null)
                {
                    if (pawn.health.hediffSet.GetBrain() != null)
                    {
                        Hediff_Injury hediff_Injury = this.FindPermanentInjury(pawn);
                        if (hediff_Injury != null)
                        {
                            hediff_Injury.Severity = (hediff_Injury.Severity / 2.0f);
                            return;
                        }
                    }

                    if (pawn.health.hediffSet.GetInjuriesTendable() != null && pawn.health.hediffSet.GetInjuriesTendable().Count<Hediff_Injury>() > 0)
                    {

                        foreach (Hediff_Injury injury in pawn.health.hediffSet.GetInjuriesTendable())
                        {
                            injury.Severity = Mathf.Clamp(injury.Severity - 0.1f, 0.0f, 1.0f);
                        }
                    }
                }
                tickCounter = 0;
            }
        }



        private Hediff_Injury FindPermanentInjury(Pawn pawn)
        {
            Hediff_Injury hediff_Injury = null;
            List<Hediff> hediffs = pawn.health.hediffSet.hediffs;
            for (int i = 0; i < hediffs.Count; i++)
            {
                Hediff_Injury hediff_Injury2 = hediffs[i] as Hediff_Injury;
                if (hediff_Injury2 != null && hediff_Injury2.Visible && hediff_Injury2.IsPermanent() && hediff_Injury2.def.everCurableByItem && hediff_Injury2.def.isBad)
                {
                    if (hediff_Injury == null || hediff_Injury2.Severity > hediff_Injury.Severity)
                    {
                        hediff_Injury = hediff_Injury2;
                    }
                }
            }
            return hediff_Injury;
        }

    }

}

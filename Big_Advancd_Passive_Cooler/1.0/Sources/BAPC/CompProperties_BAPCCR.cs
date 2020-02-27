using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using Verse;

namespace BAPC
{
    class CompProperties_BAPCCR : CompProperties
    {
        public CompProperties_BAPCCR()
        {
            this.compClass = typeof(BAPC_Collectrainwater);
        }
    }
}

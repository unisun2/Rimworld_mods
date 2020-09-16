using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.Sound;
using UnityEngine;
using RimWorld.Planet;

namespace FPDBDHook
{
    [DefOf]
    public static class Hook_JobDefOf
    {
        public static JobDef FPDBDTakeToMeatHook;
    }

    [DefOf]
    public static class HookThoughtDef
    {
        public static ThoughtDef KnowFPDBDHookedHumanlike;
        public static ThoughtDef KnowFPDBDHookedHumanlikeCannibal;
        public static ThoughtDef KnowFPDBDHookedHumanlikeBloodlust;

        public static ThoughtDef FPDBDHooked;
    }
    
    [DefOf]
    public static class HookSoundDef
    {
        public static SoundDef FPDBDHooksound;
    }

    [DefOf]
    public static class HookHediffDef
    {
        public static HediffDef FPDBDHookhediff;
    }


}

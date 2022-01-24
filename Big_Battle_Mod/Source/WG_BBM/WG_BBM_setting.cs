using System;
using UnityEngine;
using Verse;

namespace WG_BBM;

internal class WG_BBM_setting : ModSettings
{
    public static float enemypersent = 2f;

    public static float friendpersent = 1f;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref enemypersent, "WG_BBM_enemypersent", 2f);
        Scribe_Values.Look(ref friendpersent, "WG_BBM_friendpersent", 1f);
    }

    public void DoSettingsWindowContents(Rect canvas)
    {
        var listing_Standard = new Listing_Standard
        {
            ColumnWidth = canvas.width
        };
        listing_Standard.Begin(canvas);
        listing_Standard.GapLine();
        listing_Standard.Label("EnemyRaidPoints".Translate(Math.Round(enemypersent * 100f)));
        enemypersent = listing_Standard.Slider(enemypersent, 1f, 3f);
        listing_Standard.GapLine();
        listing_Standard.Label("FriendlyRaidPoints".Translate(Math.Round(friendpersent * 100f)));
        friendpersent = listing_Standard.Slider(friendpersent, 0.1f, 2f);
        listing_Standard.End();
    }
}
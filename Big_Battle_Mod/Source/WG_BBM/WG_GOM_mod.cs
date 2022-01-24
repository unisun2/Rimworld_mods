using UnityEngine;
using Verse;

namespace WG_BBM;

internal class WG_GOM_mod : Mod
{
    public static WG_BBM_setting Settings;

    public WG_GOM_mod(ModContentPack content)
        : base(content)
    {
        Settings = GetSettings<WG_BBM_setting>();
    }

    public override string SettingsCategory()
    {
        return "Big battle mod";
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        Settings.DoSettingsWindowContents(inRect);
    }
}
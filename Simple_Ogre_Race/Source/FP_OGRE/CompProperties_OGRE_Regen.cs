using Verse;

namespace FP_OGRE;

public class CompProperties_OGRE_Regen : CompProperties
{
    public int rateInTicks = 800;

    public CompProperties_OGRE_Regen()
    {
        compClass = typeof(CompOGRERegen);
    }
}
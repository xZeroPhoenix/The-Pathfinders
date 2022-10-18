using HarmonyLib;
using RimWorld;
using System;
using System.Linq;
using Verse;

// Ref: Nabber
// Alright, so no Apparel subclass, just a harmony patch on the WornGraphicPath and
// then check if the equippers race has an override for this apparels
// ThingDef and use those graphic paths instead

// Notes:
//where to hook it in, Pawn_ApparelTracker? maybe?

namespace ThePathfinders
{
    [StaticConstructorOnStartup]
    public class ThePathfindersPatchingApparelForRace
    {
        static ThePathfindersPatchingApparelForRace()
        {
            var harmony = new Harmony("ThePathfindersMod.ZeroPhoenix.patch2");
            harmony.PatchAll();
            Log.Message("Pathfinder Apparel Patching Initialisation");
        }

    }

    [HarmonyPatch(typeof(Apparel),"WornGraphicPath")]
    internal class WornGraphicPath
    {
        bool IsPawnPathfinderRace(Pawn pawn)
        {
            return pawn.def.defName == "Alien_Pathfinder";
        }

        bool ApparelHasModExtension(Pawn pawn, Apparel apparel)
        {
            return pawn.apparel.Contains(apparel) == apparel.def.HasModExtension<ApparelGraphicRaceExtension>();
        }

        public string Postfix(ref string __result, Pawn ___pawn, Apparel ___apparel)
        {
            Pawn pawn = ___pawn;
            Apparel apparel = ___apparel;

            if (IsPawnPathfinderRace(pawn) || ApparelHasModExtension(pawn, apparel))
            {
                return __result = ApparelGraphicRaceUtility.GetAltPathString(apparel);
            }
            return __result;
        }
    }

}
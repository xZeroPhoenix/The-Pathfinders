using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePathfinders.DecrepitCode
{
    internal class Decrepit
    {
        #region
        [HarmonyPatch(typeof(Apparel), "WornGraphicPath", MethodType.Getter)]
        class PathfinderWornGraphicPathPostfix
        {
            public static void Postfix(Apparel __instance, ref string __result)
            {
                string WornGraphicPath = __result;
                if (__instance.Wearer.def == PathfinderRaceDefOf.Alien_Pathfinder)
                {
                    if (__instance.def.HasModExtension<ThingDefExtension_CustomWornApparelGraphicForRace>())
                    {
                        __result = __instance.def.GetModExtension<ThingDefExtension_CustomWornApparelGraphicForRace>().AltWornGraphicPath;
                        Log.Message(__result);
                    }

                }

            }
        }
        #endregion
    }
}

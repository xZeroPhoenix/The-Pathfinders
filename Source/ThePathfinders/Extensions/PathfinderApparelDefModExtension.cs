using HarmonyLib;
using RimWorld;
using Verse;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System;

namespace ThePathfinders
{
    #region
    public static class ApparelGraphicRaceUtility
    {
        private static Dictionary<ThingDef, string> cachedAltPath = new Dictionary<ThingDef, string>();
        public static string GetAltPathString(ThingDef altPath)
        {
            if (!cachedAltPath.ContainsKey(altPath))
            {
                ThingDefExtension_CustomWornApparelGraphicForRace stringExtension = altPath.GetModExtension<ThingDefExtension_CustomWornApparelGraphicForRace>();
                string AltWornGraphicPath;
                if (stringExtension == null)
                    throw new ArgumentNullException();

                else
                    AltWornGraphicPath = stringExtension.AltWornGraphicPath;

                cachedAltPath.SetOrAdd(altPath, AltWornGraphicPath);
            }
            return cachedAltPath[altPath];
        }
    }
    #endregion
    #region
    // NightmareCorporation — Today at 7:56 PM I'm saying all you do in the extension is have a path, no logic no nothing,
    // meaning you throw this onto an apparels ThingDef and then it uses your extensions path instead of the normal one
    // That's a roundabout way of just changing the wornGraphicPath of the Apparel itself
    public class ThingDefExtension_CustomWornApparelGraphicForRace : DefModExtension
    {
        public string AltWornGraphicPath = "";
        private Dictionary<ThingDef, string> raceAltGraphicPath = new Dictionary<ThingDef, string>();
        public string GetWornGraphicPathForRace(ThingDef race)
        {
            return raceAltGraphicPath.TryGetValue(race, null);
        }
        public override IEnumerable<string> ConfigErrors() 
        {
            foreach (string error in base.ConfigErrors())
                yield return error;

            if (AltWornGraphicPath == null)
                yield return $"Required value {nameof(AltWornGraphicPath)} was not set!";
        }
    }
    #endregion
}

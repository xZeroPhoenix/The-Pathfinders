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
    public class ApparelGraphicRaceUtility
    {
        private const string FallbackAltWornGraphicPath = "FallBack";
        private static Dictionary<Apparel, string> cachedAltPath = new Dictionary<Apparel, string>();
        public static string GetAltPathString(Apparel apparel)
        {
            if (!cachedAltPath.ContainsKey(apparel))
            {
                ApparelGraphicRaceExtension stringExtension = apparel.def.GetModExtension<ApparelGraphicRaceExtension>();
                string AltWornGraphicPath;
                if (stringExtension != null)
                    AltWornGraphicPath = FallbackAltWornGraphicPath;
                else
                    AltWornGraphicPath = stringExtension.RaceAltWornGraphicPath;
                cachedAltPath.SetOrAdd(apparel, AltWornGraphicPath);
            }
            return cachedAltPath[apparel];
        }
    }
    #endregion
    #region
    public class ApparelGraphicRaceExtension : DefModExtension
    {
        public string RaceAltWornGraphicPath = "";
        public override IEnumerable<string> ConfigErrors()
        {
            foreach (string error in base.ConfigErrors())
                yield return error;

            if (RaceAltWornGraphicPath == "FallBack")
                yield return $"Required value {nameof(RaceAltWornGraphicPath)} was not set!";
        }
    }
    #endregion
}

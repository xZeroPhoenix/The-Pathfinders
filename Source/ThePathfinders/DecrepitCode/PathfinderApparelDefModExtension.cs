﻿using HarmonyLib;
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
        public static string GetAltPathStringForWornApparel(ThingDef WornApparelThingDef)
        {
            if (!cachedAltPath.ContainsKey(WornApparelThingDef))
            {
                ThingDefExtension_CustomWornApparelGraphicForRace stringExtension = WornApparelThingDef.GetModExtension<ThingDefExtension_CustomWornApparelGraphicForRace>();
                string AltWornGraphicPath;
                if (stringExtension == null)
                    throw new ArgumentNullException();

                else
                    AltWornGraphicPath = stringExtension.AltWornGraphicPath;

                cachedAltPath.SetOrAdd(WornApparelThingDef, AltWornGraphicPath);
            }
            return cachedAltPath[WornApparelThingDef];
        }
    }
    #endregion
    #region
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

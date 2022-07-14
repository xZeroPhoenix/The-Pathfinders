using HarmonyLib;
using RimWorld;
using Verse;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using System.Text;

namespace ThePathfinders
{
    public static class BodyPartTierUtility
    {
        private const int defaultTier = -1;  // used for BodyPartDef without a DefModExtension
        private static Dictionary<BodyPartDef, int> cachedPartTiers = new Dictionary<BodyPartDef, int>();
        public static int GetTier(BodyPartDef part)
        {
            // if not cached already, cache the tier
            if (!cachedPartTiers.ContainsKey(part))
            {
                BodyPartExtension_Tier tierExtension = part.GetModExtension<BodyPartExtension_Tier>();
                int tier;
                if (tierExtension == null)   // if no extension was found, we fall back to default value
                    tier = defaultTier;
                else    // but if we found an extension, retrieve the weight from it
                    tier = tierExtension.tier;

                // we retrieved a value, now we cache it so future access to this method can re-use that value
                cachedPartTiers.SetOrAdd(part, tier);
            }
            // retrieve cached value
            return cachedPartTiers[part];
        }
    }
    public class BodyPartExtension_Tier : DefModExtension
    {
        public int tier = -1;  // if no value provided, use -1, which will be validated later

        /// <summary>
        /// This throws an error at the user on launch if the value was not set in the extension, which makes troubleshooting issues easier and allows you to "restrict" which values are allowed
        /// </summary>
        public override IEnumerable<string> ConfigErrors()
        {
            // overrides to ConfigErrors() should always ask their base if they have their own config errors and yield them, otherwise they get lost
            foreach (string error in base.ConfigErrors())
                yield return error;

            if (tier == -1)
                yield return $"Required value {nameof(tier)} was not set!";
        }
    }
}

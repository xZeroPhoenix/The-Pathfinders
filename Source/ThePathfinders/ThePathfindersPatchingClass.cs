using HarmonyLib;
using RimWorld;
using System;
using Verse;

// this was made partially obsolete however the framework version doesn't seem to work as well to me



namespace ThePathfinders
{
    [StaticConstructorOnStartup]
    public class ThePathfindersPatchingClass
    {
        static ThePathfindersPatchingClass()
        {
            var harmony = new Harmony("ThePathfindersMod.ZeroPhoenix.patch");
            harmony.PatchAll();
            Log.Message("Pathfinder Patching Initialisation");
        }
    }
    // Credit to ???
    [HarmonyPatch(typeof(JobDriver_Lovin), "GenerateRandomMinTicksToNextLovin")]
    class PathfinderPatchLovinPrefix
    {
        static bool IsPathfinderRace(Pawn pawn)
        {
            return pawn.def.defName == "Alien_Pathfinder";
        }

        private static readonly SimpleCurve PathfinderCurve = new SimpleCurve
        {
        new CurvePoint(16f, 1.5f),
        new CurvePoint(22f, 1.5f),
        new CurvePoint(30f, 2f),
        new CurvePoint(50f, 2f),
        new CurvePoint(280f, 2.5f),
        new CurvePoint(385f, 4f)
        };

        public static void Postfix(ref int __result, Pawn pawn)
        {
            if (IsPathfinderRace(pawn))
            {
               // Log.Message("GenerateRandomMinTicksToNextLovin before patch: " + __result);
                __result = (int)(PathfinderCurve.Evaluate(pawn.ageTracker.AgeBiologicalYearsFloat) * 2500f);
               // Log.Message("GenerateRandomMinTicksToNextLovin after patch: " + __result);
            }
        }
    }
    // Credit to AUTOMATIC for this
    [HarmonyPatch(typeof(LovePartnerRelationUtility), "LovinMtbSinglePawnFactor")]
    class PathfinderPatchLovinPostfix
    {

        public static float LovinMtbSinglePawnFactor(Pawn pawn)
        {
            float num = 1f;
            num /= 1f - pawn.health.hediffSet.PainTotal;
            float level = pawn.health.capacities.GetLevel(PawnCapacityDefOf.Consciousness);
            if (level < 0.5f)
            {
                num /= level * 2f;
            }
            return num / GenMath.FlatHill(0f, 14f, 16f, 300f, 380f, 0.2f, pawn.ageTracker.AgeBiologicalYearsFloat);
        }
        public static bool Prefix(Pawn pawn, ref float __result)
        {
            if (pawn.def.defName == "Alien_Pathfinder")
            {
                __result = LovinMtbSinglePawnFactor(pawn);
               // Log.Message("Setting LovinMtbSinglePawnFactor to: " + __result);
                return false;
            }
            return true;
        }
    }
    // Credit to NoirFry for this
    [HarmonyPatch(typeof(Pawn_RelationsTracker), "SecondaryLovinChanceFactor")]
    internal static class Pawn_RelationsTracker_SecondaryLovinChanceFactor
    {
        static bool IsPathfinderRace(Pawn pawn)
        {
            return pawn.def.defName == "Alien_Pathfinder";
        }
        public static void Postfix(ref float __result, Pawn ___pawn, Pawn otherPawn)
        {
            
            Pawn pawn = ___pawn;
            if (IsPathfinderRace(pawn))
            {
                float ageBiologicalYearsFloat = pawn.ageTracker.AgeBiologicalYearsFloat;
                float ageBiologicalYearsFloat2 = otherPawn.ageTracker.AgeBiologicalYearsFloat;
                float originalNum = 1f;
                if (pawn.gender == Gender.Male)
                {
                    float min = ageBiologicalYearsFloat - 30f;
                    float lower = ageBiologicalYearsFloat - 10f;
                    float upper = ageBiologicalYearsFloat + 3f;
                    float max = ageBiologicalYearsFloat + 10f;
                    originalNum = GenMath.FlatHill(0.2f, min, lower, upper, max, 0.2f, ageBiologicalYearsFloat2);
                }
                else if (pawn.gender == Gender.Female)
                {
                    float min2 = ageBiologicalYearsFloat - 10f;
                    float lower2 = ageBiologicalYearsFloat - 3f;
                    float upper2 = ageBiologicalYearsFloat + 10f;
                    float max2 = ageBiologicalYearsFloat + 30f;
                    originalNum = GenMath.FlatHill(0.2f, min2, lower2, upper2, max2, 0.2f, ageBiologicalYearsFloat2);
                }
                float newNum = PathfinderRaceCalculation();
                float PathfinderRaceCalculation()
                {
                    float tempNum = 1f;
                    if (pawn.gender == Gender.Male)
                    {
                        float min = ageBiologicalYearsFloat - 142f;
                        float lower = ageBiologicalYearsFloat - 47f;
                        float upper = ageBiologicalYearsFloat + 14f;
                        float max = ageBiologicalYearsFloat + 47f;
                        tempNum = GenMath.FlatHill(0.2f, min, lower, upper, max, 0.2f, ageBiologicalYearsFloat2);
                    }
                    else if (pawn.gender == Gender.Female)
                    {
                        float min2 = ageBiologicalYearsFloat - 47f;
                        float lower2 = ageBiologicalYearsFloat - 14f;
                        float upper2 = ageBiologicalYearsFloat + 47f;
                        float max2 = ageBiologicalYearsFloat + 142f;
                        tempNum = GenMath.FlatHill(0.2f, min2, lower2, upper2, max2, 0.2f, ageBiologicalYearsFloat2);
                    }
                    return tempNum;
                }

                __result = __result / originalNum * newNum;
            }
        }
    }
}

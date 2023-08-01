using HarmonyLib;
using RimWorld;
using Verse;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System;

namespace ThePathfinders


/// ZERO-PHOENIX(2023): it occurred to me that this system may not play very nicely with mods that add Hediffs to body parts.
/// 
/// 
/// ZERO-PHOENIX: much of the code here is form ArchotechPlus so all credit gose to Biscuits and Mlie's (Out-Of-Date)
/// ZERO-PHOENIX: NABBER was of great help with this, after he got sick of me asking 20 billion questions :P
/// ZERO-PHOENIX: The neat file structure is his doing and if he commented on the code he rewrote it and helped explained what it does

{
    /// <summary>
    /// ZERO-PHOENIX: I dont really know why I need this, but I do 
    /// </summary>
    /// // NABBER: Yes, a HediffComp must have an accompanying HediffCompProperties, them's the rules. You put constant values into these, which are applied to all hediffComp instances
    /// // you could for example move your tick constant values into here, because they never change across all your regeneration handlers. stuff like that.
    public class HediffCompProperties_PathfinderRegeneration : HediffCompProperties
    {
        public HediffCompProperties_PathfinderRegeneration()
        {
            compClass = typeof(HediffComp_PathfinderRegeneration);
        }
    }


    /// <summary>
    /// ZERO-PHOENIX: This is the main part, mostly copyed form ArchotechPlus but with changes 
    /// </summary>
    /// ZERO-PHOENIX: This really isnt the case anymore but some of the code is still the same.

    public class HediffComp_PathfinderRegeneration : HediffComp
    {
        // NABBER: unused
        //private static readonly HediffDef GetBodyPartDefs = DefDatabase<HediffDef>.GetNamed("MissingBodyPart");
        // NABBER: you havea DefOf for this
        //private static readonly HediffDef RegenerationProgress = DefDatabase<HediffDef>.GetNamed("PathfinderRegenerationProgress");

        // NABBER: Stop prefacing everything with Pathfinder, it's unnecessary because you are already in your own ThePathfinders namespace
        // ZERO-PHOENIX: NEVER! 
        // ZERO-PHOENIX Later: Yeah, thats a good idea 
        private const int ticksToRestoreBodyPart = 3500;
        private const int ticksToHealRandomPermanentWound = 3500;

        private int remainingTicksToRestoreBodyPart = ticksToRestoreBodyPart;
        private int remainingTicksToHealRandomPermanentWound = ticksToHealRandomPermanentWound;

        // NABBER: the "manager" hediff does not need to know which part is currently being restored, it just needs to add a hediff to the part and move on.
        // the check for "already regenerating" will check if a part is already being restored, so keeping track of it is unnecessary overhead

        /// Ticks and stuff
        /// // NABBER: I get your idea here, but these are not necessary if you just define the value when you declare the variable. This is only ever called once, so you might as well set it directly at declaration
        //public override void CompPostMake()
        //{

        //    ResetTicksToRestore();
        //    ResetToHealRandomPermanentWound();
        //}

        // NABBER: any "raw number" is considered a "magic number" and thus a bad habit - make a const for them
        private void ResetTicksToRestoreBodyPart()
        {
            remainingTicksToRestoreBodyPart = ticksToRestoreBodyPart;
        }
        private void ResetTicksToHealRandomPermanentWound()
        {
            remainingTicksToHealRandomPermanentWound = ticksToHealRandomPermanentWound;
        }
        public override void CompPostTick(ref float severityAdjustment)
        {
            // doing these checks before reducing the ticks effectively "freezes" the regeneration manager, which seems desirable to me
            if(AlreadyRegenerating() || CurrentlyBleeding())
            {
                return;
            }
            remainingTicksToRestoreBodyPart--;
            if(remainingTicksToRestoreBodyPart <= 0)
            {
                ResetTicksToRestoreBodyPart();
                TryRestorePart();
            }
            remainingTicksToHealRandomPermanentWound--;
            if(remainingTicksToHealRandomPermanentWound <= 0)
            {
                // NABBER: something might break inside of the healing method, which stops execution of this method, so let's reset the ticks right now
                // so it doesn't re-enter this code every single tick and throw a new exception
                ResetTicksToHealRandomPermanentWound();
                TryHealRandomPermanentWound();
            }
        }

        /// <summary>
        /// ZERO-PHOENIX: This just check that a pawn is not already regenerated, as they are only supposed to regenerate one part at a time.
        /// </summary>
        /// // NABBER: you don't need to pass Pawn pawn into these methods, the HediffComp has access to the Hediff, which has access to the pawn it's on
        /// 

        private bool AlreadyRegenerating()
        {

            // NABBER: the previous check didn't ignore THIS hediff, meaning the hediff will detect itself and then assume something else is generating already, so we need to consider that
            bool isSomethingElseRegenerating = parent.pawn.health.hediffSet.hediffs    // take all hediffs
                .Except(parent)    // ignore THIS hediff (parent is the Hediff of any HediffComp)
                .Any(HediffDefIsAnyOf.PathfinderRegenerationProgressAny);

            // ZERO-PHOENIX: HediffDefIsAnyOf is one of my methods, this is likely a poor way to do this but it seems to work. 


            //check if any other hediff is regeneration
            // NABBER: instead of doing if(){} else{} for your logging, do this instead:
            // I disabled it for now, cause it spams like crazy :D
            // Log.Message("Pawn is already regenerating ? " + isSomethingElseRegenerating);
            return isSomethingElseRegenerating;
        }
        /// <summary>
        /// ZERO-PHOENIX: This makes sure the a pawn isnt able to start Regrowing a limb in combat, at least in the majority of circumstances.
        /// (A better solution would be to check for untreated injuries)
        /// </summary>
        private bool CurrentlyBleeding()
        {
            // NABBER: From personal experience checking the bleed rate is more robust than checking for the blood loss hediff
            bool isBleeding = parent.pawn.health.hediffSet.BleedRateTotal > 0;
            // Log.Message("Pawn is bleeding ? " + isBleeding);
            return isBleeding;
        }

        #region bodypart restoration logic
        private void TryRestorePart()
        {
            // logging out the pawns name can help "categorize" the logging messages if multiple hediffs are logging
            // Log.Message($"Attempting body part restoration for {parent.pawn.LabelShort}");
            try
            {
                BodyPartRecord partToRestore = FindBiggestMissingBodyPart();
                if(partToRestore == null)
                {
                    // Log.Message("No body part found to restore");
                    return;
                }
                int tier = BodyPartTierUtility.GetTier(partToRestore.def);
                // Log.Message($"Restoring part {partToRestore}, tier: {tier}");

                switch(tier)
                {
                    case 1:
                        RestoreBodyPart_TierOne(partToRestore);
                        break;
                    case 2:
                        RestoreBodyPart_TierTwo(partToRestore);
                        break;
                    case 3:
                        RestoreBodyPart_TierThree(partToRestore);
                        break;
                    default:
                        RestoreBodyPart_Fallback(partToRestore);
                        Log.Message("fallback used to restore");
                        break;
                }
            }
            // NABBER: this is a try-catch, which allows you to prevent the game from throwing nasty exceptions and do your own error handling.
            // To be concrete: this prevents the game from stopping execution of your method, which would have undesirable effects like not resetting the ticks (cause the ticks reset is called *after* this method)
            // you could put try-catch blocks everywhere you want, I mainly do it for anything that touches the health system because mods *love* to fuck with it
            catch(Exception e)
            {
                Log.Error($"Exception while trying to restore body part: {e}");
                return;
            }
            return;
        }

        /// <summary>
        /// ZERO-PHOENIX: This look for the biggest missing body part using C# magic I don't understand fully.
        /// </summary>
        private BodyPartRecord FindBiggestMissingBodyPart(float minCoverage = 0.0f)
        {
            // Log.Message("Initializing Pathfinder_FindBiggestMissingBodyPart");
            BodyPartRecord bodyPartRecord = null;
            foreach(var partsCommonAncestor in Pawn.health.hediffSet.GetMissingPartsCommonAncestors().Where(partsCommonAncestor =>
                   partsCommonAncestor.Part.coverageAbsWithChildren >= (double)minCoverage &&
                   !Pawn.health.hediffSet.PartOrAnyAncestorHasDirectlyAddedParts(partsCommonAncestor.Part) &&
                   (bodyPartRecord == null || partsCommonAncestor.Part.coverageAbsWithChildren >
                       (double)bodyPartRecord.coverageAbsWithChildren)))
            {
                bodyPartRecord = partsCommonAncestor.Part;
            }
            // Log.Message("" + bodyPartRecord?.def?.defName);
            return bodyPartRecord;
        }

        private void RestoreBodyPart_Fallback(BodyPartRecord part)
        {
            // Log.Message("using fallback");
            Pawn.health.RestorePart(part);
            Pawn.health.AddHediff(PathfinderHediffDefOf.PathfinderRegenerationProgressMinor, part);
            Pawn.health.AddHediff(PathfinderHediffDefOf.PathfinderRegenerationSicknessMinor);

        }
        private void RestoreBodyPart_TierOne(BodyPartRecord part)
        {
           // Log.Message("using tier 1");
            Pawn.health.RestorePart(part);
            Pawn.health.AddHediff(PathfinderHediffDefOf.PathfinderRegenerationProgressMinor, part);
            Pawn.health.AddHediff(PathfinderHediffDefOf.PathfinderRegenerationSicknessMinor);
        }
        private void RestoreBodyPart_TierTwo(BodyPartRecord part)
        {
           // Log.Message("using tier 2");
            Pawn.health.RestorePart(part);
            Pawn.health.AddHediff(PathfinderHediffDefOf.PathfinderRegenerationProgressModerate, part);
            Pawn.health.AddHediff(PathfinderHediffDefOf.PathfinderRegenerationSicknessModerate);
        }
        private void RestoreBodyPart_TierThree(BodyPartRecord part)
        {
            Log.Message("using tier 3");
            Pawn.health.RestorePart(part);
            Pawn.health.AddHediff(PathfinderHediffDefOf.PathfinderRegenerationProgressSevere, part);
            Pawn.health.AddHediff(PathfinderHediffDefOf.PathfinderRegenerationSicknessSevere);
        }

        #endregion

        #region permanent injury healing
        /// <summary>
        /// if _woundRegenerationTarget is true, it sets the wound Severity to 0, healing the wound 
        /// </summary>
        private void TryHealRandomPermanentWound()
        {
            //Log.Message($"Attempting permanent wound healing for {parent.pawn.LabelShort}");
            try
            {
                Hediff injury = FindRandomPermanentWound();

                if(injury == null)
                {
                    // Log.Message("No permanent injury found to heal");
                    return;
                }
                injury.Severity = 0.0f;
                //Log.Message($"Healed injury {injury}");
            }
            catch(Exception e)
            {
                Log.Error($"Exception whilst trying to heal permanent injury: {e}");
            }
        }
        /// <summary>
        /// ZERO-PHOENIX: This looks for a Random PermanentWound, that is not ResurrectionPsychosis (I think)
        /// </summary>
        private Hediff FindRandomPermanentWound()
        {
            // NABBER: Actually, this looked only for hediffs that are permanent or specifically the resurrection psychosis, changed == to !=
            // also made the code more readable, TryGetRandom is a worse approach than RandomElementWithFallback
            return Pawn.health.hediffSet.hediffs
                .Where(hd => hd.def != HediffDefOf.ResurrectionPsychosis
                    && hd.IsPermanent())
                .RandomElementWithFallback();
        }
        #endregion

        /// ZERO-PHOENIX: debug stuff
        // NABBER: ExposeData is definitely not debug, it handles saving the persistable data of your hediffComp, which right now is just the remaining ticks
        public override void CompExposeData()
        {
            Scribe_Values.Look(ref remainingTicksToRestoreBodyPart, "remainingTicksToRestoreBodyPart", ticksToRestoreBodyPart);
            Scribe_Values.Look(ref remainingTicksToHealRandomPermanentWound, "remainingTicksToHealRandomPermanentWound", ticksToHealRandomPermanentWound);
        }

        public override string CompDebugString()
        {
            // NABBER: behold the beauty that is @"" - which allows you to have line breaks in a string - combine that with $"" and you can directly use variables in string without those nasty +
            return $@"remainingTicksToRestoreBodyPart: {remainingTicksToRestoreBodyPart}
            remainingTicksToHealRandomPermanentWound: {remainingTicksToHealRandomPermanentWound}";
        }

    }
}

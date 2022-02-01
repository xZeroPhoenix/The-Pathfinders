using HarmonyLib;
using RimWorld;
using Verse;
using System.Linq;
using UnityEngine;


namespace ThePathfinders
{

    /// Notes: Just about all of the code here is form ArchotechPlus so all credit gose to Biscuits and Mlie's
    /// But Ive put several different files together so it's quite messy


    [DefOf]
    public static class PathifinderDefOf
    {
        public static HediffDef PathfinderBaseRegeneration;
        public static HediffDef PathfinderRegenerationProgress;
    }

    public class Hediff_LevelWithComps : HediffWithComps
    {
 
        public override void PostAdd(DamageInfo? dinfo)
        {

        }


    }
    public class HediffCompProperties_Regeneration : HediffCompProperties
        {
            public HediffCompProperties_Regeneration()
            { 
                compClass = typeof(HediffComp_Regeneration);

            }
        }


    public class HediffComp_Regeneration : HediffComp
        {
        private BodyPartRecord _bodyPartRegenerationTarget;
        private static readonly HediffDef RegenerationProgress = DefDatabase<HediffDef>.GetNamed("PathfinderRegenerationProgress");
        private Hediff _woundRegenerationTarget;
        private int _ticks;
        private int _ticksFullCharge;
        private const int HourTickInterval = 2500;
        private IntRange _healingCooldownRange = new IntRange(2, 55);


        private BodyPartRecord FindBiggestMissingBodyPart(float minCoverage = 0.0f)
            {
                BodyPartRecord bodyPartRecord = null;
                foreach (var partsCommonAncestor in Pawn.health.hediffSet.GetMissingPartsCommonAncestors().Where(
                    partsCommonAncestor =>
                        partsCommonAncestor.Part.coverageAbsWithChildren >= (double)minCoverage &&
                        !Pawn.health.hediffSet.PartOrAnyAncestorHasDirectlyAddedParts(partsCommonAncestor.Part) &&
                        (bodyPartRecord == null || partsCommonAncestor.Part.coverageAbsWithChildren >
                            (double)bodyPartRecord.coverageAbsWithChildren)))
                {
                    bodyPartRecord = partsCommonAncestor.Part;
                }

                return bodyPartRecord;
            }

            private Hediff FindRandomPermanentWound()
            {
                return !Pawn.health.hediffSet.hediffs.Where(hd =>
                        hd.def == HediffDefOf.ResurrectionPsychosis || hd.IsPermanent())
                    .TryRandomElement(out var result)
                    ? null
                    : result;
            }

            public override void CompPostTick(ref float severityAdjustment)
            {
                _ticks++;
                if (_ticks > _ticksFullCharge)
                {
                    PawnHasRegenerationHediff(Pawn);
                    ResetChargingTicks();
                }

                if (_ticks % HourTickInterval == 0)
                {
                    LongTick();
                }
            }
            private void LongTick()
            {
                if (IsPawnInjured() && PawnHasRegenerationHediff(Pawn))
                {
                    if (TryRestoreMissingPart() || TryHealRandomPermanentWound())
                    {
                        _ticks = 1000;
                        IsPawnInjured();
                        return;
                    }
                }

            }
            private void ResetChargingTicks()
            {
                _ticks = 0;
                _ticksFullCharge = _healingCooldownRange.RandomInRange * HourTickInterval;
            }
            private bool PawnHasRegenerationHediff(Pawn pawn)
            {
                if (pawn.health.hediffSet.HasHediff(PathifinderDefOf.PathfinderBaseRegeneration)) 
                {
                    return true;

                }
                return false;
            }

            private bool PathIsPawnBleeding(Pawn pawn)
            {
                if (pawn.health.hediffSet.HasHediff(HediffDefOf.BloodLoss))
                {
                    return true;
                }

                return false;
            }

            private bool IsPawnRegenerating(Pawn pawn)
            {
                if (pawn.health.hediffSet.HasHediff(PathifinderDefOf.PathfinderRegenerationProgress))
                {
                    return true;

                }
                return false;
             }
            private bool IsPawnInjured()
            {
                _bodyPartRegenerationTarget = FindBiggestMissingBodyPart();
                _woundRegenerationTarget = FindRandomPermanentWound();
                return _bodyPartRegenerationTarget != null || _woundRegenerationTarget != null;
            }

            private bool TryRestoreMissingPart()
            {
                if (_bodyPartRegenerationTarget == null)
                {
                    return false;
                }

                if (IsPawnRegenerating(Pawn) == true)
                {
                    return false;
                }

                if (PathIsPawnBleeding(Pawn) == true)
                {
                    return false;
                }

                Pawn.health.RestorePart(_bodyPartRegenerationTarget);
                Pawn.health.AddHediff(RegenerationProgress, _bodyPartRegenerationTarget);

              return true; 
            }

            private bool TryHealRandomPermanentWound()
            {
                if (_woundRegenerationTarget == null)
                {
                    return false;
                }

                _woundRegenerationTarget.Severity = 0.0f;
                return true;
            }

            public override void CompExposeData()
            {
                Scribe_Values.Look(ref _ticks, "ticksToHeal");

            }


        }
    
}

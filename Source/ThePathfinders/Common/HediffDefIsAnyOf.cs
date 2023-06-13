using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace ThePathfinders
{
    public class HediffDefIsAnyOf
    {
        public static HediffDef PathfinderBaseRegeneration;
        public static HediffDef PathfinderRegenerationProgressMinor;
        public static HediffDef PathfinderRegenerationProgressModerate;
        public static HediffDef PathfinderRegenerationProgressSevere;

        public static bool PathfinderRegenerationProgressAny(Hediff hediff)
        {
            var def = hediff.def;

            return def == PathfinderHediffDefOf.PathfinderBaseRegeneration
                || def == PathfinderHediffDefOf.PathfinderRegenerationProgressMinor
                || def == PathfinderHediffDefOf.PathfinderRegenerationProgressModerate
                || def == PathfinderHediffDefOf.PathfinderRegenerationProgressSevere;
        }

    }
}

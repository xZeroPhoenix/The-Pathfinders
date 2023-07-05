﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace ThePathfinders
{
    /// <summary>
    /// This loads the XML defs the this method needs later in the code (I think)
    /// </summary>
    /// // NABBER: Good call on the DefOf, it works as you think. But you had a typo and you should specify the Def type of the DefOf in the name so you can create multiple
    /// // also these are usually best placed in their own class and re-used all over your code where you need em

    [DefOf]
    public class PathfinderRaceDefOf
    {
        public static ThingDef Alien_Pathfinder;

        static PathfinderRaceDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(PathfinderRaceDefOf));
        }
    }
    [DefOf]
    public static class PathfinderHediffDefOf
    {
        public static HediffDef PathfinderBaseRegeneration;

        public static HediffDef PathfinderRegenerationProgressMinor;
        public static HediffDef PathfinderRegenerationProgressModerate;
        public static HediffDef PathfinderRegenerationProgressSevere;

        public static HediffDef PathfinderRegenerationSicknessMinor;
        public static HediffDef PathfinderRegenerationSicknessModerate;
        public static HediffDef PathfinderRegenerationSicknessSevere;

        static PathfinderHediffDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(PathfinderHediffDefOf));
        }
    }

}

using HarmonyLib;
using RimWorld;
using Verse;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace ThePathfinders
{
    public class HediffCompProperties_PathfinderSetSeveritySinkness : HediffCompProperties
    {
        public HediffCompProperties_PathfinderSetSeveritySinkness()
        {
            compClass = typeof(HediffComp_PathfinderSetSeveritySinkness);
        }
    }
    public class HediffComp_PathfinderSetSeveritySinkness : HediffComp
    {

    }
}

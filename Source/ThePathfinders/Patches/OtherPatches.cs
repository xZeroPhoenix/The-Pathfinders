using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace ThePathfinders.Patches
{
    // Thanks to Razar
    public class PawnRenderSubWorker_NoDraw : PawnRenderSubWorker
    {
        public override bool CanDrawNowSub(PawnRenderNode node, PawnDrawParms parms)
        {
            if (parms.pawn.def.defName == PathfinderRaceDefOf.Alien_Pathfinder.defName)
            {
                return false;
            }
            return true;
        }

        public PawnRenderSubWorker_NoDraw()
        { }
    }
}

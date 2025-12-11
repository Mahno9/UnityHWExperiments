using System;

using UnityEngine;

namespace InteractionExample.PointerInteractions.Logic
{
    public static class DraggableHits
    {
        public static RaycastHit[] GetHitsByRaySorted(Ray ray)
        {
            RaycastHit[] hits = Physics.RaycastAll(ray, float.PositiveInfinity);

            SortHits(hits);

            return hits;
        }

        private static void SortHits(RaycastHit[] hits)
        {
            Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));
        }
    }
}
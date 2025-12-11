using UnityEngine;

namespace InteractionExample.PointerInteractions.Interfaces
{
    public interface IDraggable
    {
        void StoreHold(Ray intersectRay);
        void MoveTo(Ray    movedRay);
    }
}
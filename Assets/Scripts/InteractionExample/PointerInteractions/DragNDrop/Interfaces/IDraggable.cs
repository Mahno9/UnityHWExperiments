using UnityEngine;

namespace InteractionExample.PointerInteractions.DragNDrop.Interfaces
{
    public interface IDraggable
    {
        void StoreHold(Ray intersectRay);
        void MoveTo(Ray    movedRay);
    }
}
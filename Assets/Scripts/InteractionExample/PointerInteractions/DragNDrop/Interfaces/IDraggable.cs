using UnityEngine;

namespace InteractionExample.PointerInteractions.DragNDrop.Interfaces
{
    public interface IDraggable
    {
        void OnGrab(Ray intersectRay);
        void MoveTo(Ray movedRay);
        void OnRelease();
    }
}
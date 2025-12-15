using InteractionExample.PointerInteractions.DragNDrop.Interfaces;

using UnityEngine;

namespace InteractionExample.PointerInteractions.DragNDrop
{
    public class DragNDropService
    {
        private IDraggable _heldItem;

        public void GrabItemOnRay(Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hit)
                && hit.transform.TryGetComponent(out IDraggable draggableItem))
            {
                _heldItem = draggableItem;
                _heldItem.OnGrab(ray);
            }
        }

        public void ReleaseItem()
        {
            _heldItem?.OnRelease();
            _heldItem = null;
        }

        public void SetItemPositionByRay(Ray ray)
        {
            _heldItem?.MoveTo(ray);
        }
    }
}
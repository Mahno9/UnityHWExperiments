using InteractionExample.PointerInteractions.DragNDrop.Interfaces;

using UnityEngine;

namespace InteractionExample.PointerInteractions.DragNDrop
{
    public class DragNDropLogic
    {
        private IDraggable _heldItem;

        public void HoldItemOnRay(Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hit)
                && hit.transform.TryGetComponent(out IDraggable draggableItem))
            {
                _heldItem = draggableItem;
                _heldItem.StoreHold(ray);
            }
        }

        public void ReleasePointedItem()
        {
            _heldItem = null;
        }

        public void SetItemPositionByRay(Ray ray)
        {
            _heldItem?.MoveTo(ray);
        }
    }
}
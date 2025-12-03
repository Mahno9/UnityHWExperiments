using UnityEngine;

namespace InteractionExample
{
    public class DragNDropLogic
    {
        private const float DragVelocity = 10;
        private readonly Vector3 PickUpShift = Vector3.up * 0.1f;

        private Transform _heldItem;
        private Vector3   _heldShift;

        public void HoldItemOnRay(Ray ray)
        {
            RaycastHit[] hits = HitsCommon.GetHitsByRaySorted(ray);

            if (hits.Length < 2)
                return;

            Transform pickedItem      = hits[0].transform;
            Vector3   groundIntersect = hits[1].point; // Not only ground plane, but every thing behind first item

            _heldItem = pickedItem;
            _heldShift = pickedItem.position - groundIntersect;
        }

        public void ReleasePointedItem()
        {
            if (_heldItem is null)
                return;

            _heldItem = null;
        }

        public void SetItemPositionByRay(Ray ray)
        {
            if (_heldItem is null)
                return;

            RaycastHit[] hits = HitsCommon.GetHitsByRaySorted(ray);

            foreach (RaycastHit hit in hits)
            {
                if (hit.transform == _heldItem)
                    continue;

                Vector3 newItemPosition = hit.point + _heldShift + PickUpShift;
                ApplyPosition(newItemPosition);

                break;
            }
        }

        private void ApplyPosition(Vector3 newItemPosition)
        {
            if (_heldItem.TryGetComponent(out Rigidbody rb))
                rb.velocity =((newItemPosition - _heldItem.position) * DragVelocity);
            else
                _heldItem.position = newItemPosition;
        }
    }
}
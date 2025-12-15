using InteractionExample.PointerInteractions.DragNDrop.Interfaces;

using UnityEngine;

namespace InteractionExample.PointerInteractions.DragNDrop
{
    [RequireComponent(typeof(Rigidbody))]
    public class DraggableItem : MonoBehaviour, IDraggable
    {
        [SerializeField] private Vector3 _pickUpShift  = Vector3.up * 0.1f;
        [SerializeField] private float   _dragVelocity = 10;

        private Vector3 _heldShift;

        public void StoreHold(Ray intersectRay)
        {
            RaycastHit[] hits = DraggableHits.GetHitsByRaySorted(intersectRay);

            foreach (RaycastHit hit in hits)
            {
                if (hit.transform == transform)
                    continue;

                _heldShift = ObjectPos - hit.point;

                break;
            }
        }

        public void MoveTo(Ray movedRay)
        {
            RaycastHit[] hits = DraggableHits.GetHitsByRaySorted(movedRay);

            foreach (RaycastHit hit in hits)
            {
                if (hit.transform == transform)
                    continue;

                Vector3 newItemPosition = hit.point + _heldShift + _pickUpShift;
                ApplyPosition(newItemPosition);

                break;
            }
        }

        private void ApplyPosition(Vector3 newItemPosition)
        {
            if (TryGetComponent(out Rigidbody rb))
                rb.velocity = ((newItemPosition - ObjectPos) * _dragVelocity);
        }

        private Vector3 ObjectPos => TryGetComponent(out Rigidbody rb) ? rb.centerOfMass + transform.position : transform.position;
    }
}
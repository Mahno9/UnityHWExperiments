using System;

using InteractionExample.PointerInteractions.DragNDrop.Interfaces;

using UnityEngine;

namespace InteractionExample.PointerInteractions.DragNDrop
{
    [RequireComponent(typeof(Rigidbody))]
    public class DraggableWithRigidbody : MonoBehaviour, IDraggable
    {
        [SerializeField] private Vector3 _pickUpShift          = Vector3.up * 0.1f;
        [SerializeField] private float   _dragVelocity         = 10;
        [SerializeField] private string  _transparentLayerName = "GrabTransparent";

        private int _baseLayerIdx;

        private Vector3 _heldShift;
        private int     _transparentLayerIdx;

        private int TransparentLayerMask => 1 << _transparentLayerIdx;

        private Vector3 ObjectPos => TryGetComponent(out Rigidbody rb) ? rb.centerOfMass + transform.position : transform.position;

        private void Awake()
        {
            _baseLayerIdx = gameObject.layer;
            _transparentLayerIdx = LayerMask.NameToLayer(_transparentLayerName);
        }

        public void OnGrab(Ray intersectRay)
        {
            SwitchToTransparentLayer();
            if (Physics.Raycast(intersectRay, out RaycastHit hit, float.PositiveInfinity, ~TransparentLayerMask) == false)
                return;

            _heldShift = ObjectPos - hit.point;
        }


        public void MoveTo(Ray movedRay)
        {
            if (Physics.Raycast(movedRay, out RaycastHit hit, float.PositiveInfinity, ~TransparentLayerMask) == false)
            {
                Debug.LogWarning($"No item with mask: {Convert.ToString(~TransparentLayerMask, 2)}b");
                return;
            }

            Vector3 newItemPosition = hit.point + _heldShift + _pickUpShift;
            ApplyPosition(newItemPosition);
        }

        public void OnRelease()
        {
            SwitchToBaseLayer();
        }

        private void ApplyPosition(Vector3 newItemPosition)
        {
            if (TryGetComponent(out Rigidbody rb))
                rb.velocity = (newItemPosition - ObjectPos) * _dragVelocity;
        }

        private void SwitchToTransparentLayer()
        {
            gameObject.layer = _transparentLayerIdx;
        }

        private void SwitchToBaseLayer()
        {
            gameObject.layer = _baseLayerIdx;
        }
    }
}
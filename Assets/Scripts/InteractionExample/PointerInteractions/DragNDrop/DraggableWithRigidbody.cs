using System;

using InteractionExample.PointerInteractions.DragNDrop.Interfaces;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace InteractionExample.PointerInteractions.DragNDrop
{
    [RequireComponent(typeof(Rigidbody))]
    public class DraggableWithRigidbody : MonoBehaviour, IDraggable
    {
        [SerializeField] private Vector3   _pickUpShift  = Vector3.up * 0.1f;
        [SerializeField] private float     _dragVelocity = 10;
        [SerializeField] private LayerMask _transparentLayerMask;

        private Vector3   _heldShift;
        private LayerMask _baseLayerMask;

        private void Awake()
        {
            _baseLayerMask = gameObject.layer;
        }

        public void OnGrab(Ray intersectRay)
        {
            SwitchToGrabLayer();

            Debug.Log($"Call with mask: not {Convert.ToString(_transparentLayerMask, 2)}b => {Convert.ToString(~_transparentLayerMask, 2)}b");
            Debug.Log($"This item layer: {gameObject.layer}d = {Convert.ToString(gameObject.layer, 2)}b");

            if (Physics.Raycast(intersectRay, out RaycastHit hit, float.PositiveInfinity, ~_transparentLayerMask) == false)
                return;

            Debug.Log($"This item: {transform.name}, Raycast item: {hit.transform.name}; {(transform == hit.transform ? "FAIL" : "SUCCESS")}");
            _heldShift = ObjectPos - hit.point;
        }

        public void MoveTo(Ray movedRay)
        {
            if (Physics.Raycast(movedRay, out RaycastHit hit, float.PositiveInfinity, ~_transparentLayerMask) == false)
            {
                Assert.IsTrue(false, "No item behind this. WTF?");
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
                rb.velocity = ((newItemPosition - ObjectPos) * _dragVelocity);
        }

        private Vector3 ObjectPos => TryGetComponent(out Rigidbody rb) ? rb.centerOfMass + transform.position : transform.position;

        private void SwitchToGrabLayer() => gameObject.layer = _transparentLayerMask;
        private void SwitchToBaseLayer() => gameObject.layer = _baseLayerMask.value;
    }
}
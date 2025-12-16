using System;

using HeadlessSkeleton;

using InteractionExample.PointerInteractions.DragNDrop.Interfaces;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace InteractionExample.PointerInteractions.DragNDrop
{
    [RequireComponent(typeof(Rigidbody))]
    public class DraggableWithRigidbody : MonoBehaviour, IDraggable
    {
        [SerializeField] private Vector3 _pickUpShift          = Vector3.up * 0.1f;
        [SerializeField] private float   _dragVelocity         = 10;
        [SerializeField] private string  _transparentLayerName = "GrabTransparent";

        private Vector3 _heldShift;

        private int _baseLayer;
        private int _transparentLayer;

        private void Awake()
        {
            _baseLayer = gameObject.layer;
            _transparentLayer = 1 << LayerMask.NameToLayer(_transparentLayerName);
        }

        public void OnGrab(Ray intersectRay)
        {
            SwitchToTransparentLayer();
            if (Physics.Raycast(intersectRay, out RaycastHit hit, float.PositiveInfinity, ~_transparentLayer) == false)
                return;

            Debug.Log($"This item: {transform.name}, behind item: {hit.transform.name}; {(transform == hit.transform ? "FAIL" : "SUCCESS")}");
            if (transform == hit.transform)
            {
                Debug.LogWarning($"Tried: {Convert.ToString(~_transparentLayer, 2)}b; " +
                                 $"Want: {Convert.ToString(1 << LayerMask.NameToLayer("Default"), 2)}b; " +
                                 $"Transparent: {Convert.ToString(_transparentLayer, 2)}b; " +
                                 $"Hit obj: {Convert.ToString(hit.transform.gameObject.layer, 2)}b; "
                );
            }

            _heldShift = ObjectPos - hit.point;
        }

        public void MoveTo(Ray movedRay)
        {
            if (Physics.Raycast(movedRay, out RaycastHit hit, float.PositiveInfinity, ~_transparentLayer) == false)
            {
                Debug.LogWarning($"No item with mask: {Convert.ToString(~_transparentLayer, 2)}b");
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
            {
                rb.velocity = ((newItemPosition - ObjectPos) * _dragVelocity);
                RuntimeDebugLine.DrawLine(ObjectPos, newItemPosition, Color.red, 1);
            }
        }

        private Vector3 ObjectPos => TryGetComponent(out Rigidbody rb) ? rb.centerOfMass + transform.position : transform.position;

        private void SwitchToTransparentLayer() => gameObject.layer = _transparentLayer;
        private void SwitchToBaseLayer() => gameObject.layer = _baseLayer;
    }
}
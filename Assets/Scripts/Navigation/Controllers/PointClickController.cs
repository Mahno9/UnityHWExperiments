using HeadlessSkeleton;

using Navigation.Interfaces;
using Navigation.Manipulators;

using UnityEngine;

namespace Navigation.Controllers
{
    public class PointClickController : ControllerBase
    {
        private const int RightMouseButton = 1;

        private readonly IMovable  _movable;
        private readonly Camera    _camera;
        private readonly LayerMask _groundLayerMask;

        public float MoveSpeed => _movable.MoveSpeed;

        public PointClickController(IMovable movable, Camera camera, LayerMask groundLayerMask)
        {
            _movable = movable;
            _camera = camera;
            _groundLayerMask = groundLayerMask;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            if (TryGetMoveTarget(out Vector3 newTargetPosition))
            {
                _movable.SetTargetPosition(newTargetPosition);
                Debug.Log($"New targetPos: {newTargetPosition}");
            }

            _movable.Update(deltaTime);
        }

        private bool TryGetMoveTarget(out Vector3 targetPosition)
        {
            Ray pointerRay = _camera.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButtonDown(RightMouseButton) && TryGetGroundPosition(pointerRay, out Vector3 groundPos))
            {
                targetPosition = groundPos;
                return true;
            }

            targetPosition = Vector3.zero;
            return false;
        }

        private bool TryGetGroundPosition(Ray ray, out Vector3 groundPos)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, _groundLayerMask))
            {
                groundPos = hit.point;
                return true;
            }

            groundPos = Vector3.zero;
            return false;
        }
    }
}
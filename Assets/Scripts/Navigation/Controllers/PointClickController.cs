using System.Collections.Generic;

using Navigation.Characters.Interfaces;
using Navigation.CoreMechanics.Movement.Interfaces;

using UnityEngine;
using UnityEngine.AI;

namespace Navigation.Controllers
{
    public class PointClickController : ControllerBase, IMovePointBroadcaster
    {
        private const    int       RightMouseButton = 1;
        private readonly Camera    _camera;
        private readonly LayerMask _groundLayerMask;

        private readonly IMovable                   _movable;
        private readonly List<IMovePointSubscriber> _subscribers = new();

        public PointClickController(IMovable movable, Camera camera, LayerMask groundLayerMask)
        {
            _movable = movable;
            _camera = camera;
            _groundLayerMask = groundLayerMask;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            if (_movable.IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData))
            {
                if (_movable.IsInJumpProcess == false)
                    _movable.Jump(offMeshLinkData);

                return;
            }

            if (TryGetMoveTarget(out Vector3 newMovePoint))
            {
                _movable.SetMovePoint(newMovePoint);
                NotifyOnNewMovePoint(newMovePoint);
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

        public void SubscribeOnMovePoints(IMovePointSubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }

        private void NotifyOnNewMovePoint(Vector3 movePoint)
        {
            foreach (IMovePointSubscriber subscriber in _subscribers)
                subscriber.OnNewMovePoint(movePoint);
        }
    }
}
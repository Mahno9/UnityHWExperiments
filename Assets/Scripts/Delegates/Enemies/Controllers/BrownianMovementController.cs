using Delegates.Timer;

using Navigation.Characters.Interfaces;
using Navigation.Controllers;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace Delegates.Enemies.Controllers
{
    public class BrownianMovementController : ControllerBase
    {
        private const int PickPointMaxTries = 100;

        private readonly IMovable _movable;
        private readonly float    _newPointRadius;
        private readonly float    _idleTime;

        private readonly TimerService _idleTimer = new TimerService();

        public BrownianMovementController(IMovable movable, float newPointRadius, float idleTime)
        {
            _movable = movable;
            _newPointRadius = newPointRadius;
            _idleTime = idleTime;

            _idleTimer.OnTimerStopped += FinishIdle;
        }

        ~BrownianMovementController() => _idleTimer.OnTimerStopped -= FinishIdle;

        protected override void UpdateLogic(float deltaTime)
        {
            _movable.Update(deltaTime);
            _idleTimer.Update(deltaTime);

            if (Mathf.Approximately(_movable.MoveSpeed, 0))
                StartIdle();
        }

        private void StartIdle() => _idleTimer.StartTimer(_idleTime);

        private void FinishIdle()
        {
            Vector3? newPoint = TryPickNewPoint();
            Assert.IsTrue(newPoint != null);

            _movable.SetMovePoint(newPoint.Value);
        }

        private Vector3? TryPickNewPoint()
        {
            for (int i = 0; i < PickPointMaxTries; i++)
            {
                Vector3 randomPos = Random.insideUnitSphere * _newPointRadius;
                randomPos.y = 0; // Pick withing a circle on the ground
                randomPos += _movable.Position;

                bool isPositionFound = NavMesh.SamplePosition(randomPos, out NavMeshHit hit, _newPointRadius/2, NavMesh.AllAreas);
                if (isPositionFound)
                    return hit.position;
            }

            return null;
        }
    }
}
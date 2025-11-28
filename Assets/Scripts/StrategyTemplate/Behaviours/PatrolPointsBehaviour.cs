using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;

namespace StrategyTemplate.Behaviours
{
    public class PatrolPointsBehaviour : MovableBehaviourBase
    {
        private const float EPSILON = 0.1f;
        private const int MIN_POINTS_COUNT = 2;

        private readonly List<Transform> _patrolPoints;

        private int _currentPointIdx;

        public PatrolPointsBehaviour(Transform owner, List<Transform> patrolPoints) : base(owner)
        {
            Assert.IsTrue(patrolPoints is { Count: >= MIN_POINTS_COUNT }, "PatrolPointsBehaviour requires at least 2 patrol points");

            _patrolPoints = patrolPoints;
            _currentPointIdx = Random.Range(0, _patrolPoints.Count);
        }

        public override void Update(float deltaTime)
        {
            if (HasReachedCurrentPoint(Owner))
                PickNextPoint();

            MoveTowardsCurrentPoint(deltaTime, Owner);
        }

        private bool HasReachedCurrentPoint(Transform owner)
        {
            var targetPosition = _patrolPoints[_currentPointIdx].position;

            return Vector3.Distance(owner.position, targetPosition) < EPSILON;
        }

        private void MoveTowardsCurrentPoint(float deltaTime, Transform owner)
        {
            var targetPosition = _patrolPoints[_currentPointIdx].position;
            var directionNorm = (targetPosition - owner.position).normalized;

            MoveTo(directionNorm);
        }

        private void PickNextPoint()
        {
            int nextPointIdx;
            do
            {
                nextPointIdx = Random.Range(0, _patrolPoints.Count);
            } while (nextPointIdx == _currentPointIdx && _patrolPoints.Count >= MIN_POINTS_COUNT);

            _currentPointIdx = nextPointIdx;
        }
    }
}
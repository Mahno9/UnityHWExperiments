using Navigation.Interfaces;

using UnityEngine;
using UnityEngine.AI;

namespace Navigation.Manipulators
{
    public class NavMeshAgentMover : IMovable
    {
        private readonly NavMeshAgent _agent;

        private Vector3 _currentTargetPos;

        public float   MoveSpeed     => _agent.velocity.magnitude;

        public Vector3 MoveDirection => _agent.velocity.normalized;

        public Vector3 Position      => _agent.transform.position;

        public Vector3 CurrentTarget => _currentTargetPos;


        public NavMeshAgentMover(NavMeshAgent agent)
        {
            _agent = agent;
            _currentTargetPos = Position;
        }

        public void Update(float deltaTime)
        {
            _agent.SetDestination(_currentTargetPos);
        }

        public void SetMovePoint(Vector3 point)
        {
            _currentTargetPos = point;
        }
    }
}
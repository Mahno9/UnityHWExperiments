using Navigation.Interfaces;

using UnityEngine;
using UnityEngine.AI;

namespace Navigation.Manipulators
{
    public class NavMeshAgentMover : IMovable
    {
        private readonly NavMeshAgent _agent;

        public float MoveSpeed => _agent.velocity.magnitude;

        public Vector3 MoveDirection => _agent.desiredVelocity.normalized;

        public Vector3 Position => _agent.transform.position;

        public Vector3 CurrentTarget { get; private set; }


        public NavMeshAgentMover(NavMeshAgent agent)
        {
            _agent = agent;
            CurrentTarget = Position;
        }

        public void Update(float deltaTime)
        {
            _agent.SetDestination(CurrentTarget);
        }

        public void SetMovePoint(Vector3 point)
        {
            CurrentTarget = point;
        }
    }
}
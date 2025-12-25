using Navigation.Common.Interfaces;
using Navigation.Movement.Interfaces;

using UnityEngine;
using UnityEngine.AI;

namespace Navigation.Movement.Manipulators
{
    public class NavMeshAgentMover : IUpdatable, IMover
    {
        private readonly NavMeshAgent _agent;

        public NavMeshAgentMover(NavMeshAgent agent)
        {
            _agent = agent;
            CurrentTarget = Position;
        }

        public Vector3 CurrentTarget { get; private set; }

        public float MoveSpeed => _agent.velocity.magnitude;

        public Vector3 MoveDirection => _agent.desiredVelocity.normalized;

        public Vector3 Position => _agent.transform.position;

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
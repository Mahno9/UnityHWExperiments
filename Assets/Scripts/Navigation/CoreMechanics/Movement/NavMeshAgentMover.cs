using Navigation.Controllers;
using Navigation.CoreMechanics.Movement.Interfaces;

using UnityEngine;
using UnityEngine.AI;

namespace Navigation.CoreMechanics.Movement
{
    public class NavMeshAgentMover : IUpdatable, IMover
    {
        private readonly NavMeshAgent _agent;
        private readonly AgentJumper  _agentJumper;

        public NavMeshAgentMover(NavMeshAgent agent, AgentJumper agentJumper)
        {
            _agent = agent;
            _agentJumper = agentJumper;

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


        public bool IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData)
        {
            if (_agent.isOnOffMeshLink)
            {
                offMeshLinkData = _agent.currentOffMeshLinkData;
                return true;
            }

            offMeshLinkData = default;
            return false;
        }

        public void Jump(OffMeshLinkData offMeshLinkData) => _agentJumper.Jump(offMeshLinkData);

        public bool IsInJumpProcess => _agentJumper.InProcess;
    }
}
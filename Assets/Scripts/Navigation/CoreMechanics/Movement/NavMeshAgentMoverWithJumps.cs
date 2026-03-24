using UnityEngine.AI;

namespace Navigation.CoreMechanics.Movement
{
    public class NavMeshAgentMoverWithJumps : NavMeshAgentMover
    {
        private readonly AgentJumper  _agentJumper;

        public NavMeshAgentMoverWithJumps(NavMeshAgent agent, AgentJumper agentJumper) : base(agent)
        {
            _agentJumper = agentJumper;
        }

        public void Jump(OffMeshLinkData offMeshLinkData) => _agentJumper.Jump(offMeshLinkData);

        public bool IsInJumpProcess => _agentJumper.InProcess;
    }
}
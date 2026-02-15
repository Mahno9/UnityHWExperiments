using System.Collections;

using UnityEngine;
using UnityEngine.AI;

namespace Navigation.CoreMechanics.Movement
{
    public class AgentJumper
    {
        private readonly float        _speed;
        private readonly NavMeshAgent _agent;

        private readonly MonoBehaviour _coroutineRunner;
        private          Coroutine     _jumpProcess;

        public AgentJumper(float speed, NavMeshAgent agent, MonoBehaviour coroutineRunner)
        {
            _speed = speed;
            _agent = agent;
            _coroutineRunner = coroutineRunner;
        }

        public bool InProcess => _jumpProcess != null;

        public void Jump(OffMeshLinkData offMeshLinkData)
        {
            if (InProcess)
                return;

            _jumpProcess = _coroutineRunner.StartCoroutine(JumpProcess(offMeshLinkData));
        }

        private IEnumerator JumpProcess(OffMeshLinkData offMeshLinkData)
        {
            Vector3 startPosition = offMeshLinkData.startPos;
            Vector3 endPosition   = offMeshLinkData.endPos;

            float duration = Vector3.Distance(startPosition, endPosition) / _speed;
            float progress = 0;

            while (progress < duration)
            {
                _agent.transform.position = Vector3.Lerp(startPosition, endPosition, progress / duration);
                progress += Time.deltaTime;
                yield return null;
            }

            _jumpProcess = null;
            _agent.CompleteOffMeshLink();
        }
    }
}
using System.Collections;

using UnityEngine;
using UnityEngine.AI;

namespace Navigation.CoreMechanics.Movement
{
    public class AgentJumper
    {
        private readonly float        _speed;
        private readonly NavMeshAgent _agent;

        private readonly MonoBehaviour  _coroutineRunner;
        private          Coroutine      _jumpProcess;
        private readonly AnimationCurve _yOffsetCurve;

        public AgentJumper(float speed, NavMeshAgent agent, AnimationCurve yOffsetCurve, MonoBehaviour coroutineRunner)
        {
            _speed = speed;
            _agent = agent;
            _yOffsetCurve = yOffsetCurve;
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

            float targetProgress = Vector3.Distance(startPosition, endPosition) / _speed;
            float progress = 0;

            while (progress < targetProgress)
            {
                float yOffset = _yOffsetCurve.Evaluate(progress / targetProgress);
                _agent.transform.position = Vector3.Lerp(startPosition, endPosition, progress / targetProgress) + Vector3.up * yOffset;
                progress += Time.deltaTime;
                yield return null;
            }

            _jumpProcess = null;
            _agent.CompleteOffMeshLink();
        }
    }
}
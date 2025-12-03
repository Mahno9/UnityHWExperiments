using System;

using Cinemachine;

using UnityEngine;

namespace InteractionExample
{
    [RequireComponent(typeof(CinemachineTargetGroup))]
    public class CinemachineTargetAutoGroup : MonoBehaviour
    {
        [SerializeField] private Transform _container;

        private void Awake()
        {
            _container ??= transform;

            CinemachineTargetGroup targetGroup = GetComponent<CinemachineTargetGroup>();

            for (int idx = 0; idx < _container.childCount; idx++)
                targetGroup.AddMember(_container.GetChild(idx), 1, 0);
        }
    }
}
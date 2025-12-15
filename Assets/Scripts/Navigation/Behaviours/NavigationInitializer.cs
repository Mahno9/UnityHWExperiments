using System;

using Navigation.Controllers;
using Navigation.Interfaces;
using Navigation.Manipulators;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace Navigation.Behaviours
{
    public class NavigationInitializer : MonoBehaviour
    {
        private const string GroundLayerName = "Ground";

        private ControllerBase _controller;

        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private float        _rotationSpeed;

        private void Awake()
        {
            Assert.IsNotNull(_navMeshAgent);

            IMovable mover = new NavMeshAgentMover(_navMeshAgent);

            _controller = new PointClickController(
                new CompositeManipulator(
                    mover,
                    new AlongMoverDirectionRotator(mover, new DirectionRotator(_rotationSpeed))
                ),
                Camera.main,
                LayerMask.GetMask(GroundLayerName)
            );

            _controller.Enable();
        }

        private void Update()
        {
            _controller.Update(Time.deltaTime);
        }
    }
}
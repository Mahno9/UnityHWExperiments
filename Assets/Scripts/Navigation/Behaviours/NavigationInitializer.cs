using System;

using Navigation.Controllers;
using Navigation.Interfaces;
using Navigation.Manipulators;
using Navigation.Utils;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

using Object = UnityEngine.Object;

namespace Navigation.Behaviours
{
    [RequireComponent(typeof(CharacterView))]
    [RequireComponent(typeof(NavigationEffectSpawner))]
    public class NavigationInitializer : MonoBehaviour
    {
        private const string GroundLayerName = "Ground";

        private MoveController          _controller;
        private CharacterView           _view;
        private NavigationEffectSpawner _effectSpawner;

        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private float        _rotationSpeed;

        private void Awake()
        {
            Assert.IsNotNull(_navMeshAgent);

            InitializeEffectSpawner();
            InitializeController(_effectSpawner);
            InitializeView();
        }

        private void InitializeController(NavigationEffectSpawner effectSpawner)
        {
            IMovable mover = new NavMeshAgentMover(_navMeshAgent);

            _controller = new PointClickController(
                new CompositeManipulator(
                    mover,
                    new AlongMoverDirectionRotator(mover, new DirectionRotator(_rotationSpeed))
                ),
                Camera.main,
                LayerMask.GetMask(GroundLayerName),
                effectSpawner
            );

            _controller.Enable();
        }

        private void InitializeView()
        {
            _view = GetComponent<CharacterView>();
            _view.SetController(_controller);
        }

        private void InitializeEffectSpawner()
        {
            _effectSpawner = GetComponent<NavigationEffectSpawner>();
        }


        private void Update()
        {
            _controller.Update(Time.deltaTime);
        }
    }
}
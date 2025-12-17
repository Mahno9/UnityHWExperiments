using System;
using System.Collections.Generic;

using Navigation.Common.Controllers;
using Navigation.Controllers;
using Navigation.Interfaces;
using Navigation.Manipulators;

using UnityEngine;
using UnityEngine.AI;

namespace Navigation.Behaviours
{
    [RequireComponent(typeof(CharacterView))]
    [RequireComponent(typeof(NavigationEffectSpawner))]
    [RequireComponent(typeof(IDamageable))]
    public class MainCharacterInitializer : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private float        _rotationSpeed;
        [SerializeField] private string       _groundLayerName = "Ground";

        private MoveController          _moveController;
        private CharacterView           _view;
        private NavigationEffectSpawner _effectSpawner;
        private DeathController         _deathController;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _effectSpawner = InitializeEffectSpawner();
            _moveController = InitializeMoveController(_effectSpawner);
            _view = InitializeView(_moveController);

            _deathController = InitializeDeathController(_moveController);
        }

        private DeathController InitializeDeathController(params ControllerBase[] controllers)
        {
            return new DeathController(GetComponent<IDamageable>(), controllers);
        }

        private NavigationEffectSpawner InitializeEffectSpawner()
        {
            return GetComponent<NavigationEffectSpawner>();
        }

        private PointClickController InitializeMoveController(NavigationEffectSpawner effectSpawner)
        {
            IMovable mover = new NavMeshAgentMover(_navMeshAgent);

            PointClickController controller = new(
                new CompositeManipulator(
                    mover,
                    new AlongMoverDirectionRotator(mover, new DirectionRotator(_rotationSpeed))
                ),
                Camera.main,
                LayerMask.GetMask(_groundLayerName),
                effectSpawner
            );

            controller.Enable();
            return controller;
        }

        private CharacterView InitializeView(MoveController moveController)
        {
            CharacterView view = GetComponent<CharacterView>();
            view.SetMoveController(moveController);
            return view;
        }

        private void Update()
        {
            _moveController.Update(Time.deltaTime);
        }
    }
}
using System.Collections.Generic;

using Navigation.Controllers;
using Navigation.CoreMechanics.Health;
using Navigation.CoreMechanics.Movement;
using Navigation.CoreMechanics.Rotation;
using Navigation.NavigationEffect;

using UnityEngine;
using UnityEngine.AI;

namespace Navigation.Characters
{
    [RequireComponent(typeof(CharacterView))]
    [RequireComponent(typeof(NavigationEffectSpawner))]
    public class MainCharacterInitializer : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private float        _rotationSpeed;
        [SerializeField] private float        _jumpSpeed;
        [SerializeField] private string       _groundLayerName = "Ground";
        [SerializeField] private float        _maxHealth       = 100;

        private Character               _character;
        private NavigationEffectSpawner _effectSpawner;

        private CharacterView        _view;
        private PointClickController _moveController;

        private readonly List<IUpdatable> _updatables = new();

        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            foreach (IUpdatable updatable in _updatables)
                updatable.Update(Time.deltaTime);
        }

        private void Initialize()
        {
            AgentJumper                agentJumper = new(_jumpSpeed, _navMeshAgent, this);
            NavMeshAgentMover          mover       = new(_navMeshAgent, agentJumper);
            AlongMoverDirectionRotator rotator     = new(new DirectionRotator(transform, _rotationSpeed), mover);

            _character = gameObject.AddComponent<Character>();
            _character.Initialize(new Health(_maxHealth), mover, rotator);

            _moveController = new PointClickController(_character, Camera.main, LayerMask.GetMask(_groundLayerName));
            _moveController.Enable();
            AddUpdatable(_moveController);

            _effectSpawner = InitializeEffectSpawner();
            _moveController.SubscribeOnMovePoints(_effectSpawner);

            _view = InitializeView(_character);
        }


        private void AddUpdatable(IUpdatable updatable) => _updatables.Add(updatable);

        private NavigationEffectSpawner InitializeEffectSpawner()
        {
            return GetComponent<NavigationEffectSpawner>();
        }

        private CharacterView InitializeView(Character character)
        {
            CharacterView view = GetComponent<CharacterView>();
            view.SetCharacter(character);
            return view;
        }
    }
}
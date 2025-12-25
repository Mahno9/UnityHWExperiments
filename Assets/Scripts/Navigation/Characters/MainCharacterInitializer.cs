using System.Collections.Generic;

using Navigation.Controllers;
using Navigation.CoreMechanics.Health;
using Navigation.CoreMechanics.Movement;
using Navigation.CoreMechanics.Rotation;
using Navigation.ForDeletion.Controllers;
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
        [SerializeField] private string       _groundLayerName = "Ground";
        [SerializeField] private float        _maxHealth       = 100;

        private Character               _character;
        private DeathController         _deathController;
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
            NavMeshAgentMover          mover   = new(_navMeshAgent);
            AlongMoverDirectionRotator rotator = new(new DirectionRotator(transform, _rotationSpeed), mover);

            _character = new Character(new Health(_maxHealth), mover, rotator);
            AddUpdatable(_character);

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
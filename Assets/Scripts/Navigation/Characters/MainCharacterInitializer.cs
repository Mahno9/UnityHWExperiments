using Navigation.Common.Controllers;
using Navigation.Damage.Behaviours;
using Navigation.Damage.Interfaces;
using Navigation.FX.Behaviours;
using Navigation.Movement.Controllers;
using Navigation.Movement.Manipulators;
using Navigation.ObjectsFacades;

using UnityEngine;
using UnityEngine.AI;

namespace Navigation.Common.Behaviours
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

        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            _character.Update(Time.deltaTime);
        }

        private void Initialize()
        {
            NavMeshAgentMover mover = new(_navMeshAgent);
            _character = new Character(
                new Health(_maxHealth),
                mover,
                new AlongMoverDirectionRotator(new DirectionRotator(transform, _rotationSpeed), mover)
            );

            _moveController = new PointClickController(_character, Camera.main, LayerMask.GetMask(_groundLayerName));
            _moveController.Enable();

            _effectSpawner = InitializeEffectSpawner();
            _moveController.SubscribeOnMovePoints(_effectSpawner);

            _view = InitializeView(_character);
        }

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
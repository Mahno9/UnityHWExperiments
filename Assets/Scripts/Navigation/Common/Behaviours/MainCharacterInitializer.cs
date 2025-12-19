using Navigation.Common.Controllers;
using Navigation.Damage.Interfaces;
using Navigation.FX.Behaviours;
using Navigation.ObjectsFacades;

using UnityEngine;
using UnityEngine.AI;

namespace Navigation.Common.Behaviours
{
    [RequireComponent(typeof(CharacterView))]
    [RequireComponent(typeof(NavigationEffectSpawner))]
    [RequireComponent(typeof(IHealth))]
    public class MainCharacterInitializer : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private float        _rotationSpeed;
        [SerializeField] private string       _groundLayerName = "Ground";

        private Character               _character;
        private DeathController         _deathController;
        private NavigationEffectSpawner _effectSpawner;

        private CharacterView _view;

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
            _character = new Character(transform, _navMeshAgent, _groundLayerName, _rotationSpeed, GetComponent<IHealth>());

            _effectSpawner = InitializeEffectSpawner();
            _character.SubscribeOnMovePoints(_effectSpawner);

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
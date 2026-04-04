using System.Collections;

using Cinemachine;

using MiniGame.Characters;
using MiniGame.Configs;

using Navigation.CoreMechanics.Rotation;

using UnityEngine;

namespace MiniGame
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private LevelBuilder             _levelBuilder;
        [SerializeField] private GameObject               _spawnPoint;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private MainCharacterConfig      _mainCharConfig;

        private UpdaterService _updaterService;

        private void Awake()
        {
            StartCoroutine(ProcessStart());
            // ProcessStart();
        }

        private IEnumerator ProcessStart()
        {
            _updaterService = new UpdaterService();

            CharactersFactory charactersFactory = new(_updaterService);

            // Load resources

            // Load level
            _levelBuilder.BuildLevelBox(10, 3);

            // Spawn
            charactersFactory.CreateMainCharacter(_mainCharConfig, _spawnPoint.transform.position, _camera);

            yield return null;
        }


        private void Update()
        {
            _updaterService.Update(Time.deltaTime);
        }
    }
}
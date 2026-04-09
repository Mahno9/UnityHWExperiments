using System.Collections;
using System.Linq;

using Cinemachine;

using MiniGame.Characters;
using MiniGame.Configs;

using Navigation.CoreMechanics.Rotation;

using Unity.AI.Navigation;

using UnityEngine;

namespace MiniGame
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private LevelBuilder        _levelBuilder;
        [SerializeField] private GameObject          _spawnPoint;

        [SerializeField] private GameObject           _enemySpawnPoint;

        private UpdaterService _updaterService;
        private EnemySpawner   _spawner;

        private void Awake()
        {
            StartCoroutine(ProcessStart());
            // ProcessStart();
        }

        private IEnumerator ProcessStart()
        {
            _updaterService = new UpdaterService();

            ControllersUpdaterService controllersUpdaterService = new();
            _updaterService.Add(controllersUpdaterService);

            CharactersFactory charactersFactory = new(_updaterService, controllersUpdaterService);

            // Load resources
            MainCharacterConfig mainCharacterConfig = Resources.Load<MainCharacterConfig>("MiniGame/MainCharacterConfig");
            EnemyCharacterConfig enemyCharacterConfig = Resources.Load<EnemyCharacterConfig>("MiniGame/EnemyCharacterConfig");

            // Load level
            Level level = _levelBuilder.BuildLevelBox(4, 3);

            // Spawn
            charactersFactory.CreateMainCharacter(mainCharacterConfig, _spawnPoint.transform);

            _spawner = new EnemySpawner(charactersFactory, enemyCharacterConfig, Enumerable.Range(0, 10).Select(_ => level.GetRandomSpawnPoint()).ToArray());
            _spawner.Spawn(7);

            yield return null;
        }

        // 1. Вынести в компонент DamageableCharacter
        // 2. ControllersUpdater - отдельным сервисом
        // 3. Фасад персонажа - бех

        private void Update()
        {
            _updaterService.Update(Time.deltaTime);
        }
    }
}
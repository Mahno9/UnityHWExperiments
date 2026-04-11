using System.Collections;

using MiniGame.Characters;
using MiniGame.Configs;

using UnityEngine;

namespace MiniGame
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private LevelBuilder _levelBuilder;
        [SerializeField] private GameObject   _spawnPoint;

        [SerializeField] private GameObject _enemySpawnPoint;

        [SerializeField] private float _mainCharacterRadius;

        private UpdaterService _updaterService;

        private void Awake()
        {
            StartCoroutine(ProcessStart());
            // ProcessStart();
        }

        private void Update() => _updaterService.Update(Time.deltaTime);

        private IEnumerator ProcessStart()
        {
            _updaterService = new UpdaterService();

            ControllersUpdaterService controllersUpdaterService = new();
            _updaterService.Add(controllersUpdaterService);

            CharactersFactory charactersFactory = new(controllersUpdaterService);

            // Load resources

            MainCharacterConfig  mainCharacterConfig  = Resources.Load<MainCharacterConfig>(R.MiniGame.MainCharacterConfig);
            EnemyCharacterConfig enemyCharacterConfig = Resources.Load<EnemyCharacterConfig>(R.MiniGame.EnemyCharacterConfig);
            LevelConfig levelConfig = Resources.Load<LevelConfig>(R.MiniGame.LevelConfig);

            // Load level

            Level level = _levelBuilder.BuildLevelBox(4, 3);

            // Spawn

            MainCharacter mainCharacter = charactersFactory.CreateMainCharacter(mainCharacterConfig, _spawnPoint.transform);

            // Start game

            GameMode gameMode = new(levelConfig, charactersFactory, enemyCharacterConfig, level, mainCharacter);
            _updaterService.Add(gameMode);

            gameMode.Start();

            yield return null;
        }
    }
}
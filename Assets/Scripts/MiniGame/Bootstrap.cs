using System.Collections;

using MiniGame.Characters;
using MiniGame.Configs;

using UnityEngine;

namespace MiniGame
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private LevelBuilder _levelBuilder;

        private UpdaterService _updaterService;

        private void Awake()
        {
            StartCoroutine(ProcessStart());
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
            LevelConfig          levelConfig          = Resources.Load<LevelConfig>(R.MiniGame.LevelConfig);

            // Load level

            Level                     level              = _levelBuilder.BuildLevelBox(4, 3);
            SpawnPoseGeneratorService spawnPoseGenerator = new(level);

            // Spawn

            GameplayCycle gameplayCycle = new(charactersFactory, mainCharacterConfig, enemyCharacterConfig, levelConfig, spawnPoseGenerator, this);

            // Start game

            StartCoroutine(gameplayCycle.Launch());
            _updaterService.Add(gameplayCycle);

            yield return null;
        }
    }
}
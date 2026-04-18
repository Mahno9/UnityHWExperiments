using Delegates.Timer;

using MiniGame.Characters;
using MiniGame.Configs;
using MiniGame.LoseConditions;
using MiniGame.WinConditions;

using Navigation.Controllers;
using Navigation.Utils;

using UnityEngine;

namespace MiniGame
{
    public class GameMode : IUpdatable
    {
        public IReactiveVariableReadonly<bool> Win    => _winCondition.IsWin;
        public IReactiveVariableReadonly<bool> Defeat => _loseCondition.IsLost;

        private readonly LevelConfig               _levelConfig;
        private readonly CharactersFactory         _charactersFactory;
        private readonly SpawnPoseGeneratorService _spawnPoseGenerator;
        private readonly MainCharacterConfig       _mainCharacterConfig;
        private          MainCharacter             _mainCharacter;

        private readonly TimerService   _spawnTimer;
        private readonly EnemiesService _enemiesService;

        private IWinCondition  _winCondition;
        private ILoseCondition _loseCondition;

        public GameMode(
            LevelConfig               levelConfig,
            CharactersFactory         charactersFactory,
            EnemyCharacterConfig      enemyCharacterConfig,
            SpawnPoseGeneratorService spawnPoseGenerator,
            MainCharacterConfig             mainCharacterConfig)
        {
            _levelConfig = levelConfig;
            _charactersFactory = charactersFactory;
            _spawnPoseGenerator = spawnPoseGenerator;
            _mainCharacterConfig = mainCharacterConfig;

            EnemySpawner spawner = new(_charactersFactory, enemyCharacterConfig);
            _enemiesService = new EnemiesService(spawner, _spawnPoseGenerator);

            _spawnTimer = new TimerService();
            _spawnTimer.OnTimerStopped += OnSpawnTimerTick;
        }

        public void Start()
        {
            _mainCharacter = _charactersFactory.CreateMainCharacter(_mainCharacterConfig, _spawnPoseGenerator.GetRandomSpawnPoint());

            _spawnPoseGenerator.InitExclude(_mainCharacter.transform, _levelConfig.MainCharacterSpawnExcludeRadius);
            _enemiesService.SpawnEnemies(_levelConfig.StartEnemiesCount);

            LoseConditionsFactory loseConditionsFactory = new(_mainCharacter, _enemiesService);
            _loseCondition = loseConditionsFactory.GetLoseCondition(_levelConfig.LoseConditionConfig);

            WinConditionsFactory winConditionsFactory = new(_enemiesService);
            _winCondition = winConditionsFactory.GetWinCondition(_levelConfig.WinConditionConfig);

            StartSpawnTimer();

            Debug.Log($"GameStarted");
        }

        public void Update(float deltaTime)
        {
            _spawnTimer.Update(deltaTime);
            _enemiesService.Update(deltaTime);

            _loseCondition.Update(deltaTime);
            _winCondition.Update(deltaTime);
        }

        private void OnSpawnTimerTick()
        {
            _enemiesService.SpawnEnemy();

            StartSpawnTimer();
        }

        public void ProcessGameEnd()
        {
            _enemiesService.DestroyEnemies();
            _mainCharacter.Destroy();
        }

        private void StartSpawnTimer() => _spawnTimer.StartTimer(_levelConfig.EnemiesSpawnDelay, true);
    }
}
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

        private readonly IWinCondition  _winCondition;
        private readonly ILoseCondition _loseCondition;

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

            // IDK if this ok - use configs just from a scriptable object
            _winCondition = _levelConfig.WinCondition;
            _loseCondition = _levelConfig.LoseCondition;
        }

        public void Start()
        {
            _mainCharacter = _charactersFactory.CreateMainCharacter(_mainCharacterConfig, _spawnPoseGenerator.GetRandomSpawnPoint());

            _spawnPoseGenerator.InitExclude(_mainCharacter.transform, _levelConfig.MainCharacterSpawnExcludeRadius);
            _enemiesService.SpawnEnemies(_levelConfig.StartEnemiesCount);

            _winCondition.Init(new WinInitData { EnemiesService = _enemiesService });
            _loseCondition.Init(new LoseInitData
            {
                MainCharacter = _mainCharacter,
                EnemiesService = _enemiesService
            });

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
            _enemiesService.Dispose();
            _mainCharacter.Destroy();
        }

        private void StartSpawnTimer() => _spawnTimer.StartTimer(_levelConfig.EnemiesSpawnDelay, true);
    }
}
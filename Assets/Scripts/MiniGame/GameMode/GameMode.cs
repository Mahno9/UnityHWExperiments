using Delegates.Timer;

using MiniGame.Characters;
using MiniGame.Configs;
using MiniGame.LoseConditions;
using MiniGame.WinConditions;

using Navigation.Controllers;
using Navigation.Utils;

namespace MiniGame
{
    public class GameMode : IUpdatable
    {
        public IReactiveVariableReadonly<bool> Win    => _winCondition.IsWin;
        public IReactiveVariableReadonly<bool> Defeat => _loseCondition.IsLost;

        private readonly LevelConfig               _levelConfig;
        private readonly SpawnPoseGeneratorService _spawnPoseGenerator;
        private readonly MainCharacter             _mainCharacter;

        private readonly TimerService   _spawnTimer;
        private readonly EnemiesService _enemiesService;

        private readonly IWinCondition  _winCondition;
        private readonly ILoseCondition _loseCondition;

        public GameMode(
            GameModeOptions           gameModeOptions,
            LevelConfig               levelConfig,
            CharactersFactory         charactersFactory,
            EnemyCharacterConfig      enemyCharacterConfig,
            SpawnPoseGeneratorService spawnPoseGenerator,
            MainCharacter             mainCharacter)
        {
            _levelConfig = levelConfig;
            _spawnPoseGenerator = spawnPoseGenerator;
            _mainCharacter = mainCharacter;

            EnemySpawner spawner = new(charactersFactory, enemyCharacterConfig);
            _enemiesService = new EnemiesService(spawner, _spawnPoseGenerator);

            _spawnTimer = new TimerService();
            _spawnTimer.OnTimerStopped += OnSpawnTimerTick;

            _winCondition = gameModeOptions.WinCondition;
            _loseCondition = gameModeOptions.LoseCondition;
        }

        public void Start()
        {
            _spawnPoseGenerator.Init(_mainCharacter.transform, _levelConfig.MainCharacterSpawnExcludeRadius);
            _enemiesService.SpawnEnemies(_levelConfig.StartEnemiesCount);

            _winCondition.Init(new WinInitData { EnemiesService = _enemiesService });

            _loseCondition.Init(new LoseInitData
            {
                MainCharacter = _mainCharacter,
                EnemiesService = _enemiesService
            });

            StartSpawnTimer();
        }

        public void Update(float deltaTime)
        {
            _spawnTimer.Update(deltaTime);

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
        }


        private void StartSpawnTimer() => _spawnTimer.StartTimer(_levelConfig.EnemiesSpawnDelay);
    }
}
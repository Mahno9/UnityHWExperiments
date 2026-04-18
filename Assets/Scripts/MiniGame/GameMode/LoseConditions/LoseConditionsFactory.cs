using System.ComponentModel;

using MiniGame.Characters;
using MiniGame.LoseConditions.ConfigData;

namespace MiniGame.LoseConditions
{
    public class LoseConditionsFactory
    {
        private readonly EnemiesService _enemiesService;
        private readonly MainCharacter  _mainCharacter;

        public LoseConditionsFactory(MainCharacter mainCharacter, EnemiesService enemiesService)
        {
            _mainCharacter = mainCharacter;
            _enemiesService = enemiesService;
        }

        public ILoseCondition GetLoseCondition(ILoseConditionConfigData configData)
        {
            return configData switch
            {
                LoseOnEnemiesOverflowConfig config => new LoseOnEnemiesOverflow(_enemiesService, config.EnemiesMaxCount),
                LoseOnPlayerDeathConfig => new LoseOnPlayerDeath(_mainCharacter),
                _ => throw new InvalidEnumArgumentException($"Unknown type of argument: {configData}")
            };
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel;

using MiniGame.Characters;
using MiniGame.LoseConditions.ConfigData;

namespace MiniGame.LoseConditions
{
    public class LoseConditionsFactory
    {
        private readonly MainCharacter  _mainCharacter;
        private readonly EnemiesService _enemiesService;

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
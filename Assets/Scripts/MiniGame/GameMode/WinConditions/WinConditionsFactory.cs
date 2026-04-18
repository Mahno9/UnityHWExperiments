using System.ComponentModel;

using MiniGame.WinConditions.ConfigData;

namespace MiniGame.WinConditions
{
    public class WinConditionsFactory
    {
        private readonly EnemiesService _enemiesService;

        public WinConditionsFactory(EnemiesService enemiesService)
        {
            _enemiesService = enemiesService;
        }

        public IWinCondition GetWinCondition(IWinConditionConfigData configData)
        {
            return configData switch
            {
                WinOnSurviveOverTimeConfig config => new WinOnSurviveOverTime(config.SurviveTime),
                WinOnKillAmountConfig config => new WinOnKillAmount(_enemiesService, config.KillsRequired),
                _ => throw new InvalidEnumArgumentException($"Unknown type of argument: {configData}")
            };
        }
    }
}
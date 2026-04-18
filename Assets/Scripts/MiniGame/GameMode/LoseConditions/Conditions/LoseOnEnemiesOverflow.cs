using System.Collections.Generic;

using MiniGame.Characters;

using Navigation.Utils;

namespace MiniGame.LoseConditions
{
    public class LoseOnEnemiesOverflow : LoseConditionBase
    {
        private readonly EnemiesService _enemiesService;
        private readonly int            _enemiesMaxCount;

        public LoseOnEnemiesOverflow(EnemiesService enemiesService, int enemiesMaxCount)
        {
            _enemiesService = enemiesService;
            _enemiesMaxCount = enemiesMaxCount;
        }

        public override void Update(float deltaTime)
        {
            if (_enemiesService.EnemiesCount >= _enemiesMaxCount)
                TriggerLost();
        }
    }
}
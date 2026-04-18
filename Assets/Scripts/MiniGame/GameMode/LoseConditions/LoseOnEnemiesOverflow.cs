using System.Collections.Generic;

using MiniGame.Characters;

using Navigation.Utils;

using UnityEngine;

namespace MiniGame.LoseConditions
{
    public class LoseOnEnemiesOverflow : LoseConditionBase
    {
        [SerializeField] private int                  _enemiesMaxCount;

        private                  EnemiesService _enemiesService;

        public override void Init(LoseInitData data)
        {
            _enemiesService = data.EnemiesService;
        }

        public override void Update(float deltaTime)
        {
            if (_enemiesService.EnemiesCount >= _enemiesMaxCount)
                TriggerLost();
        }
    }
}
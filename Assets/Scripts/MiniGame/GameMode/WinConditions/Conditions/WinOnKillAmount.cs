using UnityEngine;

namespace MiniGame.WinConditions
{
    public class WinOnKillAmount : WinConditionBase
    {
        private readonly EnemiesService _enemiesService;
        private readonly int            _killsRequired;
        private          int            _killsAmount;

        public WinOnKillAmount(EnemiesService enemiesService, int killsRequired)
        {
            _enemiesService = enemiesService;
            _killsRequired  = killsRequired;
            _enemiesService.OnEnemyKilled += OnEnemyDead;
        }

        private void OnEnemyDead()
        {
            _killsAmount++;
            Debug.Log($"Kill amount: {_killsAmount}");
        }

        public override void Update(float deltaTime)
        {
            if (_killsAmount >= _killsRequired)
                TriggerWin();
        }
    }
}
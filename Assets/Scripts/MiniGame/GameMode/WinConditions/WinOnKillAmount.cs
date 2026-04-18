using UnityEngine;

namespace MiniGame.WinConditions
{
    public class WinOnKillAmount : WinConditionBase
    {
        [SerializeField] private int _killsRequired;

        private EnemiesService _enemiesService;
        private int            _killsAmount;

        public override void Init(WinInitData data)
        {
            base.Init(data);
            Resubscribe(data.EnemiesService);
            _killsAmount = 0;
        }

        private void Resubscribe(EnemiesService newEnemiesService)
        {
            if (_enemiesService != null)
                _enemiesService.OnEnemyKilled -= OnEnemyDead;

            _enemiesService = newEnemiesService;
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
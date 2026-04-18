namespace MiniGame.LoseConditions
{
    public class LoseOnEnemiesOverflow : LoseConditionBase
    {
        private readonly int            _enemiesMaxCount;
        private readonly EnemiesService _enemiesService;

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
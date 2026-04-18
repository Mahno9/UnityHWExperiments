using MiniGame.LoseConditions.ConfigData;

using UnityEngine;

namespace MiniGame.LoseConditions
{
    public class LoseOnEnemiesOverflowConfig : ILoseConditionConfigData
    {
        public int EnemiesMaxCount;
    }
}
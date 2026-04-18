using Common.Utils;

using MiniGame.LoseConditions.ConfigData;
using MiniGame.WinConditions.ConfigData;

using UnityEngine;

namespace MiniGame.Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/Gameplay/LevelConfig", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public int   StartEnemiesCount               { get; private set; }
        [field: SerializeField] public float EnemiesSpawnDelay               { get; private set; }
        [field: SerializeField] public float MainCharacterSpawnExcludeRadius { get; private set; }


        [SerializeReference] [SubclassSelector]
        public ILoseConditionConfigData LoseConditionConfig;

        [SerializeReference] [SubclassSelector]
        public IWinConditionConfigData WinConditionConfig;
    }
}
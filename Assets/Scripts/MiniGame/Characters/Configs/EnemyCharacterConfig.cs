using MiniGame.Characters;

using UnityEngine;

namespace MiniGame.Configs
{
    [CreateAssetMenu(fileName = "EnemyCharacterConfig", menuName = "Configs/Gameplay/EnemyCharacterConfig", order = 0)]
    public class EnemyCharacterConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyCharacter Prefab { get; private set; }

        [field: SerializeField]
        [field: Min(0)]
        public float MoveSpeed { get; private set; }

        [field: SerializeField]
        [field: Min(0)]
        public float RotationSpeed { get; private set; }

        [field: SerializeField]
        [field: Min(0)]
        public float StartHealth { get; private set; }

        [field: SerializeField]
        [field: Min(0)]
        public float NewPointRadius { get; private set; }

        [field: SerializeField]
        [field: Min(0)]
        public float IdleTime { get; private set; }

        [field: SerializeField]
        [field: Min(0)]
        public float ContactDamage { get; private set; }
    }
}
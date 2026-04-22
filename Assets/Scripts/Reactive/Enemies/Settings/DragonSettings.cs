using System;

using UnityEngine;

namespace Reactive.Enemies.Enemy
{
    [Serializable]
    public class DragonSettings : EnemySettings
    {
        public Dragon Prefab;
        public float  Wisdom;
    }
}
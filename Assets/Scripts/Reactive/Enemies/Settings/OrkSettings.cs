using System;

namespace Reactive.Enemies.Enemy
{
    [Serializable]
    public class OrkSettings : EnemySettings
    {
        public Ork   Prefab;
        public float Strength;
    }
}
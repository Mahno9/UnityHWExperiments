using System;

using UnityEngine;

namespace Reactive.Enemies.Enemy
{
    [Serializable]
    public class ElfSettings : EnemySettings
    {
        public Elf   Prefab;
        public float Dexterity;
    }
}
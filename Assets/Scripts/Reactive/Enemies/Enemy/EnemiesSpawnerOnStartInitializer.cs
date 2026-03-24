using System;
using System.Collections.Generic;

using UnityEngine;

namespace Reactive.Enemies.Enemy
{
    [RequireComponent(typeof(AreaEnemySpawnerFromSettings))]
    public class EnemiesSpawnerOnStartInitializer : MonoBehaviour
    {
        [SerializeField] private List<EnemySettingsWithCount> _enemiesSettings;

        private AreaEnemySpawnerFromSettings _spawner;

        [Serializable]
        private struct EnemySettingsWithCount
        {
            [Min(0)]             public int           Count;
            [SerializeReference] public EnemySettings Settings;
        }

        private void Awake()
        {
            foreach (var enemiesSetting in _enemiesSettings)
                for (int i = 0; i < enemiesSetting.Count; i++)
                    _spawner.SpawnEnemy(enemiesSetting.Settings, transform);
        }
    }
}
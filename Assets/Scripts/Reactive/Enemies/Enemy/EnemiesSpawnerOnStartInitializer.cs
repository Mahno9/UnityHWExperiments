using System;
using System.Collections.Generic;

using UnityEngine;

using Common.Utils;

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
            [Min(0)] public int Count;

            [SerializeReference] [SubclassSelector]
            public EnemySettings Settings;
        }

        private void Awake()
        {
            _spawner = GetComponent<AreaEnemySpawnerFromSettings>();
        }

        private void Start()
        {
            foreach (var enemiesSetting in _enemiesSettings)
                for (int i = 0; i < enemiesSetting.Count; i++)
                    _spawner.SpawnEnemy(enemiesSetting.Settings, transform);
        }
    }
}
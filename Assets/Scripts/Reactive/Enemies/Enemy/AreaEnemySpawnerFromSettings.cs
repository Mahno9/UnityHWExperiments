using System.IO;

using Delegates.Enemies.Enemy;

using UnityEngine;

namespace Reactive.Enemies.Enemy
{
    public class AreaEnemySpawnerFromSettings : AreaEnemySpawner
    {
        public Delegates.Enemies.Enemy.Enemy SpawnEnemy(EnemySettings enemySettings, Transform enemiesParent)
        {
            switch (enemySettings)
            {
                case OrkSettings orkSettings:
                    Ork ork = base.SpawnEnemy(orkSettings.Prefab, enemiesParent);
                    ork.Initialize(orkSettings.Strength);
                    return ork;

                case ElfSettings elfSettings:
                    Elf elf = base.SpawnEnemy(elfSettings.Prefab, enemiesParent);
                    elf.Initialize(elfSettings.Dexterity);
                    return elf;

                case DragonSettings dragonSettings:
                    Dragon dragon = base.SpawnEnemy(dragonSettings.Prefab, enemiesParent);
                    dragon.Initialize(dragonSettings.Wisdom);
                    return dragon;

                default:
                    throw new InvalidDataException("Invalid enemy settings type");
            }
        }
    }
}
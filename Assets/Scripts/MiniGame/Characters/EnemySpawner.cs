using System.Collections.Generic;
using System.Linq;

using Delegates.Enemies.Enemy;

using MiniGame.Configs;

using UnityEngine;

namespace MiniGame.Characters
{
    public class EnemySpawner
    {
        private readonly CharactersFactory    _charFactory;
        private readonly EnemyCharacterConfig _config;

        public EnemySpawner(CharactersFactory charFactory, EnemyCharacterConfig config, params Pose[] spawnPoints)
        {
            _charFactory = charFactory;
            _config = config;
        }

        public List<EnemyCharacter> Spawn(params Pose[] spawnPoints)
        {
            Shuffle(spawnPoints);

            int                  pointsCount = spawnPoints.Length;

            return spawnPoints
                .Select((t, i) => spawnPoints[i % pointsCount])
                .Select(spawnPosition => _charFactory.CreateEnemyCharacter(_config, spawnPosition))
                .ToList();
        }

        private static void Shuffle(Pose[] array)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }
        }
    }
}
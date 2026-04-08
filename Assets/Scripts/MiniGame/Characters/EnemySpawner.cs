using System.Collections.Generic;

using Delegates.Enemies.Enemy;

using MiniGame.Configs;

using UnityEngine;

namespace MiniGame.Characters
{
    public class EnemySpawner
    {
        private readonly Pose[]          _spawnPoints;
        private readonly CharactersFactory    _charFactory;
        private readonly EnemyCharacterConfig _config;

        public EnemySpawner(CharactersFactory charFactory, EnemyCharacterConfig config, params Pose[] spawnPoints)
        {
            _spawnPoints = spawnPoints;
            _charFactory = charFactory;
            _config = config;
        }

        public List<EnemyCharacter> Spawn(int amount)
        {
            Shuffle(_spawnPoints);

            int                  pointsCount = _spawnPoints.Length;
            List<EnemyCharacter> enemies     = new();

            for (int i = 0; i < amount; i++)
            {
                Pose spawnPosition = _spawnPoints[i % pointsCount];
                enemies.Add(_charFactory.CreateEnemyCharacter(_config, spawnPosition));
            }

            return enemies;
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
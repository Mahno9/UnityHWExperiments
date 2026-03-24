using System;

using Delegates.Enemies.Controllers;
using Delegates.Enemies.EnemiesService;

using Navigation.CoreMechanics.Movement;
using Navigation.CoreMechanics.Rotation;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

using Object = UnityEngine.Object;

namespace Delegates.Enemies.Enemy
{
    using EnemiesService = EnemiesService.EnemiesService;

    public class AreaEnemySpawner : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;

        [Header("Behaviour")] [SerializeField] private float _newMovePointRadius;
        [SerializeField]                       private float _idleTime;

        [Header("System")] [SerializeField] private EnemiesServiceInitializer _enemiesServiceInitializer;
        [SerializeField]                    private SpawnArea                 _spawnArea;

        public Enemy SpawnEnemy(Enemy enemyPrefab, Transform enemiesParent, Func<bool> aliveDelegate = null)
        {
            Enemy newEnemy = Instantiate(enemyPrefab, _spawnArea.GetRandomPoint(), enemyPrefab.transform.rotation, enemiesParent);

            NavMeshAgent               navMeshAgent   = newEnemy.GetComponent<NavMeshAgent>();
            NavMeshAgentMover          mover          = new(navMeshAgent);
            AlongMoverDirectionRotator rotator        = new(new DirectionRotator(newEnemy.transform, _rotationSpeed), mover);
            BrownianMovementController moveController = new(mover, _newMovePointRadius, _idleTime);
            moveController.Enable();

            newEnemy.Initialize(mover, rotator, moveController);

            if (aliveDelegate is not null)
                newEnemy.SetIsAliveDelegate(aliveDelegate);

            _enemiesServiceInitializer.GetService().AddEnemy(newEnemy);

            return newEnemy;
        }
    }
}
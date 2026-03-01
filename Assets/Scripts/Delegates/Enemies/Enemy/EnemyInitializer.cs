using Delegates.Enemies.Controllers;

using Navigation.CoreMechanics.Movement;
using Navigation.CoreMechanics.Rotation;

using UnityEngine;
using UnityEngine.AI;

namespace Delegates.Enemies.Enemy
{
    public class EnemyInitializer : MonoBehaviour
    {
        [Header("Navigation")]
        [SerializeField] private NavMeshAgent _navMeshAgent;

        [SerializeField] private float  _rotationSpeed;
        [SerializeField] private string _groundLayerName = "Ground";

        [Header("Behaviour")]
        [SerializeField] private float _newPointRadius;
        [SerializeField] private float _idleTime;

        private Enemy                      _enemy;
        private BrownianMovementController _moveController;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            NavMeshAgentMover          mover   = new(_navMeshAgent);
            AlongMoverDirectionRotator rotator = new(new DirectionRotator(transform, _rotationSpeed), mover);

            _enemy = gameObject.AddComponent<Enemy>();
            _enemy.Initialize(mover, rotator);

            _moveController = new BrownianMovementController(_enemy, _newPointRadius, _idleTime);
            _moveController.Enable();
        }

        private void Update()
        {
            if (_enemy.IsDead())
                return;

            _moveController.Update(Time.deltaTime);
        }
    }
}
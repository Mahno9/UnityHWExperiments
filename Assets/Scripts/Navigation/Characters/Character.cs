using Navigation.Common.Controllers;
using Navigation.Damage.Interfaces;
using Navigation.Movement.Controllers;
using Navigation.Movement.Interfaces;
using Navigation.Movement.Manipulators;

using UnityEngine;
using UnityEngine.AI;

namespace Navigation.ObjectsFacades
{
    public class Character : IMovable, IHealth, IMovePointBroadcaster
    {
        private DeathController _deathController;

        // Health
        private IHealth        _health;
        private MoveController _moveController;

        // Movement
        private IMovable _mover;
        private float MoveSpeed;

        public Character(Transform transform, NavMeshAgent navMeshAgent, string groundLayerName, float rotationSpeed, IHealth health)
        {
            InitializeMovement(transform, navMeshAgent, groundLayerName, rotationSpeed);
        }

        public Character(PointClickController moveController, IHealth navMeshAgent)
        {
            _moveController = moveController;
            InitializeHealth(health);
            InitializeDeathController(_health, _moveController);
        }

        public void TakeDamage(float damage)
        {
            _health.TakeDamage(damage);
        }

        public float RemainHealth => _health.RemainHealth;

        public bool IsDead()
        {
            return _health.IsDead();
        }

        public void SubscribeOnHealthChange(IHealthChangeSubscriber subscriber)
        {
            _health.SubscribeOnHealthChange(subscriber);
        }

        public float   MoveSpeed     => _mover.MoveSpeed;
        public Vector3 MoveDirection => _mover.MoveDirection;
        public Vector3 Position      => _mover.Position;

        public void SetMovePoint(Vector3 point)
        {
            Debug.LogWarning("Should not use Character.SetMovePoint()");
        }

        public void Update(float deltaTime)
        {
            _moveController.Update(deltaTime);
        }

        public void SubscribeOnMovePoints(IMovePointSubscriber subscriber)
        {
            _moveController.SubscribeOnMovePoints(subscriber);
        }

        private void InitializeDeathController(IHealth health, params ControllerBase[] controllers)
        {
            _deathController = new DeathController(health, controllers);
        }

        private void InitializeHealth(IHealth health)
        {
            _health = health;
        }

        private void InitializeMovement(Transform transform, NavMeshAgent navMeshAgent, string groundLayerName, float rotationSpeed)
        {
            _mover = new NavMeshAgentMover(navMeshAgent);

            _moveController = new PointClickController(
                new CompositeManipulator(
                    _mover,
                    new AlongMoverDirectionRotator(_mover, new DirectionRotator(transform, rotationSpeed))
                ),
                Camera.main,
                LayerMask.GetMask(groundLayerName)
            );
            _moveController.Enable();
        }
    }
}
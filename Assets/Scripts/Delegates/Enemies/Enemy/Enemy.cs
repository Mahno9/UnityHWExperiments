using Navigation.Characters.Interfaces;
using Navigation.CoreMechanics.Movement;
using Navigation.CoreMechanics.Rotation;

using UnityEngine;

namespace Delegates.Enemies.Enemy
{
    public class Enemy : MonoBehaviour, IMovable, IDamageable, IDying
    {
        private AlongMoverDirectionRotator _rotator;
        private NavMeshAgentMover          _mover;
        private bool                       _isDead;

        public float   MoveSpeed     => _mover.MoveSpeed;
        public Vector3 MoveDirection => _mover.MoveDirection;
        public Vector3 Position      => _mover.Position;

        public bool IsDead() => _isDead;

        public void Initialize(NavMeshAgentMover mover, AlongMoverDirectionRotator rotator)
        {
            _mover = mover;
            _rotator = rotator;
        }

        public void Update()
        {
            Update(Time.deltaTime);
        }

        public void Update(float deltaTime)
        {
            _mover.Update(Time.deltaTime);
            _rotator.Update(Time.deltaTime);
        }

        public void SetMovePoint(Vector3 point) => _mover.SetMovePoint(point);

        public void TakeDamage(float damage) => _isDead = true;
    }
}
using Navigation.Characters.Interfaces;
using Navigation.Common.Controllers;
using Navigation.Damage.Behaviours;
using Navigation.Damage.Interfaces;
using Navigation.Movement.Controllers;
using Navigation.Movement.Interfaces;
using Navigation.Movement.Manipulators;

using UnityEngine;
using UnityEngine.AI;

namespace Navigation.ObjectsFacades
{
    public class Character : IMovable, IDamageable, IDying
    {
        private readonly Health                     _health;
        private readonly NavMeshAgentMover          _mover;
        private readonly AlongMoverDirectionRotator _rotator;

        public Character(Health health, NavMeshAgentMover mover, AlongMoverDirectionRotator rotator)
        {
            _health = health;
            _mover = mover;
            _rotator = rotator;
        }

        public void TakeDamage(float damage) => _health.TakeDamage(damage);

        public float RemainHealth => _health.RemainHealth;

        public bool IsDead() => _health.IsDead();

        public float   MoveSpeed     => _mover.MoveSpeed;
        public Vector3 MoveDirection => _mover.MoveDirection;
        public Vector3 Position      => _mover.Position;

        public void SetMovePoint(Vector3 point) => _mover.SetMovePoint(point);

        public void Update(float deltaTime)
        {
            _mover.Update(deltaTime);
        }
    }
}
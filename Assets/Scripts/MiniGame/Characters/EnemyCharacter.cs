using Delegates.Enemies.Controllers;

using Navigation.Characters.Interfaces;
using Navigation.Controllers;
using Navigation.CoreMechanics.Movement;
using Navigation.CoreMechanics.Rotation;

using UnityEngine;

namespace MiniGame.Characters
{
    public class EnemyCharacter : DamageableCharacter, IMovable
    {
        public float   MoveSpeed => _mover.MoveSpeed;
        public Vector3 Position  => _mover.Position;

        private readonly AlongMoverDirectionRotator _rotator;
        private readonly NavMeshAgentMover          _mover;

        public EnemyCharacter(NavMeshAgentMover mover, AlongMoverDirectionRotator rotator, float startHealth, params ControllerBase[] controllers) : base(startHealth, controllers)
        {
            _mover = mover;
            _rotator = rotator;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            _rotator.Update(deltaTime);
        }

        public void SetMovePoint(Vector3 point) => _mover.SetMovePoint(point);
    }
}
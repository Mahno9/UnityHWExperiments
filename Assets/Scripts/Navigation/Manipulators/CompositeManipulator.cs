using Navigation.Interfaces;

using UnityEngine;

namespace Navigation.Manipulators
{
    public class CompositeManipulator : IMovable
    {
        private readonly IMovable   _mover;
        private readonly IUpdatable _rotator;


        public CompositeManipulator(IMovable mover, IUpdatable rotator)
        {
            _mover = mover;
            _rotator = rotator;
        }

        public void Update(float deltaTime)
        {
            _rotator.Update(deltaTime);
            _mover.Update(deltaTime);
        }

        public float   MoveSpeed     => _mover.MoveSpeed;
        public Vector3 MoveDirection => _mover.MoveDirection;
        public Vector3 Position      => _mover.Position;

        public void SetTargetPosition(Vector3 position)
        {
            _mover.SetTargetPosition(position);
        }
    }
}
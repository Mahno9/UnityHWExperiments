using Navigation.Controllers;
using Navigation.CoreMechanics.Rotation;
using UnityEngine;

namespace MiniGame.Characters
{
    public class MainCharacter : DamageableCharacter, ISimpleMovable, IRotatableInPosition
    {
        public Vector3 Position      => _characterController.transform.position;
        public float   RotationSpeed => _rotator.RotationSpeed;

        private readonly CharacterController _characterController;
        private readonly DirectionRotator    _rotator;

        public MainCharacter(CharacterController characterController, DirectionRotator rotator, float health, params ControllerBase[] controllers) : base(health, controllers)
        {
            _characterController = characterController;
            _rotator = rotator;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            // _characterController updates just after move
            _rotator.Update(deltaTime);
        }

        public void Move(Vector3 direction) => _characterController.Move(direction);

        public void SetLookDirection(Vector3 direction) => _rotator.SetLookDirection(direction);
    }
}
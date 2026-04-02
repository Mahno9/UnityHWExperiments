using UnityEngine;

namespace MiniGame.MainCharacter
{
    public class MainCharacter : IMovable
    {
        private CharacterController _characterController;

        public MainCharacter(CharacterController characterController)
        {
            _characterController = characterController;
        }


        public void Move(Vector3 direction) => _characterController.Move(direction);
    }
}
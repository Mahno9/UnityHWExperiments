using Cinemachine;

using MiniGame.Characters.Behaviours;
using MiniGame.Configs;

using Navigation.CoreMechanics.Rotation;

using UnityEngine;

namespace MiniGame.Characters
{
    public class CharactersFactory
    {
        private readonly UpdaterService _updaterService;

        public CharactersFactory(UpdaterService updaterService)
        {
            _updaterService = updaterService;
        }

        public MainCharacter.MainCharacter CreateMainCharacter(MainCharacterConfig config, Vector3 position, CinemachineVirtualCamera camera)
        {
            MainCharacterBeh characterObject = Object.Instantiate(config.Prefab, position, config.Prefab.transform.rotation);

            // TODO: move to prefab too
            camera.LookAt = characterObject.transform;
            camera.Follow = characterObject.transform;

            DirectionRotator directionRotator = new(characterObject.transform, config.RotationSpeed);

            CharacterController characterController = characterObject.GetComponent<CharacterController>();

            MainCharacter.MainCharacter character = new(characterController, directionRotator, config.StartHealth);

            LookAtPointerController rotationController = new(character);
            rotationController.Enable();
            ArrowsMoveController    moveController     = new(character, config.MoveSpeed);
            moveController.Enable();

            _updaterService.Add(rotationController);
            _updaterService.Add(moveController);

            // Add after controllers to process their input
            _updaterService.Add(character);

            return character;
        }
    }
}
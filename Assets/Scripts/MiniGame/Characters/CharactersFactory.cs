using Cinemachine;

using Delegates.Enemies.Controllers;

using MiniGame.Characters.Behaviours;
using MiniGame.Configs;

using Navigation.CoreMechanics.Movement;
using Navigation.CoreMechanics.Rotation;

using UnityEngine;
using UnityEngine.AI;

namespace MiniGame.Characters
{
    public class CharactersFactory
    {
        private readonly UpdaterService _updaterService;

        public CharactersFactory(UpdaterService updaterService)
        {
            _updaterService = updaterService;
        }

        public EnemyCharacter CreateEnemyCharacter(EnemyCharacterConfig config, Pose spawnPoint)
        {
            EnemyCharacterBeh characterObject = Object.Instantiate(config.Prefab, spawnPoint.position, spawnPoint.rotation);

            NavMeshAgent navMeshAgent = characterObject.GetComponent<NavMeshAgent>();
            navMeshAgent.speed = config.MoveSpeed;
            NavMeshAgentMover mover = new(navMeshAgent);

            AlongMoverDirectionRotator rotator = new(characterObject.transform, config.RotationSpeed, mover);

            EnemyCharacter character = new(mover, rotator, config.StartHealth);
            _updaterService.Add(character);

            BrownianMovementController movementController = new(character, config.NewPointRadius, config.IdleTime);
            character.AddController(movementController);

            return character;
        }

        public MainCharacter CreateMainCharacter(MainCharacterConfig config, Transform spawnPoint)
        {
            MainCharacterBeh characterObject = Object.Instantiate(config.Prefab, spawnPoint.position, spawnPoint.rotation);

            CinemachineVirtualCamera camera = Object.Instantiate(config.VirtualCamera);
            camera.LookAt = characterObject.transform;
            camera.Follow = characterObject.transform;

            DirectionRotator directionRotator = new(characterObject.transform, config.RotationSpeed);

            CharacterController characterController = characterObject.GetComponent<CharacterController>();

            MainCharacter character = new(characterController, directionRotator, config.StartHealth);

            LookAtPointerController rotationController = new(character);
            rotationController.Enable();
            ArrowsMoveController moveController = new(character, config.MoveSpeed);
            moveController.Enable();

            character.AddController(rotationController);
            character.AddController(moveController);

            _updaterService.Add(character);

            return character;
        }
    }
}
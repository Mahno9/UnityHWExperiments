using Cinemachine;

using Delegates.Enemies.Controllers;

using MiniGame.Configs;

using Navigation.CoreMechanics.Movement;
using Navigation.CoreMechanics.Rotation;

using UnityEngine;
using UnityEngine.AI;

namespace MiniGame.Characters
{
    public class CharactersFactory
    {
        private readonly UpdaterService            _updaterService;
        private readonly ControllersUpdaterService _controllersUpdaterService;

        public CharactersFactory(UpdaterService updaterService, ControllersUpdaterService controllersUpdaterService)
        {
            _updaterService            = updaterService;
            _controllersUpdaterService = controllersUpdaterService;
        }

        public EnemyCharacter CreateEnemyCharacter(EnemyCharacterConfig config, Pose spawnPoint)
        {
            EnemyCharacter character = Object.Instantiate(config.Prefab, spawnPoint.position, spawnPoint.rotation);

            NavMeshAgent navMeshAgent = character.GetComponent<NavMeshAgent>();
            navMeshAgent.speed = config.MoveSpeed;
            NavMeshAgentMover mover = new(navMeshAgent);

            AlongMoverDirectionRotator rotator = new(character.transform, config.RotationSpeed, mover);

            character.Initialize(mover, rotator, config.StartHealth);

            BrownianMovementController movementController = new(character, config.NewPointRadius, config.IdleTime);
            _controllersUpdaterService.Add(movementController, () => character.IsDestroyed);

            return character;
        }

        public MainCharacter CreateMainCharacter(MainCharacterConfig config, Transform spawnPoint)
        {
            MainCharacter character = Object.Instantiate(config.Prefab, spawnPoint.position, spawnPoint.rotation);

            CinemachineVirtualCamera camera = Object.Instantiate(config.VirtualCamera);
            camera.LookAt = character.transform;
            camera.Follow = character.transform;

            DirectionRotator  directionRotator    = new(character.transform, config.RotationSpeed);
            CharacterController characterController = character.GetComponent<CharacterController>();

            character.Initialize(characterController, directionRotator, config.StartHealth);

            LookAtPointerController rotationController = new(character);
            ArrowsMoveController    moveController     = new(character, config.MoveSpeed);

            _controllersUpdaterService.Add(rotationController, () => character.IsDestroyed);
            _controllersUpdaterService.Add(moveController, () => character.IsDestroyed);

            return character;
        }
    }
}

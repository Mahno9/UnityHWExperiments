using Cinemachine;

using Common.Utils;

using Delegates.Enemies.Controllers;

using MiniGame.Characters.View;
using MiniGame.Configs;
using MiniGame.CoreMechanics.Damage;
using MiniGame.CoreMechanics.Shooting;

using Navigation.CoreMechanics.Movement;
using Navigation.CoreMechanics.Rotation;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.AI;

namespace MiniGame.Characters
{
    public class CharactersFactory
    {
        private readonly ControllersUpdaterService _controllersUpdaterService;
        private readonly UpdaterService            _updaterService;

        public CharactersFactory(ControllersUpdaterService controllersUpdaterService, UpdaterService updaterService)
        {
            _controllersUpdaterService = controllersUpdaterService;
            _updaterService = updaterService;
        }

        public EnemyCharacter CreateEnemyCharacter(EnemyCharacterConfig config, Pose spawnPoint)
        {
            EnemyCharacter character = Object.Instantiate(config.Prefab, spawnPoint.position, spawnPoint.rotation);

            NavMeshAgent navMeshAgent = character.GetComponent<NavMeshAgent>();
            navMeshAgent.speed = config.MoveSpeed;
            NavMeshAgentMover mover = new(navMeshAgent);

            AlongMoverDirectionRotator rotator      = new(character.transform, config.RotationSpeed, mover);
            IDamageDealer              damageDealer = new DamageDealer(config.ContactDamage, TeamId.Enemy);

            character.Initialize(mover, rotator, config.StartHealth, damageDealer);

            BrownianMovementController movementController = new(character, config.NewPointRadius, config.IdleTime);
            _controllersUpdaterService.Add(movementController, () => character.IsDestroyed);

            HealthView healthView = character.GetComponentInChildren<HealthView>(true);
            if (healthView)
                healthView.Initialize(character, character);

            return character;
        }

        public MainCharacter CreateMainCharacter(MainCharacterConfig config, Pose spawnPoint)
        {
            MainCharacter character = Object.Instantiate(config.Prefab, spawnPoint.position, spawnPoint.rotation);

            CinemachineVirtualCamera camera = Object.Instantiate(config.VirtualCamera);
            camera.LookAt = character.transform;
            camera.Follow = character.transform;
            camera.AddComponent<ConditionalDestroyer>().Initialize(() => character.IsDestroyed);

            DirectionRotator    directionRotator    = new(character.transform, config.RotationSpeed);
            CharacterController characterController = character.GetComponent<CharacterController>();
            Shooter             shooter             = new(config.ProjectilePrefab, config.ShootDamage, TeamId.Player);

            character.Initialize(characterController, directionRotator, config.StartHealth, shooter);

            LookAtPointerController rotationController = new(character);
            _controllersUpdaterService.Add(rotationController, () => character.IsDestroyed);

            ArrowsMoveController moveController = new(character, config.MoveSpeed);
            _controllersUpdaterService.Add(moveController, () => character.IsDestroyed);

            ShootController shootController = new(character);
            _controllersUpdaterService.Add(shootController, () => character.IsDestroyed);

            HealthView healthView = character.GetComponentInChildren<HealthView>(true);
            if (healthView)
                healthView.Initialize(character, character);

            return character;
        }
    }
}
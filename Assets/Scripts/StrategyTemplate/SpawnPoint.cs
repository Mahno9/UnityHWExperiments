using System;
using System.Collections.Generic;

using StrategyTemplate.Behaviours;
using StrategyTemplate.Enums;

using UnityEngine;
using UnityEngine.Assertions;

namespace StrategyTemplate
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private BehaviourPicker _enemyPrefab;
        [SerializeField] private IdleBehaviourTypes _idleBehaviourTypes;
        [SerializeField] private AggroBehaviourTypes _aggroBehaviourTypes;

        [SerializeField] private Transform _player;
        [SerializeField] private List<Transform> _patrolPoints;

        private void Start()
        {
            Assert.IsNotNull(_enemyPrefab);

            var behPicker = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            behPicker.Initialize(
                GetBehaviour(behPicker.transform, _idleBehaviourTypes),
                GetBehaviour(behPicker.transform, _aggroBehaviourTypes),
                _player
            );
        }

        private IUpdatableBehaviour GetBehaviour(Transform owner, IdleBehaviourTypes idleBehaviourTypesType)
        {
            return idleBehaviourTypesType switch
            {
                IdleBehaviourTypes.Hold => new HoldBehaviour(owner),
                IdleBehaviourTypes.PatrolPoints => new PatrolPointsBehaviourBase(owner, _patrolPoints),
                IdleBehaviourTypes.PatrolBrownian => new PatrolBrownianBehaviourBase(owner),
                _ => throw new ArgumentOutOfRangeException(nameof(idleBehaviourTypesType), idleBehaviourTypesType, null)
            };
        }

        private IUpdatableBehaviour GetBehaviour(Transform owner, AggroBehaviourTypes aggroBehaviourTypeType)
        {
            return aggroBehaviourTypeType switch
            {
                AggroBehaviourTypes.RunAwayPlayer => new RunAwayBehaviourBase(owner, _player),
                AggroBehaviourTypes.RunToPlayer => new RunToPlayerBehaviourBase(owner, _player),
                AggroBehaviourTypes.FearDeath => new FearDeathBehaviour(owner),
                _ => throw new ArgumentOutOfRangeException(nameof(aggroBehaviourTypeType), aggroBehaviourTypeType, null)
            };
        }
    }
}
using System;
using System.Collections.Generic;

using StrategyTemplate.Behaviours;
using StrategyTemplate.Enums;
using StrategyTemplate.Markers;

using UnityEngine;
using UnityEngine.Assertions;

namespace StrategyTemplate
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private BehaviourPicker _enemyPrefab;
        [SerializeField] private IdleBehaviourTypes _idleBehaviourTypes;
        [SerializeField] private AggroBehaviourTypes _aggroBehaviourTypes;

        [SerializeField] private Player _player;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private List<Transform> _patrolPoints;
        [SerializeField] private Effect _postMortemEffectPrefab;

        private void Start()
        {
            Assert.IsNotNull(_enemyPrefab);

            var behPicker = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            behPicker.Initialize(GetBehaviour(_idleBehaviourTypes), GetBehaviour(_aggroBehaviourTypes));
        }

        private IUpdatableBehaviour GetBehaviour(IdleBehaviourTypes idleBehaviourTypesType)
        {
            return idleBehaviourTypesType switch
            {
                IdleBehaviourTypes.Hold => new HoldBehaviour(),
                IdleBehaviourTypes.PatrolPoints => new PatrolPointsBehaviour(_patrolPoints, _moveSpeed),
                IdleBehaviourTypes.PatrolBrownian => new PatrolBrownianBehaviour(_moveSpeed),
                _ => throw new ArgumentOutOfRangeException(nameof(idleBehaviourTypesType), idleBehaviourTypesType, null)
            };
        }

        private IUpdatableBehaviour GetBehaviour(AggroBehaviourTypes aggroBehaviourTypeType)
        {
            return aggroBehaviourTypeType switch
            {
                AggroBehaviourTypes.RunAwayPlayer => new RunAwayBehaviour(_player, _moveSpeed),
                AggroBehaviourTypes.RunToPlayer => new RunToPlayerBehaviour(_player, _moveSpeed),
                AggroBehaviourTypes.FearDeath => new FearDeathBehaviour(_postMortemEffectPrefab),
                _ => throw new ArgumentOutOfRangeException(nameof(aggroBehaviourTypeType), aggroBehaviourTypeType, null)
            };
        }
    }
}
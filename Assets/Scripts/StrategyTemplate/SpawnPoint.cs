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
        [SerializeField] private List<Transform> _patrolPoints;
        [SerializeField] private Effect _postMortemEffectPrefab;

        private void Start()
        {
            Assert.IsNotNull(_enemyPrefab);

            var behPicker = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            behPicker.Initialize(
                GetBehaviour(behPicker.transform, _idleBehaviourTypes),
                GetBehaviour(behPicker.transform, _aggroBehaviourTypes)
            );
        }

        private UpdatableBehaviourBase GetBehaviour(Transform owner, IdleBehaviourTypes idleBehaviourTypesType)
        {
            return idleBehaviourTypesType switch
            {
                IdleBehaviourTypes.Hold => new HoldBehaviour(owner),
                IdleBehaviourTypes.PatrolPoints => new PatrolPointsBehaviour(owner, _patrolPoints),
                IdleBehaviourTypes.PatrolBrownian => new PatrolBrownianBehaviour(owner),
                _ => throw new ArgumentOutOfRangeException(nameof(idleBehaviourTypesType), idleBehaviourTypesType, null)
            };
        }

        private UpdatableBehaviourBase GetBehaviour(Transform owner, AggroBehaviourTypes aggroBehaviourTypeType)
        {
            return aggroBehaviourTypeType switch
            {
                AggroBehaviourTypes.RunAwayPlayer => new RunAwayBehaviourRunBehaviour(owner, _player),
                AggroBehaviourTypes.RunToPlayer => new RunToPlayerBehaviourRunBehaviour(owner, _player),
                AggroBehaviourTypes.FearDeath => new FearDeathBehaviour(owner, _postMortemEffectPrefab),
                _ => throw new ArgumentOutOfRangeException(nameof(aggroBehaviourTypeType), aggroBehaviourTypeType, null)
            };
        }
    }
}
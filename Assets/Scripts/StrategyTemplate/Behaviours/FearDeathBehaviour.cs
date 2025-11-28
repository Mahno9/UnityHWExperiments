using StrategyTemplate.Markers;

using Unity.VisualScripting;

using UnityEngine;

namespace StrategyTemplate.Behaviours
{
    public class FearDeathBehaviour : UpdatableBehaviourBase
    {
        private readonly Effect _postMortemEffectPrefab;

        public FearDeathBehaviour(Transform owner, Effect postMortemEffectPrefab) : base(owner)
        {
            _postMortemEffectPrefab = postMortemEffectPrefab;
        }

        public override void Update(float deltaTime)
        {
            if (Owner.IsDestroyed())
                return;

            Object.Instantiate(_postMortemEffectPrefab, Owner.transform.position, Owner.rotation);
            Object.Destroy(Owner.gameObject);
        }
    }
}
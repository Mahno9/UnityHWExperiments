using StrategyTemplate.Markers;

using Unity.VisualScripting;

using UnityEngine;

namespace StrategyTemplate.Behaviours
{
    public class FearDeathBehaviour : IUpdatableBehaviour
    {
        private readonly Effect _postMortemEffectPrefab;

        public FearDeathBehaviour(Effect postMortemEffectPrefab)
        {
            _postMortemEffectPrefab = postMortemEffectPrefab;
        }

        public void Update(float deltaTime, Transform owner)
        {
            if (owner.IsDestroyed())
                return;

            Object.Instantiate(_postMortemEffectPrefab, owner.transform.position, owner.rotation);
            Object.Destroy(owner.gameObject);
        }
    }
}
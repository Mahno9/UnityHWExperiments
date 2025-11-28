using StrategyTemplate.Markers;

using UnityEngine;

namespace StrategyTemplate.Actions
{
    public interface IKillable
    {
        void Kill();
    }

    public class SelfKiller : MonoBehaviour, IKillable
    {
        [SerializeField] private Effect _postMortemEffectPrefab;

        public void Kill()
        {
            Instantiate(_postMortemEffectPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
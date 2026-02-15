using Navigation.CoreMechanics.Movement.Interfaces;

using UnityEngine;

namespace Navigation.NavigationEffect
{
    public class NavigationEffectSpawner : MonoBehaviour, IMovePointSubscriber
    {
        [SerializeField] private GameObject _effectPrefab;

        public void OnNewMovePoint(Vector3 position)
        {
            Instantiate(_effectPrefab, position, _effectPrefab.transform.rotation);
        }
    }
}
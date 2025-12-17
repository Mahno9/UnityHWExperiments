using Navigation.Controllers;

using UnityEngine;

namespace Navigation.Behaviours
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
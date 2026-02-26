using System;

using Navigation.Characters.Interfaces;

using UnityEngine;

namespace Navigation.Heal
{
    [RequireComponent(typeof(SphereCollider))]
    public class Heal : MonoBehaviour
    {
        [SerializeField] private float      _healthPoints = 10;
        [SerializeField] private GameObject _healTakenFXPrefab;

        private void Awake()
        {
            SphereCollider healCollider = GetComponent<SphereCollider>();
            Vector3        center       = transform.position + healCollider.center;

            Collider[] colliders = Physics.OverlapSphere(center, healCollider.radius);
            foreach (Collider foundCollider in colliders)
            {
                if (foundCollider.transform.TryGetComponent(out IHealable healable))
                {
                    DoHeal(healable);
                    break;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IHealable healable))
                DoHeal(healable);
        }

        private void DoHeal(IHealable healable)
        {
            healable.Heal(_healthPoints);
            DestroyWithEffects();
        }

        private void DestroyWithEffects()
        {
            Instantiate(_healTakenFXPrefab, transform.position, _healTakenFXPrefab.transform.rotation);
            Destroy(gameObject);
        }
    }
}
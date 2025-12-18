using System;
using System.Collections.Generic;

using Navigation.Interfaces;

using UnityEngine;

namespace Navigation.Damage.Behaviours
{
    [RequireComponent(typeof(Collider))]
    public class Mine : MonoBehaviour
    {
        [SerializeField] private GameObject _explosionEffectPrefab;
        [SerializeField] private GameObject _countDownVisual;

        [SerializeField] private float _detonationTime;
        [SerializeField] private float _damage;


        private readonly List<IDamageable> _targets    = new();
        private          float             _remainTime = float.PositiveInfinity;

        private void Update()
        {
            _remainTime -= Time.deltaTime;
            if (_remainTime <= 0)
                Detonate();
        }

        private void OnTriggerEnter(Collider other)
        {
            IDamageable target = other.transform.GetComponent<IDamageable>();
            if (target is not null)
                _targets.Add(target);

            StartCountDown();
        }

        private void StartCountDown()
        {
            if (float.IsPositiveInfinity(_remainTime) == false)
                return;

            _remainTime = _detonationTime;
            _countDownVisual.SetActive(true);
        }

        private void OnTriggerExit(Collider other)
        {
            IDamageable target = other.transform.GetComponent<IDamageable>();
            if (target is not null)
                _targets.Remove(target);
        }

        private void Detonate()
        {
            foreach (IDamageable target in _targets)
                target.TakeDamage(_damage);

            Instantiate(_explosionEffectPrefab, transform.position, _explosionEffectPrefab.transform.rotation);
            Destroy(gameObject);
        }
    }
}
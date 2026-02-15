using System.Collections;

using Navigation.CoreMechanics.Damage;
using Navigation.Utils;

using UnityEngine;

namespace Navigation.Mine
{
    [RequireComponent(typeof(SphereCollider))]
    public class Mine : DestroyOnDie
    {
        [SerializeField] private GameObject _explosionEffectPrefab;
        [SerializeField] private GameObject _countDownVisualNode;

        [SerializeField] private float _detonationTime;
        [SerializeField] private float _damage;
        [SerializeField] private float _explosionRadius;

        private Timer        _countDownExplosionTimer;
        private DamageDealer _explosion;

        private void Awake()
        {
            SphereCollider mineCollider = GetComponent<SphereCollider>();
            _explosion = new DamageDealer(
                _damage,
                new SphereTargetsDetector(transform.position + mineCollider.center, _explosionRadius)
            );
        }

        private void Update()
        {
            _countDownExplosionTimer?.Update(Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            ExplosionTrigger explosionExplosionTrigger = other.transform.GetComponent<ExplosionTrigger>();
            if (explosionExplosionTrigger is not null)
                StartCountDown();
        }

        public override void TakeDamage(float damage)
        {
            StartCountDown();
        }

        private void StartCountDown()
        {
            StartCoroutine(TickTilExplosion());
        }

        private IEnumerator TickTilExplosion()
        {
            if (_countDownExplosionTimer is not null)
                yield break;

            float timeTilExplosion = _detonationTime;
            _countDownVisualNode.SetActive(true);

            while (timeTilExplosion > 0)
            {
                timeTilExplosion -= Time.deltaTime;
                yield return null;
            }

            Explode();
        }

        private void Explode()
        {
            _explosion.DealDamage();
            DestroyWithEffect();
        }

        private void DestroyWithEffect()
        {
            Instantiate(_explosionEffectPrefab, transform.position, _explosionEffectPrefab.transform.rotation);
            Destroy(gameObject);
        }
    }
}
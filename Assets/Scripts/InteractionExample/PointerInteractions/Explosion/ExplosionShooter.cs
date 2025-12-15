using InteractionExample.PointerInteractions.Logic;

using StrategyTemplate.Markers;

using UnityEngine;
using UnityEngine.Assertions;

namespace InteractionExample.PointerInteractions.Behaviours
{
    public class ExplosionShooter : MonoBehaviour
    {
        private const int RightMouseButton = 1;

        [SerializeField] private float  _explosionRadius = 3f;
        [SerializeField] private float  _explosionForce  = 2000;
        [SerializeField] private Effect _explosionEffect;

        private Camera                _camera;
        private ExplosionShooterLogic _shooter;

        private void Awake()
        {
            _camera = Camera.main;
            Assert.IsNotNull(_camera);

            _shooter = new ExplosionShooterLogic(_explosionRadius, _explosionForce);
        }

        private void Update()
        {
            Ray pointerRay = _camera.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(RightMouseButton))
                ProcessShoot(pointerRay);
        }

        private void ProcessShoot(Ray pointerRay)
        {
            if (_shooter.Shoot(pointerRay, out Vector3 explosionPoint))
                Instantiate(_explosionEffect, explosionPoint, _explosionEffect.transform.rotation);
        }
    }
}
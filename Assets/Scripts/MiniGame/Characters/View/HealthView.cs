using MiniGame.CoreMechanics.Damage;

using UnityEngine;
using UnityEngine.UI;

namespace MiniGame.Characters.View
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;
        private                  Camera _camera;
        private                  IDying _dying;

        private IHaveHealth _healthContainer;
        private float       _maxHealth;

        private void Awake()
        {
            _camera = Camera.main;
            gameObject.SetActive(false);
        }

        private void LateUpdate()
        {
            transform.rotation = _camera.transform.rotation;
        }

        private void OnDestroy()
        {
            if (_healthContainer != null) _healthContainer.Health.Changed -= OnHealthChanged;
            if (_dying != null) _dying.IsDead.Changed -= OnDead;
        }

        public void Initialize(IHaveHealth charHealth, IDying charDying)
        {
            _healthContainer = charHealth;
            _dying = charDying;

            if (_healthContainer == null) return;

            _maxHealth = _healthContainer.Health.Value;
            _healthSlider.minValue = 0;
            _healthSlider.maxValue = _maxHealth;
            _healthSlider.value = _maxHealth;

            _healthContainer.Health.Changed += OnHealthChanged;
            _dying.IsDead.Changed += OnDead;
        }

        private void OnHealthChanged(float _, float newValue)
        {
            _healthSlider.value = newValue;
            gameObject.SetActive(newValue < _maxHealth);
        }

        private void OnDead(bool _, bool isDead)
        {
            if (isDead) gameObject.SetActive(false);
        }
    }
}
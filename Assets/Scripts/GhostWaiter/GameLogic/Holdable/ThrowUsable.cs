using GhostWaiter.VFX;

using UnityEngine;
using UnityEngine.Assertions;

namespace GhostWaiter.GameLogic.Holdable
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(DestroyerWithEffect))]
    public class ThrowUsable : Usable
    {
        [SerializeField] private Transform _visualShift;

        [SerializeField] private float _throwAngle = 30f;
        [SerializeField] private float _throwForce = 3f;

        [SerializeField] private TrailRenderer _trail;

        [SerializeField] private float _damage = 1;

        private Rigidbody _body;
        private Collider _collider;
        private DestroyerWithEffect _destroyer;

        private bool _isInWashArea = false;
        private Health _targetHealth;

        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
            _body.isKinematic = true;

            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;

            _destroyer = GetComponent<DestroyerWithEffect>();

            if (_trail is not null)
                _trail.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<WashArea>())
                _isInWashArea = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<WashArea>())
                _isInWashArea = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_isInWashArea == false)
                _targetHealth?.AddHealth(-_damage);

            _destroyer.DestroyWithEffect(_isInWashArea);
        }

        public override void Use(GameObject targetObject)
        {
            ThrowSelf(targetObject);
        }

        private void ThrowSelf(GameObject targetObject)
        {
            Assert.IsNotNull(Holder, "Owner is null in Throw.Use");

            StoreTargetHealth(targetObject);
            ResetVisualPos();
            SetStartPos();
            Launch();
            UnbindFromHolder();
            EnableTrail();
        }

        private void StoreTargetHealth(GameObject targetObject)
        {
            Health newTargetHealth = targetObject.GetComponent<Health>();
            if (newTargetHealth is not null)
                _targetHealth = newTargetHealth;
        }

        private void EnableTrail()
        {
            if (_trail is not null)
                _trail.enabled = true;
        }

        private void UnbindFromHolder()
        {
            transform.SetParent(null, true);
            SetHolder(null);
        }

        private void ResetVisualPos()
        {
            if (_visualShift != null)
                _visualShift.transform.localPosition = Vector3.zero;
        }

        private void SetStartPos()
        {
            Transform jointTransform = Holder.GetJointTransform();
            gameObject.transform.position = jointTransform.position;
            gameObject.transform.rotation = jointTransform.rotation;
        }

        private void Launch()
        {
            _body.isKinematic = false;
            _body.velocity = gameObject.transform.rotation * Quaternion.Euler(-_throwAngle, 0, 0) * Vector3.forward * _throwForce;

            // Мне очень нужен был этот костыль. Если знаешь как сделать лучше, подскажи, пожалуйста
            StartCoroutine(SetColliderAsNonTriggerAfterDelay());
        }

        private System.Collections.IEnumerator SetColliderAsNonTriggerAfterDelay()
        {
            yield return new WaitForSeconds(0.1f);
            _collider.isTrigger = false;
        }
    }
}

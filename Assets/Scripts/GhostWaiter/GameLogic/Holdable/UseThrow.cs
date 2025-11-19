using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(DestroyerWithEffect))]
public class UseThrow : Usable
{
    [SerializeField] private Transform _visualShift;

    [SerializeField] private float _throwAngle = 30f;
    [SerializeField] private float _throwForce = 3f;

    [SerializeField] private TrailRenderer _trail;

    [SerializeField] private float _damage = 1;

    private Rigidbody _body;
    private Collider _collider;
    private DestroyerWithEffect _destroyer;

    private WaiterGameState _gameState;
    private bool _isInWashArea = false;

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

    public override void Init(WaiterGameState gameState)
    {
        _gameState = gameState;
        base.Init(gameState);
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
            _gameState.AddHealth(-_damage);

        _destroyer.DestroyWithEffect(_isInWashArea);
    }

    public override void Use()
    {
        Assert.IsNotNull(Holder, "Owner is null in Throw.Use");

        ResetVisualPos();
        SetStartPos();
        Launch();
        UnbindFromHolder();
        EnableTrail();
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

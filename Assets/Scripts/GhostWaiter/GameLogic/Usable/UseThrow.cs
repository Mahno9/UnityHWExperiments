using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class UseThrow : Usable
{
    [SerializeField] private Transform _visualShift;

    [SerializeField] private float _throwAngle = 30f;
    [SerializeField] private float _throwForce = 3f;

    [SerializeField] private GameObject _clearExplosionFx;
    [SerializeField] private GameObject _dirtyExplosionFx;

    private Rigidbody _body;
    private Collider _collider;

    private bool _isInWashArea = false;

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
        _body.isKinematic = true;

        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
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
        //GameObject explosionFx = _isInWashArea ? _clearExplosionFx : _dirtyExplosionFx;
        //Instantiate(explosionFx, transform.position, Quaternion.identity);
        Debug.Log($"Explosion clear: {_isInWashArea}");
    }

    public override void Use()
    {
        Assert.IsNotNull(Owner, "Owner is null in Throw.Use");

        ResetVisualPos();
        SetStartPos();
        Launch();
    }

    private void ResetVisualPos()
    {
        if (_visualShift != null)
            _visualShift.transform.localPosition = Vector3.zero;
    }

    private void SetStartPos()
    {
        transform.SetParent(null, true);

        Transform jointTransform = Owner.GetJointTransform();
        gameObject.transform.position = jointTransform.position;
        gameObject.transform.rotation = jointTransform.rotation;
    }

    private void Launch()
    {
        _body.isKinematic = false;
        _body.velocity = gameObject.transform.rotation * Quaternion.Euler(-_throwAngle, 0, 0) * Vector3.forward * _throwForce;
        _collider.isTrigger = false;
    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private Rigidbody _rigidbody;
    private Transform _rotationModel;

    private void Awake()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
        if (_rotationModel == null)
            _rotationModel = transform;
    }

    private void Update()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    public void ProcessMoveTo(Vector3 normDirection)
    {
        _rigidbody.velocity = normDirection * _speed;
    }

    public void ProcessRotateTo(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        float step = _rotationSpeed * Time.deltaTime;

        _rotationModel.rotation = Quaternion.RotateTowards(_rotationModel.rotation, lookRotation, step);
    }
}

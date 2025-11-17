using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private Rigidbody _rigidbody;
    private Transform _rotationModel;

    private Vector3 _moveShiftCumulative;

    private void Awake()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
        if (_rotationModel == null)
            _rotationModel = transform;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _moveShiftCumulative * _speed * Time.fixedDeltaTime;
        _moveShiftCumulative = Vector3.zero;
    }

    public void ProcessMoveTo(Vector3 normDirection)
    {
        _moveShiftCumulative = (_moveShiftCumulative + normDirection).normalized;
    }

    public void ProcessRotateTo(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        float step = _rotationSpeed * Time.deltaTime;

        _rotationModel.rotation = Quaternion.RotateTowards(_rotationModel.rotation, lookRotation, step);
    }

    public void IncreaseMoveSpeedBy(float speedMultiplier)
    {
        _speed *= speedMultiplier;
    }
}

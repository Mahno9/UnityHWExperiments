using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMover : Progressable
{
    [SerializeField] private float _speed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _rotationMaxSpeed;

    private Rigidbody _rigidbody;
    private Transform _rotationModel;

    private Vector3 _moveShiftCumulative;

    private float _startSpeed;

    private void Awake()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
        if (_rotationModel == null)
            _rotationModel = transform;

        _startSpeed = _speed;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _moveShiftCumulative * (_speed * Time.fixedDeltaTime);
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
        _speed = Mathf.Min(_speed * speedMultiplier, _maxSpeed);
        _rotationSpeed = Mathf.Min(_rotationSpeed * speedMultiplier, _rotationSpeed);
    }

    public override float GetProgress()
    {
        return (_speed - _startSpeed) / (_maxSpeed - _startSpeed);
    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GroundSwitcher))]
public class Movement : MonoBehaviour
{
    [SerializeField] float movementForce;
    [SerializeField] float jumpForce;

    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";
    private const float InputDeadZone = 0.1f;
    private const float Invert = -1;

    Rigidbody _rigidbody;
    GroundSwitcher _ground;

    float _horizontalMoveDirection;

    private bool _needJump = false; // Use NeedJump property to access and expire
    private bool NeedJump
    {
        get
        {
            if (_needJump)
            {
                _needJump = false;
                return true;
            }
            return false;
        }
        set
        {
            _needJump = value;
        }
    }


    private Vector3 GravityNormal { get { return Physics.gravity.normalized; } }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _ground = GetComponent<GroundSwitcher>();
    }

    private void Update()
    {
        if (Input.GetAxisRaw(VerticalAxisName) > InputDeadZone && _ground.IsGrounded)
            NeedJump = true;

        float horizontalAxisValue = Input.GetAxisRaw(HorizontalAxisName);
        _horizontalMoveDirection = Mathf.Abs(horizontalAxisValue) > InputDeadZone ? horizontalAxisValue : 0;
    }

    private void FixedUpdate()
    {
        Vector3 movingForce = _ground.WorldForwardNormal * movementForce * _horizontalMoveDirection;
        _rigidbody.AddForce(movingForce, ForceMode.Acceleration);
        Debug.DrawLine(transform.position, transform.position + movingForce, Color.yellow);

        if (NeedJump)
        {
            Vector3 jumpingForce = _ground.GravityNormal * jumpForce * Invert;
            _rigidbody.AddForce(jumpingForce, ForceMode.Impulse);
            Debug.DrawLine(transform.position, transform.position + jumpingForce, Color.cyan);
        }
    }
}

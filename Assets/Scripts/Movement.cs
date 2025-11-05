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

    private Rigidbody _rigidbody;
    private GroundSwitcher _ground;

    private float _horizontalMoveDirection; // Use HorizontalMoveDirection property to access and expire
    private float HorizontalMoveDirection
    {
        get
        {
            if (_horizontalMoveDirection != 0)
            {
                float tmp = _horizontalMoveDirection;
                _horizontalMoveDirection = 0;
                return tmp;
            }
            return 0;
        }
        set { _horizontalMoveDirection = value; }
    }

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
        if (_ground.IsGrounded)
        {
            if (Input.GetAxisRaw(VerticalAxisName) > InputDeadZone)
                NeedJump = true;

            float horizontalAxisValue = Input.GetAxisRaw(HorizontalAxisName);
            HorizontalMoveDirection = Mathf.Abs(horizontalAxisValue) > InputDeadZone ? horizontalAxisValue : 0;
        }
    }

    private void FixedUpdate()
    {
        Vector3 movingForce = _ground.WorldForwardNormal * movementForce * HorizontalMoveDirection;
        _rigidbody.AddForce(movingForce, ForceMode.Acceleration);

        if (NeedJump)
        {
            Vector3 jumpingForce = _ground.GravityNormal * jumpForce * Invert;
            _rigidbody.AddForce(jumpingForce, ForceMode.Impulse);
        }
    }
}

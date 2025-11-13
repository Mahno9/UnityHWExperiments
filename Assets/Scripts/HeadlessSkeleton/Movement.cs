using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GroundSwitcher))]
public class Movement : MonoBehaviour
{
    private const string HORIZONTAL_AXIS_NAME = "Horizontal";
    private const string VERTICAL_AXIS_NAME = "Vertical";
    private const float INPUT_DEAD_ZONE = 0.1f;
    private const float IVNERT = -1;

    [SerializeField] private float _movementForce;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _airMovementReduce = 2.0f;

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


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _ground = GetComponent<GroundSwitcher>();
    }

    private void Update()
    {
        if (_ground.IsGrounded)
        {
            ProcessJump();
            ProcessMove(1.0f);
        }
        else
        {
            ProcessMove(_airMovementReduce);
        }
    }

    private void ProcessJump()
    {
        if (Input.GetAxisRaw(VERTICAL_AXIS_NAME) < INPUT_DEAD_ZONE)
            return;

        NeedJump = true;
    }

    private void ProcessMove(float movementReduce)
    {
        float horizontalAxisValue = Input.GetAxisRaw(HORIZONTAL_AXIS_NAME);
        HorizontalMoveDirection = Mathf.Abs(horizontalAxisValue) > INPUT_DEAD_ZONE ? horizontalAxisValue / movementReduce : 0;
    }

    private void FixedUpdate()
    {
        Vector3 movingForce = _movementForce * HorizontalMoveDirection * _ground.WorldForwardNormal;
        _rigidbody.AddForce(movingForce, ForceMode.Acceleration);

        if (NeedJump)
        {
            Vector3 jumpingForce = _jumpForce * IVNERT * _ground.GravityNormal;
            _rigidbody.AddForce(jumpingForce, ForceMode.Impulse);
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

[RequireComponent(typeof(SphereCollider))]
public class GroundSwitcher : MonoBehaviour
{
    private const float CHECK_ANGLE_BASE = 0f;
    private const float CHECK_ANGLE_LEFT = -40f;
    private const float CHECK_ANGLE_RIGHT = 40f;

    [SerializeField] private float groundCheckDistanceTail = 1.1f;

    private float _objectRadius;
    private Vector3 _objectCenterShift;

    private GravityUpdaterStrategy _gravityUpdater;

    public Vector3 GravityNormal => Physics.gravity.normalized;

    private Vector3 ObjectCenter => _objectCenterShift + transform.position;

    private float GroundCheckDistance => _objectRadius * groundCheckDistanceTail;

    public bool IsGrounded
    {
        get
        {
            return
                CheckIsGrounded(CHECK_ANGLE_BASE) ||
                CheckIsGrounded(CHECK_ANGLE_LEFT) ||
                CheckIsGrounded(CHECK_ANGLE_RIGHT);
        }
    }

    public Vector3 WorldForwardNormal
    {
        get
        {
            return Vector3.Cross(GravityNormal, Vector3.back).normalized;
        }
    }

    private void Awake()
    {
        SphereCollider collider = GetComponent<SphereCollider>();
        _objectCenterShift = collider.bounds.center - transform.position;
        _objectRadius = collider.radius * transform.localScale.y;
    }

    private void Update()
    {
        _gravityUpdater?.OnUpdate(ObjectCenter);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (_gravityUpdater == null)
            return;

        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.gameObject.GetComponent<GravityUpdaterStrategy>() == null)
                continue;

            _gravityUpdater.OnGrounding(-contact.normal);
            return;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        _gravityUpdater = null;

        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.gameObject.GetComponent<GravityUpdaterStrategy>() == null)
                continue;

            GravityUpdaterStrategy gravityUpdater = collision.collider.gameObject.GetComponent<GravityUpdaterStrategy>();
            _gravityUpdater = gravityUpdater;

            _gravityUpdater.OnGrounding(-collision.contacts[0].normal);
            return;
        }
    }

    bool CheckIsGrounded(float degreeToCheck)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, degreeToCheck);

        //RuntimeDebugLine.DrawLine(ObjectCenter, ObjectCenter + GravityNormal * GroundCheckDistance, Color.red, 1);

        Physics.Raycast(ObjectCenter, rotation * GravityNormal, out RaycastHit hitInfo, GroundCheckDistance);
        Collider collider = hitInfo.collider?.gameObject?.GetComponent<Collider>();
        return collider != null && collider.isTrigger == false;
    }
}

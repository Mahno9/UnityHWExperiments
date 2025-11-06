using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(SphereCollider))]
public class GroundSwitcher : MonoBehaviour
{
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
            //RuntimeDebugLine.DrawLine(ObjectCenter, ObjectCenter + GravityNormal * GroundCheckDistance, Color.red, 1);
            Physics.Raycast(ObjectCenter, GravityNormal, out RaycastHit hitInfo, GroundCheckDistance);
            Collider collider = hitInfo.collider?.gameObject?.GetComponent<Collider>();
            return collider != null && collider.isTrigger == false;
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
}

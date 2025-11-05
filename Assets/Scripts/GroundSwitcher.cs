using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Collider))]
public class GroundSwitcher : MonoBehaviour
{
    const float GroundCheckDistanceTail = 0.01f;

    private float _objectHalfSize;
    private Vector3 _objectCenterShift;


    private GravityUpdaterStrategy _gravityUpdater;

    public Vector3 GravityNormal => Physics.gravity.normalized;

    private Vector3 ObjectCenter => _objectCenterShift + transform.position;

    private float GroundCheckDistance => _objectHalfSize + GroundCheckDistanceTail;

    public bool IsGrounded
    {
        get
        {
            return Physics.Raycast(ObjectCenter, GravityNormal, GroundCheckDistance);
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
        Collider collider = GetComponent<Collider>();
        _objectCenterShift = collider.bounds.center - transform.position;
        _objectHalfSize = collider.bounds.extents.y;
    }

    private void Update()
    {
        _gravityUpdater?.OnUpdate(ObjectCenter);
    }

    private void OnCollisionStay(Collision collision)
    {
        _gravityUpdater?.OnGrounding(-collision.contacts[0].normal);
    }

    void OnCollisionEnter(Collision collision)
    {
        GravityUpdaterStrategy gravityUpdater = collision.collider.gameObject.GetComponent<GravityUpdaterStrategy>();
        if (gravityUpdater != null)
            _gravityUpdater = gravityUpdater;

        _gravityUpdater?.OnGrounding(-collision.contacts[0].normal);
    }
}

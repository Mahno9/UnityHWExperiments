using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GroundSwitcher : MonoBehaviour
{
    private float _objectHalfSize;
    private Vector3 _objectCenterShift;
    const float GroundCheckDistance = 0.01f;

    public Vector3 GravityNormal => Physics.gravity.normalized;

    private Vector3 ObjectCenter => _objectCenterShift + transform.position;

    public bool IsGrounded
    {
        get
        {
            Debug.DrawLine(_objectCenterShift + transform.position, _objectCenterShift + transform.position + GravityNormal * (_objectHalfSize + GroundCheckDistance), Color.green);
            return Physics.Raycast(ObjectCenter, GravityNormal, _objectHalfSize + GroundCheckDistance);
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
}

using UnityEngine;

public abstract class Usable : MonoBehaviour
{
    private User _owner;
    protected User Owner => _owner;

    public abstract void Use();

    public void SetOwner(User owner)
    {
        _owner = owner;

        transform.SetParent(_owner.transform);
        transform.SetParent(owner.GetJointTransform());
        transform.localPosition = Vector3.zero;
    }
}

using UnityEngine;
using UnityEngine.Assertions;

public class User : MonoBehaviour
{
    [SerializeField] private Transform _jointTransform;

    private Usable _heldUsable;

    public Transform GetJointTransform()
    {
        Assert.IsNotNull(_jointTransform, "_jointTransform is not assigned in the inspector.");
        return _jointTransform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_heldUsable != null)
            return;

        Holder holder = other.GetComponent<Holder>();
        if (holder == null || holder.IsEmpty)
            return;

        Hold(holder.ExtractUsable());
    }

    public void Use()
    {
        if (_heldUsable == null)
            return;

        Debug.Log("Using " + _heldUsable.name);

        _heldUsable.Use();
        Hold(null);
    }

    private void Hold(Usable usable)
    {
        _heldUsable = usable;

        if (_heldUsable == null)
            return;

        Debug.Log("Captured " + usable.name);
        _heldUsable.SetOwner(this);
    }
}
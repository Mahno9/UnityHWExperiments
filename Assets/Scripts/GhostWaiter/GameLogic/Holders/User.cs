using UnityEngine;
using UnityEngine.Assertions;

public class User : Holder
{
    [SerializeField] private Transform _jointTransform;

    private Usable _heldUsable;

    public override Transform GetJointTransform()
    {
        Assert.IsNotNull(_jointTransform, "_jointTransform is not assigned in the inspector.");
        return _jointTransform;
    }

    public void Use()
    {
        if (_heldUsable == null)
            return;

        _heldUsable.Use();
        Hold(null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_heldUsable != null)
            return;

        SpawnPoint holder = other.GetComponent<SpawnPoint>();
        if (holder == null || holder.IsEmpty)
            return;

        Hold(holder.ExtractUsable());
    }

    private void Hold(Usable usable)
    {
        _heldUsable = usable;

        if (_heldUsable != null)
            _heldUsable.SetHolder(this);
    }
}
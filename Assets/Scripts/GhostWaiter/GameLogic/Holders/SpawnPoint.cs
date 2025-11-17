using UnityEngine;

public class SpawnPoint : Holder
{
    [SerializeField] private Transform _jointTransform;

    private Usable _heldUsable;

    public virtual Usable ExtractUsable()
    {
        Usable _extractedUsable = _heldUsable;
        _heldUsable = null;
        return _extractedUsable;
    }

    public bool InlayUsable(Usable usable)
    {
        if (IsEmpty)
            _heldUsable = usable;
        else
            return false;

        usable.SetHolderInstant(this);

        return true;
    }

    public override Transform GetJointTransform()
    {
        return _jointTransform;
    }

    public bool IsEmpty => _heldUsable == null;
}

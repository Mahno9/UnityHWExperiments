using UnityEngine;

public class SpawnPoint : Holder
{
    [SerializeField] private Transform _jointTransform;

    private Usable _currentUsable;

    public virtual Usable ExtractUsable()
    {
        Usable _extractedUsable = _currentUsable;
        _currentUsable = null;
        return _extractedUsable;
    }

    public bool InlayUsable(Usable usable)
    {
        if (IsEmpty)
            _currentUsable = usable;
        else
            return false;

        usable.SetHolderInstant(this);

        return true;
    }

    public override Transform GetJointTransform()
    {
        return _jointTransform;
    }

    public bool IsEmpty => _currentUsable == null;
}

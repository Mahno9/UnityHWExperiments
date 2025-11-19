using System;

using UnityEngine;

public class SpawnPoint : Holder
{
    [SerializeField] private Transform _jointTransform;
    [SerializeField] private JunkHolder _junkHolder;
    [SerializeField] private float _destroyInterval = 2f;

    private Holdable _curHoldable;
    private SpawnPointState _state = SpawnPointState.Empty;

    public virtual Usable TryExtract()
    {
        Usable extractedUsable = (Usable)_curHoldable;
        _curHoldable = null;

        UpdateState();

        return extractedUsable;
    }

    public bool Put(Holdable holdable)
    {
        bool isReplacing = false;
        if (!CanSpawn() && !(isReplacing = CanReplaceWith(holdable)))
            return false;

        if (_curHoldable)
        {
            Destroy(_curHoldable.gameObject);
            _curHoldable = null;
        }

        if (isReplacing)
        {
            holdable.SetHolder(_junkHolder);
            Destroy(holdable.gameObject, _destroyInterval);
        }
        else
        {
            _curHoldable = holdable;
            holdable.SetHolderInstant(this);
        }

        UpdateState();

        return true;
    }

    private void UpdateState()
    {
        if (_curHoldable is null)
        {
            _state = SpawnPointState.Empty;
            return;
        }

        _state = (_curHoldable as Replaceable is not null) ? SpawnPointState.NeedReplace : SpawnPointState.Possessed;
    }

    public override Transform GetJointTransform()
    {
        return _jointTransform;
    }

    private string RequiredTag => (_curHoldable as Replaceable)?.RequiredTag;

    public bool CanSpawn() => _state is SpawnPointState.Empty;

    public bool CanReplaceWith(Holdable holdable)
    {
        Replacing other = holdable as Replacing;
        return other is not null
               && _state == SpawnPointState.NeedReplace
               && other.Tag != string.Empty
               && other.Tag == RequiredTag;
    }

    public bool NeedTakeAway() => _state == SpawnPointState.Possessed;
}
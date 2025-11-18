using System;

using UnityEngine;

public class SpawnPoint : Holder
{
    [SerializeField] private Transform _jointTransform;

    private Holdable _curHoldable;
    private SpawnPointState _state = SpawnPointState.Empty;

    public virtual Usable TryExtract()
    {
        Usable extractedUsable = (Usable)_curHoldable;
        _curHoldable = null;

        UpdateState(false);

        return extractedUsable;
    }

    public bool Inlay(Holdable holdable)
    {
        bool canReplace = false;
        if (!CanSpawn() && !(canReplace = CanReplaceWith(holdable)))
            return false;

        if (_curHoldable)
            Destroy(_curHoldable.gameObject);

        _curHoldable = holdable;
        holdable.SetHolderInstant(this);

        UpdateState(canReplace);

        return true;
    }

    private void UpdateState(bool wasReplaced)
    {
        if (_curHoldable is null)
        {
            _state = SpawnPointState.Empty;
            return;
        }

        if (wasReplaced)
        {
            _state = SpawnPointState.ReplaceDone;
            return;
        }

        _state = (_curHoldable as Replaceable is not null) ? SpawnPointState.NeedReplace : SpawnPointState.Possessed;
    }

    public override Transform GetJointTransform()
    {
        return _jointTransform;
    }

    private string RequiredTag => (_curHoldable as Replaceable)?.RequiredTag;

    public bool CanSpawn() => _state is SpawnPointState.Empty or SpawnPointState.ReplaceDone;

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
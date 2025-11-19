using System;

using Unity.VisualScripting;

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
        if (_heldUsable is null)
            return;

        _heldUsable.Use();
        Hold(null);
    }

    private void OnTriggerEnter(Collider other)
    {
        SpawnPoint spawnPoint = other.GetComponent<SpawnPoint>();
        if (spawnPoint == null)
            return;

        bool holdSome = _heldUsable != null;

        if (holdSome)
            ReplaceWithHeld(spawnPoint);
        else if (spawnPoint.NeedTakeAway())
            Hold(spawnPoint.TryExtract());
    }

    private void ReplaceWithHeld(SpawnPoint spawnPoint)
    {
        if (spawnPoint.CanReplaceWith(_heldUsable))
        {
            spawnPoint.Put(_heldUsable);
            Hold(null);
        }
    }

    private void Hold(Usable usable)
    {
        _heldUsable = usable;

        if (_heldUsable is null)
            return;

        _heldUsable.SetHolder(this);
        DisableHighlight(_heldUsable);
    }

    private void DisableHighlight(Usable heldUsable)
    {
        heldUsable.GetComponentInChildren<HighlightItem>()?.DisableHighlight();
    }
}
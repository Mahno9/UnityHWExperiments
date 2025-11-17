using UnityEngine;
using UnityEngine.Assertions;

public class InfiniteSelfSpawnPoint : SpawnPoint
{
    [SerializeField] private Usable _usablePrefab;

    private void Awake()
    {
        SpawnUsable();
    }

    public override Usable ExtractUsable()
    {
        Usable oldUsable = base.ExtractUsable();
        SpawnUsable();
        return oldUsable;
    }

    private void SpawnUsable()
    {
        Assert.IsNotNull(_usablePrefab, $"{nameof(_usablePrefab)} is not assigned in the inspector of {nameof(InfiniteSelfSpawnPoint)} attached to {gameObject.name}.");
        Assert.IsTrue(IsEmpty, $"Trying to spawn usable in non-empty {nameof(InfiniteSelfSpawnPoint)} attached to {gameObject.name}.");
        InlayUsable(Instantiate(_usablePrefab));
    }
}

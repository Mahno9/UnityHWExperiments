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
        Assert.IsNotNull(_usablePrefab);
        Assert.IsTrue(IsEmpty);
        InlayUsable(Instantiate(_usablePrefab));
    }
}

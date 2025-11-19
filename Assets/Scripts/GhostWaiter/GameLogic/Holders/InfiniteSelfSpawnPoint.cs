using UnityEngine;
using UnityEngine.Assertions;

public class InfiniteSelfSpawnPoint : SpawnPoint
{
    [SerializeField] private Usable _usablePrefab;

    private void Awake()
    {
        Spawn();
    }

    public override Usable TryExtract()
    {
        Usable oldUsable = base.TryExtract();
        Spawn();
        return oldUsable;
    }

    private void Spawn()
    {
        Assert.IsNotNull(_usablePrefab);
        Assert.IsTrue(CanSpawn());
        Put(Instantiate(_usablePrefab));
    }
}

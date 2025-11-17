using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Holder[] _spawnHolders;
    [SerializeField] private Usable[] _spawnItems;
    [SerializeField] private float _spawnInterval = 2f;

    private float _spawnRemainingTime;

    private void Start()
    {
        Assert.IsTrue(_spawnHolders.Length > 0, "_spawnHolders is empty in Spawner.");
        Assert.IsTrue(_spawnItems.Length > 0, "_spawnItems is empty in Spawner.");
    }

    private void Update()
    {
        if (_spawnRemainingTime <= 0)
        {
            _spawnRemainingTime = _spawnInterval;
            SpawnItem();
        }

        _spawnRemainingTime -= Time.deltaTime;
    }

    private void SpawnItem()
    {
        Holder emptyHolder = FindEmptyHolder();
        if (emptyHolder == null)
            return;

        Usable itemPrefab = GetRandomSpawnItem();
        Usable spawnedItem = Instantiate(itemPrefab);
        // Skip Init since spawned items in Spawner have no owner.

        emptyHolder.InlayUsable(spawnedItem);
    }

    private Holder FindEmptyHolder()
    {
        List<Holder> emptyHolders = new List<Holder>();
        foreach (Holder holder in _spawnHolders)
        {
            if (holder.IsEmpty)
                emptyHolders.Add(holder);
        }

        if (emptyHolders.Count == 0)
            return null;

        return emptyHolders[UnityEngine.Random.Range(0, emptyHolders.Count)];
    }

    private Usable GetRandomSpawnItem()
    {
        return _spawnItems[UnityEngine.Random.Range(0, _spawnItems.Length)];
    }

}

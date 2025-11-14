using System.Collections.Generic;
using System.Linq;

using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Collector : MonoBehaviour
{
    private HashSet<Collectable> _knownCollectables;

    private void Awake()
    {
        _knownCollectables = new();
        Collectable[] collectables = FindObjectsOfType<Collectable>();
        foreach (Collectable obj in collectables)
            _knownCollectables.Add(obj);

        Debug.Log("Collectables: " + string.Join("\n", collectables.Select((c, i) => $"{i}: {c.name}").ToArray()));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Collectable>(out Collectable collectable))
        {
            collectable.OnCollect();
            _knownCollectables.Remove(collectable);
            Debug.Log("Collecting: " + other.name);
        }
        else
        {
            Debug.LogWarning("Collector collided with non-collectable object: " + other.name);
        }
    }

    public Collectable[] GetUncollected()
    {
        return _knownCollectables.ToArray();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Collector : MonoBehaviour
{
    SortedSet<Collectable> _knownCollectables;

    private void Awake()
    {
        _knownCollectables = new ();
        Collectable[] collectables = FindObjectsOfType<Collectable>();
        foreach (Collectable obj in collectables)
            _knownCollectables.Add(obj);
    }

    private void OnTriggerEnter(Collider other)
    {
        Collectable collectable = other.GetComponent<Collectable>();
        if (collectable != null)
        {
            collectable.OnCollect();
            _knownCollectables.Remove(collectable);
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

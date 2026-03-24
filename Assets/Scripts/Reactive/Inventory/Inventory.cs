using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Assertions;

public class Inventory
{
    public int CurrentSize => _items.Sum(item => item.Value);

    public int MaxSize { get; private set; }

    public IReadOnlyDictionary<Item, int> Items => _items;

    private readonly Dictionary<Item, int> _items = new();

    public Inventory(List<Item> items, int maxSize)
    {
        MaxSize = maxSize;
        if (items.Any(item => TryAdd(item) == false))
            Debug.LogWarning($"Only the first {MaxSize} items are placed in inventory from the {items.Count} given items.");
    }

    public bool TryAdd(Item item)
    {
        if (CurrentSize + 1 > MaxSize)
            return false;

        _items[item]++;

        return true;
    }

    public List<Item> GetItemsBy(string name, int count)
    {
        KeyValuePair<Item, int> itemCount = _items.FirstOrDefault(kvItem => kvItem.Key.Name == name);

        if (itemCount.Value == 0)
            return null;

        int        canGetAmount = Mathf.Min(count, itemCount.Value);
        List<Item> foundItems   = new(canGetAmount);
        for (int i = 0; i < canGetAmount; i++)
            foundItems.Add(itemCount.Key);

        _items[itemCount.Key] -= canGetAmount;

        return foundItems;
    }
}

public class Item
{
    public string Name;
}
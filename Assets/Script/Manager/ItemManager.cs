using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item[] items;

    private Dictionary<string, Item> nameItemsDict
        = new Dictionary<string, Item>();

    private void Awake()
    {
        foreach(Item item in items)
        {
            AddItem(item);
        }
    }

    private void AddItem(Item item)
    {
        if (!nameItemsDict.ContainsKey(item.data.itemName))
        {
            nameItemsDict.Add(item.data.itemName, item);
        }
    }

    public Item GetItemByName(string key)
    {
        if (nameItemsDict.ContainsKey(key))
        {
            return nameItemsDict[key];
        }

        return null;
    }
}

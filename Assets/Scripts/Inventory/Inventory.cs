using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();

        // testing list will delete soon
        AddItem(new Item { itemType = Item.ItemType.Berries, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Wood, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Axe, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Fish, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Water, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Flower, amount = 1 });
        Debug.Log("Inventory Count: " + itemList.Count);
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}

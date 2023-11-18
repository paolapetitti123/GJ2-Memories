using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    private List<Item> itemList;

    public event EventHandler OnItemListChanged;

    private Action<Item> useItemAction;

    public Inventory(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();

        /* testing list will delete soon*/
        AddItem(new Item { itemType = Item.ItemType.Berries, amount = 1 });/*
        AddItem(new Item { itemType = Item.ItemType.Mushroom, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Axe, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Fish, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Water, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Flower, amount = 1 });
        */
        Debug.Log("Inventory Count: " + itemList.Count);
    }

    public void AddItem(Item item)
    {
        if (item.isStackable())
        {
            bool itemInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if(inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemInInventory = true;
                }
            }
            if (!itemInInventory)
            {
                itemList.Add(item); 
            }
        }
        else
        {
            itemList.Add(item);
        }

        
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public void RemoveItem(Item item)
    {
        if (item.isStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemList.Remove(itemInInventory);
            }
        }
        else
        {
            itemList.Remove(item);
        }


        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UseItem(Item item)
    {
        useItemAction(item);
    }

}

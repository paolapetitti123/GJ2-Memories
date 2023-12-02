using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    public List<Item> itemList;

    public event EventHandler OnItemListChanged;

    private Action<Item> useItemAction;

    public Inventory(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();

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

    public bool GetItem(Item item)
    {
        if (itemList.Contains(item))
        {
            return true;
        }
        else
        {
            return false;
        }
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

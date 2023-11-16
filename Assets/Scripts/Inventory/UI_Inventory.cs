using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inv;
    private Transform itemSlotParent;
    private Transform itemSlotContainer;

    private GameObject[] itemSlot;

    private void Awake()
    {
        itemSlotParent = transform.Find("ItemsParent");
        itemSlotContainer = transform.Find("InventorySlot");


        itemSlot = GameObject.FindGameObjectsWithTag("itemImage");
        foreach(GameObject item in itemSlot)
        {
            item.SetActive(false);
        }
    }

    public void SetInventory(Inventory inventory)
    {
        this.inv = inventory;
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        int counter = 0;
        foreach (Item item in inv.GetItemList())
        {
            itemSlot[counter].SetActive(true);
            counter++;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;

    private GameObject[] itemSlot;

    private void Awake()
    {
        itemSlot = GameObject.FindGameObjectsWithTag("itemImage");
        foreach(GameObject item in itemSlot)
        {
            item.SetActive(false);
        }
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        int counter = 0;
        foreach (Item item in inventory.GetItemList())
        {
            itemSlot[counter].SetActive(true);
            Image img = itemSlot[counter].GetComponent<Image>();
            img.sprite = item.GetSprite();
            TextMeshProUGUI uiText = itemSlot[counter].GetComponentInChildren<TextMeshProUGUI>();
            if(item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }
            
            counter++;
        }
    }

}

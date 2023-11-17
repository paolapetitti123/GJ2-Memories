using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;
using System;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;

    private GameObject[] itemSlot;

    private GameObject[] removeButton;
    private GameObject[] useButton;
    private PlayerMovement player;


    private void Awake()
    {
        itemSlot = GameObject.FindGameObjectsWithTag("itemImage");
        foreach(GameObject item in itemSlot)
        {
            item.SetActive(false);
        }

        removeButton = GameObject.FindGameObjectsWithTag("removeButton");
        foreach (GameObject item in removeButton)
        {
            item.SetActive(false);
        }

        useButton = GameObject.FindGameObjectsWithTag("useButton");
        foreach (GameObject item in useButton)
        {
            item.SetActive(false);
        }
    }

    public void SetPlayer(PlayerMovement player)
    {
        this.player = player;
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
            Debug.Log("Item Inventory Count: " + inventory.GetItemList().Count);
            Debug.Log("refresh inv counter: " + counter);
            itemSlot[counter].SetActive(true);
            Image img = itemSlot[counter].GetComponent<Image>();
            img.sprite = item.GetSprite();
            


            itemSlot[counter].GetComponent<Button_UI>().ClickFunc = () =>
            {
               
                counter = inventory.GetItemList().Count;
                Debug.Log("refresh inv counter: " + counter);

                /*
                if (inventory.GetItemList().Count == 0 )
                {
                    useButton[counter].SetActive(true);
                    removeButton[counter].SetActive(true);
                }
                else if (inventory.GetItemList().Count == 1)
                {
                    useButton[counter - 1].SetActive(true);
                    removeButton[counter - 1].SetActive(true);
                }
                else if (inventory.GetItemList().Count == 2)
                {
                    useButton[counter - 2].SetActive(true);
                    removeButton[counter - 2].SetActive(true);
                }
                */
                useButton[counter-1].SetActive(true);
                removeButton[counter-1].SetActive(true);
            };

            removeButton[counter].GetComponent<Button_UI>().ClickFunc = () =>
            {
                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                inventory.RemoveItem(item);
                itemSlot[counter-1].SetActive(false);
                ItemWorld.DropItem(player.transform.position, duplicateItem);
                useButton[counter-1].SetActive(false);
                removeButton[counter-1].SetActive(false);
            };



            //itemSlot[counter].GetComponent<Button>().onClick.AddListener(() => UseItem(counter -1, item));


            TextMeshProUGUI uiText = itemSlot[counter].GetComponentInChildren<TextMeshProUGUI>();
            if(item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }
            
           counter = counter + 1;


            
        }
    }


    public void UseItem(int counter, Item item)
    {
        Debug.Log(counter);
        Debug.Log("USE ITEM");
        
        useButton[counter].SetActive(true);
        removeButton[counter].SetActive(true);

        removeButton[counter].GetComponent<Button>().onClick.AddListener(() =>
        {
            inventory.RemoveItem(item);
            itemSlot[counter].SetActive(false);
            ItemWorld.DropItem(player.transform.position, item);
        }
        );

    }


}

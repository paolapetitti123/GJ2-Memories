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
        foreach (GameObject item in itemSlot)
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
        // gets the current player item that contains info such as the current position 
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
        int guiCounter = 0;
        foreach (Item item in inventory.GetItemList())
        {
            int counter = inventory.GetItemList().IndexOf(item);


            itemSlot[counter].SetActive(true);

            // assigning the correct image to the item slot
            Image img = itemSlot[counter].GetComponent<Image>();
            img.sprite = item.GetSprite();


            itemSlot[counter].GetComponent<Button_UI>().ClickFunc = () =>
            {
                // makes use and drop buttons visible

                if (item.isUseable())
                {
                    useButton[counter].SetActive(true);
                    removeButton[counter].SetActive(true);
                }
                else if (!item.isUseable())
                {
                    removeButton[counter].SetActive(true);
                }



            };

            // deletes/drops the item back into the scene. 
            removeButton[counter].GetComponent<Button_UI>().ClickFunc = () =>
            {
                /* First create a temporary duplicate of the active item in the inventory (the one that
                 * currently has the buttons active on it)so that we can use the duplicate to drop into 
                 * the scene after we delete the item from our List. 
                 * 
                 * We can't drop the item itself because then if the item was stackable, when we drop the item,
                 * the last item in the stack would be deleted so the total # in the item stack would be wrong
                 * hence the need to make a copy of the item before removing it from the inventory.
                 */
                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                inventory.RemoveItem(item);
                itemSlot[counter].SetActive(false);
                ItemWorld.DropItem(player.transform.position, duplicateItem);


                // setting the action buttons back to false 
                useButton[counter].SetActive(false);
                removeButton[counter].SetActive(false);
            };


            useButton[counter].GetComponent<Button_UI>().ClickFunc = () =>
            {
                inventory.UseItem(item);
                inventory.RemoveItem(item);
                itemSlot[counter].SetActive(false);
                useButton[counter].SetActive(false);
                removeButton[counter].SetActive(false);
            };

            /* This code is thanks to the CodeMonkey Utils package I downloaded, it makes detecting
             * right vs left clicks a lot easier as well as adding the ability for implicit function 
             * declarations. 
             * 
             * Note you can do that with Unity's default button package however it is 
             * slightly different and does not work the exact same way this does.
             */


            /* This allows for the text associated to the stackable inventory item to update  
             * whenever a new item gets added to that item stack. 
             */
            TextMeshProUGUI uiText = itemSlot[guiCounter].GetComponentInChildren<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }

            guiCounter++;
        }
    

    }
}

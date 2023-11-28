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

    public GameObject[] itemSlot;

    private GameObject[] removeButton;
    private GameObject[] useButton;
    private PlayerMovement player;
    public Quest quest;

    public bool questComplete;
    private Conversation convo;



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


    private void Update()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            int index = i;
            RefreshInventoryItems(index);
           

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
        RefreshInventoryItems(0);
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems(0);
    }

    private void RefreshInventoryItems(int index)
    {

        int guiCounter = 0;
        //int counterTest = 0;
        int counter = 0;
        

        foreach (Item item in inventory.GetItemList())
        {

            itemSlot[counter].SetActive(true);
            // assigning the correct image to the item slot
            Image img = itemSlot[counter].GetComponent<Image>();
            img.sprite = item.GetSprite();
            img.enabled = true;


            itemSlot[index].GetComponent<Button_UI>().ClickFunc = () => {

                if (item.isUseable())
                {
                    useButton[index].SetActive(true);
                    removeButton[index].SetActive(true);
                    StartCoroutine(InventorySlotTimer(index));

                }
                else if (!item.isUseable())
                {
                    removeButton[index].SetActive(true);
                    StartCoroutine(InventorySlotTimer(index));

                }
            };
            

            /* First create a temporary duplicate of the active item in the inventory (the one that
              * currently has the buttons active on it)so that we can use the duplicate to drop into 
              * the scene after we delete the item from our List. 
              * 
              * We can't drop the item itself because then if the item was stackable, when we drop the item,
              * the last item in the stack would be deleted so the total # in the item stack would be wrong
              * hence the need to make a copy of the item before removing it from the inventory.
              */

            removeButton[index].GetComponent<Button_UI>().ClickFunc = () =>
            {
                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };

                inventory.RemoveItem(item);

                ItemWorld.DropItem(player.transform.position, duplicateItem);
                Image img = itemSlot[index].GetComponent<Image>();
                img.enabled = false;
                //itemSlot[index].SetActive(false);

                // setting the action buttons back to false 
                useButton[index].SetActive(false);
                removeButton[index].SetActive(false);

            };

            
                useButton[index].GetComponent<Button_UI>().ClickFunc = () => {

                    if (item.IsPotionOne())
                    {
                        Debug.Log("Potion Clicked");
                        inventory.UseItem(item);
                        inventory.RemoveItem(item);
                        itemSlot[index].SetActive(false);
                        useButton[index].SetActive(false);
                        removeButton[index].SetActive(false);
                        StartCoroutine(PotionDrink());
                    }
                    else
                    {
                        inventory.UseItem(item);
                        inventory.RemoveItem(item);
                        itemSlot[index].SetActive(false);
                        useButton[index].SetActive(false);
                        removeButton[index].SetActive(false);
                    }
                    

                    
                };
            
       

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

            if (questComplete)
            {
                int count = 0;
                while(count < 9)
                {
                    if (itemSlot[count].activeInHierarchy && itemSlot[count] != null)
                    {
                        
                        itemSlot[count].SetActive(false);
                        count++;
                       
                    }
                    else
                        break;
                }
                //inventory = null;
                inventory.AddItem(new Item { itemType = Item.ItemType.Potion1, amount = 1 });

                Debug.Log("in UI Inventory has spoken");
               


            }


            counter++;
        }
    

    }

    private IEnumerator InventorySlotTimer(int counterTest)
    {
        yield return new WaitForSeconds(3f);
  
            useButton[counterTest].SetActive(false);
            removeButton[counterTest].SetActive(false);
    }


    private IEnumerator PotionDrink()
    {
        yield return new WaitForSeconds(1f);

        GameStateManager.Instance.LoadScene(GameStateManager.Scene.GameWin);
    }
}

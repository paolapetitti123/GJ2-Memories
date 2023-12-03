using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;
using System;
using System.Linq;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;

    public GameObject[] itemSlot;

    
    public GameObject[] useButton;
    private PlayerMovement player;
    public Quest quest;

    public bool questComplete;
    private Conversation convo;


    public GameObject check1;
    public GameObject check2;
    public GameObject check3;
    public GameObject check4;
    public GameObject check5;
    public GameObject check6;
    public GameObject check7;


    public GameObject axe;
    public GameObject fishingRod;

    public GameObject InventoryWarningMessage;
    private GameObject InventoryWarningButton;


    // Start is called before the first frame update
    void Start()
    {
        check1.SetActive(false);
        check2.SetActive(false);
        check3.SetActive(false);
        check4.SetActive(false);
        check5.SetActive(false);
        check6.SetActive(false);
        check7.SetActive(false);
        InventoryWarningMessage.SetActive(false);
        
        /*
        itemSlot = GameObject.FindGameObjectsWithTag("itemImage");
        foreach (GameObject item in itemSlot)
        {
            item.SetActive(false);
            
        }*/



        //useButton = GameObject.FindGameObjectsWithTag("useButton");
        foreach (GameObject item in useButton)
        {
            item.SetActive(false);
        }

       

    }


    private void Awake()
    {

        check1 = GameObject.FindWithTag("check1");
        check2 = GameObject.FindWithTag("check2");
        check3 = GameObject.FindWithTag("check3");
        check4 = GameObject.FindWithTag("check4");
        check5 = GameObject.FindWithTag("check5");
        check6 = GameObject.FindWithTag("check6");
        check7 = GameObject.FindWithTag("check7");


        
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

   
           


            // Compare itemType with the enum value
            if (img.sprite.name  == "Mushroom" && img.enabled == true)
            {
                check1.SetActive(true);
                //Debug.Log(item.itemType + "checked off");
            }
            // Compare itemType with the enum value
            if (img.sprite.name == "Herb" && img.enabled == true)
            {
                check2.SetActive(true);
               // Debug.Log(item.itemType + "checked off");
            }
            // Compare itemType with the enum value
            if (img.sprite.name == "Flower" && img.enabled == true)
            {
                check3.SetActive(true);
               // Debug.Log(item.itemType + "checked off");
            }
            // Compare itemType with the enum value
            if (img.sprite.name == "Wood" && img.enabled == true)
            {
                check4.SetActive(true);
                //Debug.Log(item.itemType + "checked off");
            }
            // Compare itemType with the enum value
            if (img.sprite.name == "Berries" && img.enabled == true)
            {
                check5.SetActive(true);
                //Debug.Log(item.itemType + "checked off");
            }
            // Compare itemType with the enum value
            if (img.sprite.name == "Fish" && img.enabled == true)
            {
                check6.SetActive(true);
               // Debug.Log(item.itemType + "checked off");
            }
            // Compare itemType with the enum value
            if (img.sprite.name == "Water" && img.enabled == true)
            {
                check7.SetActive(true);
                //Debug.Log(item.itemType + "checked off");
            }

            itemSlot[index].GetComponent<Button_UI>().ClickFunc = () => {

                Item currItem = inventory.GetItemList()[index];
                if (currItem.isUseable())
                {
                    useButton[index].SetActive(true);
                    StartCoroutine(InventorySlotTimer(index));

                }
                else if (!currItem.isUseable())
                {
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
              /*
            removeButton[index].GetComponent<Button_UI>().ClickFunc = () =>
            {
                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                Item currItem = inventory.GetItemList()[index];

                inventory.RemoveItem(currItem);

                ItemWorld.DropItem(player.transform.position, duplicateItem);
                Image img = itemSlot[index].GetComponent<Image>();
                img.enabled = false;
                itemSlot[index].SetActive(false);

                // setting the action buttons back to false 
                useButton[index].SetActive(false);

            };
            */ 


            useButton[index].GetComponent<Button_UI>().ClickFunc = () => {
                Debug.Log("use button index: " + index);
                Item currItem = inventory.GetItemList()[index];
                Debug.Log("Inventory Size: " + (inventory.GetItemList().Count - 1));

                if (currItem.IsPotionOne())
                {
                    Debug.Log("Potion Clicked");


                    inventory.UseItem(currItem);
                    inventory.RemoveItem(currItem);
                    itemSlot[index].SetActive(false);
                    useButton[index].SetActive(false);
                    StartCoroutine(PotionDrink());

                   
                }
                if (!axe.activeInHierarchy && !fishingRod.activeInHierarchy)
                {
                    if (currItem.IsAnAxe())
                    {
                        axe.SetActive(true);
                        inventory.RemoveItem(currItem);
                        itemSlot[index].SetActive(false);
                        itemSlot[inventory.GetItemList().Count ].SetActive(false);
                        useButton[index].SetActive(false);


                        // Trigger item audio
                        AudioController.Instance.PlaySoundGameplay("axe-1");
                    }
                    else if (currItem.IsAFishingRod())
                    {

                        fishingRod.SetActive(true);
                        inventory.RemoveItem(currItem);
                        itemSlot[index].SetActive(false);
                        itemSlot[inventory.GetItemList().Count ].SetActive(false);
                        useButton[index].SetActive(false);

                        // Trigger item audio
                        AudioController.Instance.PlaySoundGameplay("fishing-rod-1");
                    }
                }
                else if (axe.activeInHierarchy && !fishingRod.activeInHierarchy || !axe.activeInHierarchy && fishingRod.activeInHierarchy)
                {
                    InventoryWarningMessage.SetActive(true);
                    InventoryWarningButton = GameObject.FindGameObjectWithTag("InventoryFullMessage");

                    InventoryWarningButton.GetComponent<Button_UI>().ClickFunc = () =>
                    {
                        InventoryWarningMessage.SetActive(false);
                    };


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
    }


    private IEnumerator PotionDrink()
    {
        yield return new WaitForSeconds(1f);

        GameStateManager.Instance.LoadScene(GameStateManager.Scene.GameWin);
    }
}

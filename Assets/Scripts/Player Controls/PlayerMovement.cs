using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using CodeMonkey.Utils;

/* Followed this tutorial for the movement if any of you are interested in the detailed 
 * explanation: https://www.youtube.com/watch?v=whzomFgjT50 
 */
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    private Inventory inventory;

    [SerializeField] private UI_Inventory uiInv;

    // For potion use flash
    [ColorUsage(true,true)]
    [SerializeField] private Color FlashRedColor;
    [ColorUsage(true, true)]
    [SerializeField] private Color FlashOrangeColor;
    [ColorUsage(true, true)]
    [SerializeField] private Color FlashGreenColor;
    [SerializeField] private float _flashTime;
    public SpriteRenderer spriteRend;
    public Material characterMat;

    public GameObject InventoryWarningMessage;
    private GameObject InventoryWarningButton;

    private Item item;

    //public Conversation convo;
    public Quest quest;

    public GameObject Conversation1;


    public int mushroomCounter = 0;
    public int flowerCounter = 0;
    public int herbCounter = 0;
    public int berryCounter = 0;
    public int woodCounter = 0;
    public int fishCounter = 0;
    public int waterCounter = 0;

    public bool axeCollected = false;
    public bool fishingRodCollected = false;

    public GameObject axe;
    public GameObject fishingRod;
    public GameObject treeStump;

    public bool convoActive;
    public GameObject Alchemist;
    public GameObject[] itemSlot;


    void Start()
    {
        inventory = new Inventory(UseItem);
        uiInv.SetInventory(inventory);
        uiInv.SetPlayer(this);
        
        convoActive = false;

        itemSlot = GameObject.FindGameObjectsWithTag("itemImage");
        foreach (GameObject item in itemSlot)
        {
            item.SetActive(false);

        }

        treeStump.SetActive(false);
    }

    /* Update is called once per frame so we need to check when the player hits certain keys
     * the movement for those keys will be handled in FixedUpdate
     */
    void Update()
    {
      

        convoActive = Conversation1.activeInHierarchy;
        // gives value between -1 and 1 depending on horizontal input, i.e pressing the Left arrow key give -1 and Right gives 1
        // (WASD + arrow keys work)
        movement.x = Input.GetAxisRaw("Horizontal");

        // gives value between -1 and 1 depending on vertical input (WASD + arrow keys work)
        movement.y = Input.GetAxisRaw("Vertical");


        /* I created seperate animations for walking up, down, left, right, I use a blend tree called Movement in the 
         * Main Character Animator that essentially puts them all together, this way when the character sprite sheet is ready 
         * all I have to do is modify the animation to use the sprites instead of color changes.
         * 
         * These 3 lines allow for the character to move around the screen
         */
        

        if (!InventoryWarningMessage.activeInHierarchy)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        else if (InventoryWarningMessage.activeInHierarchy)
        {
            movement.x = 0;
            movement.y = 0;
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
       
    }

    /* Need to use Fixed Update for movement since the framerate of the game can change
     * per computer making manipulating the physics of the character unpredictable. Fixed
     * Update allows for a set fixed frame rate making it much more manageable.
     */
    void FixedUpdate()
    {
        /* To make sure the movement speed will always stay the same "Time.fixedDeltaTime" is super important as
         * no matter how many times the fixed update function is called, fixedDeltaTime is the amount of time that has
         * elapsed since the last time the function was called and the result of that is a constant movement speed.
         */
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
       
        if(itemWorld != null)
        {
            if(inventory.GetItemList().Count < 10)
            {
                if (quest.isActive)
                {
                    // Trigger pick up item audio
                    // AudioController.Instance.PlaySoundGameplayNoRepeat("pickup_item_1");

                    itemWorld.enabled = true;
                    if (itemWorld.GetItem().isParent())
                    {

                        if (itemWorld.GetItem().GetSprite().name == "well-1")
                        {
                            inventory.AddItem(new Item { itemType = Item.ItemType.Water, amount = 1 });

                             // Trigger pick up item audio
                            AudioController.Instance.PlaySoundGameplayNoRepeat("pickup_well_2");

                            if (waterCounter == 0)  // doing this so the ingredientsgathered is only ever called once
                            {
                                quest.goal.IngredientGathered();
                                waterCounter++;
                            }

                        }
                        else if (itemWorld.GetItem().GetSprite().name == "flowers-3")
                        {

                            inventory.AddItem(new Item { itemType = Item.ItemType.Flower, amount = 1 });

                             // Trigger pick up item audio
                            AudioController.Instance.PlaySoundGameplayNoRepeat("pickup_flowers_1");

                            if (flowerCounter == 0) // doing this so the ingredientsgathered is only ever called once
                            {
                                quest.goal.IngredientGathered();
                                flowerCounter++;
                                Debug.Log("f count = " + flowerCounter);
                            }
                            
                        }
                        else if (itemWorld.GetItem().GetSprite().name == "herb-plant")
                        {
                            inventory.AddItem(new Item { itemType = Item.ItemType.Herb, amount = 1 });
                            // Trigger pick up item audio
                            AudioController.Instance.PlaySoundGameplayNoRepeat("pickup_herb_2");

                            if(herbCounter == 0)  // doing this so the ingredientsgathered is only ever called once
                            {
                                quest.goal.IngredientGathered();
                                herbCounter++;
                            }
                            
                        }
                        else if (itemWorld.GetItem().GetSprite().name == "berry-bush")
                        {
                            // Trigger pick up item audio
                            AudioController.Instance.PlaySoundGameplayNoRepeat("pickup_berry_2");
                            inventory.AddItem(new Item { itemType = Item.ItemType.Berries, amount = 1 });
                            if (berryCounter == 0)  // doing this so the ingredientsgathered is only ever called once
                            {
                                quest.goal.IngredientGathered();
                                berryCounter++;
                            }

                        }
                        // Tree
                        else if (itemWorld.GetItem().GetSprite().name == "special-tree")
                        {
                            // Trigger pick up item audio
                            //AudioController.Instance.PlaySoundGameplayNoRepeat("pickup_berry_2");

                            // First check if axe is in inventory + active 
                            if (axe.activeInHierarchy)
                            {
                                
                                if (woodCounter == 0)  // doing this so the ingredientsgathered is only ever called once
                                {
                                    StartCoroutine(ChopWood(itemWorld));
                                /*
                                 *      ADD SOUND HERE!
                                 */
                                }
                            }

                            // if it's not active but in inventory
                            else if(!axe.activeInHierarchy && axeCollected)
                            {
                                // do other thing
                                Debug.Log("USE THE AXE");
                            }
                            
                            // if its not in inventory
                            else if (!axeCollected)
                            {
                                // do last thing
                                Debug.Log("GET THE AXE");
                            }

                        }

                        // Fish
                        else if (itemWorld.GetItem().GetSprite().name == "fish-poster")
                        {
                            // Trigger pick up item audio
                            //AudioController.Instance.PlaySoundGameplayNoRepeat("pickup_berry_2");

                            // First check if axe is in inventory + active 
                            if (fishingRod.activeInHierarchy)
                            {
                                
                                if (fishCounter == 0)  // doing this so the ingredientsgathered is only ever called once
                                {
                                    StartCoroutine(Fishing());
                                 /*
                                 *      ADD SOUND HERE!
                                 */
                                }
                            }

                            // if it's not active but in inventory
                            else if (!fishingRod.activeInHierarchy && fishingRodCollected)
                            {
                                // do other thing
                                Debug.Log("USE THE FISHING ROD");
                            }

                            // if its not in inventory
                            else if (!fishingRodCollected)
                            {
                                // do last thing
                                Debug.Log("GET THE FISHING ROD");
                            }

                        }

                    }
                    else if (!itemWorld.GetItem().isParent())
                    {                      
                        if (itemWorld.GetItem().IsAMushroom() && mushroomCounter == 0) // doing this so the ingredientsgathered is only ever called once
                        {                          
                            inventory.AddItem(itemWorld.GetItem());
                            quest.goal.IngredientGathered();
                            mushroomCounter++;
                            itemWorld.DestroySelf();
                        }
                        else if (itemWorld.GetItem().IsAnAxe())
                        {
                            inventory.AddItem(itemWorld.GetItem());
                            axeCollected = true;
                            itemWorld.DestroySelf();
                        }
                        else if (itemWorld.GetItem().IsAFishingRod())
                        {
                            inventory.AddItem(itemWorld.GetItem());
                            fishingRodCollected = true;
                            itemWorld.DestroySelf();
                        }
                        else
                        {
                            inventory.AddItem(itemWorld.GetItem());
                            itemWorld.DestroySelf();
                        }
                        
                    }

                    
                   
                }
                else
                {
                    itemWorld.enabled = false;
                    Debug.Log("Quest not active");
                }
                

                
            }
            else
            {
                Debug.Log("inventory full");
            }

            
        }

        if (quest.goal.IsReached())
        {
            gameObject.GetComponent<Conversation>().questFinished = true;

            Debug.Log("in Player Movement quest one complete goal count 0");
  
            // remove mushroom, herb and flower
            if (collision.gameObject == Alchemist)
            {
                Debug.Log("hitting alchemist");
                quest.goal.TalkingCounter();
                
                if (quest.goal.HasSpoken())
                {

                    Debug.Log("I HAVE MADE IT IN HERE WOOO " );

                    int count = 1;
                    foreach (Item item in inventory.itemList)
                    {
                        // Access the 'GetSprite().name' property of each item's sprite and print it
                        Debug.Log(count + item.GetSprite().name);

                        itemSlot = GameObject.FindGameObjectsWithTag("itemImage");
                        foreach (GameObject specificItem in itemSlot)
                        {
                            if (item.IsAMushroom() || item.IsAFlower() || item.IsAnHerb())
                            {
                                specificItem.SetActive(false);
                                
                                Debug.Log(count + item.GetSprite().name + "setting to false");
                            }
                            

                        }

                        count++;
                    }
                    inventory.itemList.Clear();
                    inventory.AddItem(new Item { itemType = Item.ItemType.Potion3, amount = 1 });
                }
               
            }
        }

        
    }


    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Potion1:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Potion3, amount = 1 });
                StartCoroutine(FlashColor(FlashOrangeColor));
                // Trigger pick up item audio
                AudioController.Instance.PlaySoundGameplayNoRepeat("potion_drink_1");
                break;
            case Item.ItemType.Potion2:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Potion2, amount = 1 });
                StartCoroutine(FlashColor(FlashGreenColor));
                break;
            case Item.ItemType.Potion3:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Potion3, amount = 1 });
                StartCoroutine(FlashColor(FlashRedColor));
                break;
            case Item.ItemType.Axe:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Axe, amount = 1 });
                break;
            case Item.ItemType.FishingRod:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.FishingRod, amount = 1 });
                break; 
        }

    }

    private IEnumerator FlashColor(Color color)
    {
        // set the color
        characterMat.SetColor("_FlashColor", color);


        // lerp the flash amount
        float currentFlashAmount = 0f;
        float elapsedTime = 0f;

        while(elapsedTime < _flashTime)
        {
            // iterate elapsed time 
            elapsedTime += Time.deltaTime;

            // lerp flash amount
            currentFlashAmount = Mathf.Lerp(1f, 0f, (elapsedTime / _flashTime));
            SetFlashAmount(currentFlashAmount);

            yield return null;
        }
    }

    private void SetFlashAmount(float amount)
    {
        characterMat.SetFloat("_FlashAmount", amount);
    }


    private IEnumerator ChopWood(ItemWorld itemWorld)
    {
        yield return new WaitForSeconds(2f);
        quest.goal.IngredientGathered();
        treeStump.SetActive(true);
        axe.SetActive(false);
        itemWorld.DestroySelf();
        inventory.AddItem(new Item { itemType = Item.ItemType.Wood, amount = 1 });
        woodCounter++;
    }

    private IEnumerator Fishing()
    {
        yield return new WaitForSeconds(3f);

        quest.goal.IngredientGathered();

        fishingRod.SetActive(false);
        inventory.AddItem(new Item { itemType = Item.ItemType.Fish, amount = 1 });
        // inventory.AddItem(new Item { itemType = Item.ItemType.FishingRod, amount = 1 });
        fishCounter++;

    }

}

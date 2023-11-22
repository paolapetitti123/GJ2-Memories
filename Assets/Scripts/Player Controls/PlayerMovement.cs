using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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




    void Start()
    {
        inventory = new Inventory(UseItem);
        uiInv.SetInventory(inventory);
        uiInv.SetPlayer(this);
        InventoryWarningMessage.SetActive(false);
    }

    /* Update is called once per frame so we need to check when the player hits certain keys
     * the movement for those keys will be handled in FixedUpdate
     */
    void Update()
    {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();

        if(itemWorld != null)
        {
            Debug.Log("IM TOUCHING YOUUUU ");

            if(inventory.GetItemList().Count < 10)
            {
                if (itemWorld.GetItem().isParent())
                {
                    Debug.Log("item sprite: " + itemWorld.GetItem().GetSprite().name);
                    if(itemWorld.GetItem().GetSprite().name == "well-1")
                    {
                        inventory.AddItem(new Item { itemType = Item.ItemType.Water, amount = 1 });
                    }
                    else if (itemWorld.GetItem().GetSprite().name == "flowers-3")
                    {
                        inventory.AddItem(new Item { itemType = Item.ItemType.Flower, amount = 1 });

                    }
                    else if(itemWorld.GetItem().GetSprite().name == "herb-plant")
                    {
                        inventory.AddItem(new Item { itemType = Item.ItemType.Herb, amount = 1 });
                    }
                    else if (itemWorld.GetItem().GetSprite().name == "berry-bush")
                    {
                        inventory.AddItem(new Item { itemType = Item.ItemType.Berries, amount = 1 });
                    }
                }
                else if (!itemWorld.GetItem().isParent())
                {
                    inventory.AddItem(itemWorld.GetItem());
                    itemWorld.DestroySelf();
                }
                
            }
            else
            {
                InventoryWarningMessage.SetActive(true);
                InventoryWarningButton = GameObject.FindGameObjectWithTag("InventoryFullMessage");

                InventoryWarningButton.GetComponent<Button_UI>().ClickFunc = () =>
                {
                    InventoryWarningMessage.SetActive(false);
                };
            }

            
        }
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        ItemWorld itemWorld =  collision.collider.GetComponent<ItemWorld>();

        if (itemWorld != null)
        {
            Debug.Log("IM TOUCHING YOUUUU ");

            if (inventory.GetItemList().Count < 10)
            {
                if (itemWorld.GetItem().isParent())
                {
                    Debug.Log("item sprite: " + itemWorld.GetItem().GetSprite().name);
                    if (itemWorld.GetItem().GetSprite().name == "well-1")
                    {
                        inventory.AddItem(new Item { itemType = Item.ItemType.Water, amount = 1 });
                    }
                    else if (itemWorld.GetItem().GetSprite().name == "flowers-3")
                    {
                        inventory.AddItem(new Item { itemType = Item.ItemType.Flower, amount = 1 });

                    }
                    else if (itemWorld.GetItem().GetSprite().name == "herb-plant")
                    {
                        inventory.AddItem(new Item { itemType = Item.ItemType.Herb, amount = 1 });
                    }
                    else if (itemWorld.GetItem().GetSprite().name == "berry-bush")
                    {
                        inventory.AddItem(new Item { itemType = Item.ItemType.Berries, amount = 1 });
                    }
                }
                else if (!itemWorld.GetItem().isParent())
                {
                    inventory.AddItem(itemWorld.GetItem());
                    itemWorld.DestroySelf();
                }

            }
            else
            {
                InventoryWarningMessage.SetActive(true);
                InventoryWarningButton = GameObject.FindGameObjectWithTag("InventoryFullMessage");

                InventoryWarningButton.GetComponent<Button_UI>().ClickFunc = () =>
                {
                    InventoryWarningMessage.SetActive(false);
                };
            }


        }
    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Potion1:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Potion1, amount = 1 });
                StartCoroutine(FlashColor(FlashOrangeColor));
                break;
            case Item.ItemType.Potion2:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Potion2, amount = 1 });
                StartCoroutine(FlashColor(FlashGreenColor));
                break;
            case Item.ItemType.Potion3:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Potion3, amount = 1 });
                StartCoroutine(FlashColor(FlashRedColor));
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

}

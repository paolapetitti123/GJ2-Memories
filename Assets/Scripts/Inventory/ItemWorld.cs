using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using TMPro;

public class ItemWorld : MonoBehaviour
{
    private Item item;
    private SpriteRenderer spriteRend;
    private TextMeshPro txtMshPro;



    public void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        txtMshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Debug.Log("Item name: " + item.itemType);

        Transform itemWorldTrans = ItemAssets.Instance.pfItemWorld;

        Transform transform;
        ItemWorld itemWorld;

        

        transform = Instantiate(itemWorldTrans, position, Quaternion.identity);
        itemWorld = transform.GetComponent<ItemWorld>();

        if (item.IsAWell() || item.IsAnHerbBush())
        {
            itemWorld.GetComponent<CircleCollider2D>().radius += 2;
        }
        
        if(item.IsAFlowerBush() || item.IsABerryBush())
        {
            itemWorld.GetComponent<CircleCollider2D>().radius += 1;
        }
        if (item.IsAFishingSpot())
        {
            itemWorld.GetComponent<Transform>().localScale = new Vector3(0.4f,0.4f,0.4f);
            itemWorld.GetComponent<CircleCollider2D>().radius += 2;
        }

        itemWorld.SetItem(item);

        return itemWorld;




    }

    public void SetItem(Item item)
    {
        this.item = item;
        spriteRend.sprite = item.GetSprite();
        if(item.amount > 1)
        {
            txtMshPro.SetText(item.amount.ToString());
        }
        else
        {
            txtMshPro.SetText("");
        }
        
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    {
        Vector3 randomDir = UtilsClass.GetRandomDir();
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir * 2f, item);
        itemWorld.GetComponent<Rigidbody2D>().AddForce(randomDir * 2f, ForceMode2D.Impulse);
        itemWorld.GetComponent<Rigidbody2D>().Sleep();

        


        return itemWorld;

    }
}

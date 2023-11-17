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
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
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

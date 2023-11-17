using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemWorld : MonoBehaviour
{
    private Item item;
    private SpriteRenderer spriteRend;

    public void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
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
        Vector3 randomDir = Random.insideUnitCircle.normalized;
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir * 5f, item);
        itemWorld.GetComponent<Rigidbody2D>().AddForce(randomDir );

        return itemWorld;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum ItemType
    {
        Axe,
        Mushroom,
        Herb,
        Flower,
        Wood,
        Berries,
        Water,
        FishingRod,
        Fish
    }

    public ItemType itemType;
    public int amount;
}

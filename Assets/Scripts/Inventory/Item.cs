using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
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

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Axe:        return ItemAssets.Instance.axeSprite;
            case ItemType.Mushroom:   return ItemAssets.Instance.mushroomSprite;
            case ItemType.Herb:       return ItemAssets.Instance.herbSprite;
            case ItemType.Flower:     return ItemAssets.Instance.flowerSprite;
            case ItemType.Wood:       return ItemAssets.Instance.woodSprite;
            case ItemType.Berries:    return ItemAssets.Instance.berriesSprite;
            case ItemType.Water:      return ItemAssets.Instance.waterSprite;
            case ItemType.FishingRod: return ItemAssets.Instance.fishingRodSprite;
            case ItemType.Fish:       return ItemAssets.Instance.fishSprite;
        }
    }

    public bool isStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Axe:          return false;
            case ItemType.Mushroom:     return true;
            case ItemType.Herb:         return true;
            case ItemType.Flower:       return true;
            case ItemType.Wood:         return true;
            case ItemType.Berries:      return true;
            case ItemType.Water:        return false;
            case ItemType.FishingRod:   return false;
            case ItemType.Fish:         return true;
        }
    }
}

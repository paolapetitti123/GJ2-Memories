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
        Fish,
        Potion1,
        Potion2,
        Potion3,
        FlowerBush,
        WaterWell,
        HerbBush,
        BerryBush
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
            case ItemType.Potion1:    return ItemAssets.Instance.potion1Sprite;
            case ItemType.Potion2:    return ItemAssets.Instance.potion2Sprite;
            case ItemType.Potion3:    return ItemAssets.Instance.potion3Sprite;
            case ItemType.FlowerBush: return ItemAssets.Instance.flowerBush;
            case ItemType.WaterWell:  return ItemAssets.Instance.waterWell;
            case ItemType.HerbBush:   return ItemAssets.Instance.herbBush;
            case ItemType.BerryBush:  return ItemAssets.Instance.berryBush;
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


    public bool isUseable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Axe:          return false;
            case ItemType.Mushroom:     return false;
            case ItemType.Herb:         return false;
            case ItemType.Flower:       return false;
            case ItemType.Wood:         return false;
            case ItemType.Berries:      return false;
            case ItemType.Water:        return false;
            case ItemType.FishingRod:   return true;
            case ItemType.Fish:         return false;
            case ItemType.Potion1:      return true;
            case ItemType.Potion2:      return true;
            case ItemType.Potion3:      return true;
        }
    }

    public bool isParent()
    {
        switch (itemType)
        {
            default:
            case ItemType.Axe:        return false;
            case ItemType.Mushroom:   return false;
            case ItemType.Herb:       return false;
            case ItemType.Flower:     return false;
            case ItemType.Wood:       return false;
            case ItemType.Berries:    return false;
            case ItemType.Water:      return false;
            case ItemType.FishingRod: return false;
            case ItemType.Fish:       return false;
            case ItemType.Potion1:    return false;
            case ItemType.Potion2:    return false;
            case ItemType.Potion3:    return false;
            case ItemType.FlowerBush: return true;
            case ItemType.WaterWell:  return true;
            case ItemType.HerbBush:   return true;
            case ItemType.BerryBush:  return true;
        }
    
    }

    public bool IsAWell()
    {
        switch (itemType)
        {
            default:
            case ItemType.Axe: return false;
            case ItemType.Mushroom: return false;
            case ItemType.Herb: return false;
            case ItemType.Flower: return false;
            case ItemType.Wood: return false;
            case ItemType.Berries: return false;
            case ItemType.Water: return false;
            case ItemType.FishingRod: return false;
            case ItemType.Fish: return false;
            case ItemType.Potion1: return false;
            case ItemType.Potion2: return false;
            case ItemType.Potion3: return false;
            case ItemType.FlowerBush: return false;
            case ItemType.WaterWell: return true;
            case ItemType.HerbBush: return false;
            case ItemType.BerryBush: return false;
        }
    }

    public bool IsAFlowerBush()
    {
        switch (itemType)
        {
            default:
            case ItemType.Axe: return false;
            case ItemType.Mushroom: return false;
            case ItemType.Herb: return false;
            case ItemType.Flower: return false;
            case ItemType.Wood: return false;
            case ItemType.Berries: return false;
            case ItemType.Water: return false;
            case ItemType.FishingRod: return false;
            case ItemType.Fish: return false;
            case ItemType.Potion1: return false;
            case ItemType.Potion2: return false;
            case ItemType.Potion3: return false;
            case ItemType.FlowerBush: return true;
            case ItemType.WaterWell: return false;
            case ItemType.HerbBush: return false;
            case ItemType.BerryBush: return false;
        }
    }

    public bool IsAnHerbBush()
    {
        switch (itemType)
        {
            default:
            case ItemType.Axe: return false;
            case ItemType.Mushroom: return false;
            case ItemType.Herb: return false;
            case ItemType.Flower: return false;
            case ItemType.Wood: return false;
            case ItemType.Berries: return false;
            case ItemType.Water: return false;
            case ItemType.FishingRod: return false;
            case ItemType.Fish: return false;
            case ItemType.Potion1: return false;
            case ItemType.Potion2: return false;
            case ItemType.Potion3: return false;
            case ItemType.FlowerBush: return false;
            case ItemType.WaterWell: return false;
            case ItemType.HerbBush: return true;
            case ItemType.BerryBush: return false;
        }
    }

    public bool IsABerryBush()
    {
        switch (itemType)
        {
            default:
            case ItemType.Axe: return false;
            case ItemType.Mushroom: return false;
            case ItemType.Herb: return false;
            case ItemType.Flower: return false;
            case ItemType.Wood: return false;
            case ItemType.Berries: return false;
            case ItemType.Water: return false;
            case ItemType.FishingRod: return false;
            case ItemType.Fish: return false;
            case ItemType.Potion1: return false;
            case ItemType.Potion2: return false;
            case ItemType.Potion3: return false;
            case ItemType.FlowerBush: return false;
            case ItemType.WaterWell: return false;
            case ItemType.HerbBush: return false;
            case ItemType.BerryBush: return true;
        }
    }

    public bool IsAMushroom()
    {
        switch (itemType)
        {
            default:
            case ItemType.Axe: return false;
            case ItemType.Mushroom: return true;
            case ItemType.Herb: return false;
            case ItemType.Flower: return false;
            case ItemType.Wood: return false;
            case ItemType.Berries: return false;
            case ItemType.Water: return false;
            case ItemType.FishingRod: return false;
            case ItemType.Fish: return false;
            case ItemType.Potion1: return false;
            case ItemType.Potion2: return false;
            case ItemType.Potion3: return false;
            case ItemType.FlowerBush: return false;
            case ItemType.WaterWell: return false;
            case ItemType.HerbBush: return false;
            case ItemType.BerryBush: return false;
        }
    }
}

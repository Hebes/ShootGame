using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item
{
    public enum ItemType
    {
        Normal,
        Sword,
        HealthPotion,
        ManaPotion,
        Coin,
        Medkit,
    }

    public ItemType itemType;
    public int amount;//数量

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword:
                return GameManager.Instance.swordSprite;
            case ItemType.HealthPotion:
                return GameManager.Instance.healthPotionSprite;
            case ItemType.ManaPotion:
                return GameManager.Instance.manaPotionSprite;
            case ItemType.Coin:
                return GameManager.Instance.coinSprite;
            case ItemType.Medkit:
                return GameManager.Instance.medkitSprite;
        }
    }

    public Color GetColor()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword:
                return new Color(0, 0, 0);
            case ItemType.HealthPotion:
                return new Color(1, 0, 0);
            case ItemType.ManaPotion:
                return new Color(0, 0, 1);
            case ItemType.Coin:
                return new Color(1, 1, 0);
            case ItemType.Medkit:
                return new Color(0, 1, 1);
        }

    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Coin:
            case ItemType.HealthPotion:
            case ItemType.ManaPotion:
            case ItemType.Medkit:
                return true;
            case ItemType.Sword:
                return false;
        }
    }
}

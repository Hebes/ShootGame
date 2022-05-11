using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物体标识符
/// </summary>
public class ItemIdentifier : SingletonMono<ItemIdentifier>
{
    public enum ItemType
    {
        Medkit,
        Helmet,
        Armor,
    }

    public ItemType itemType;

    public Sprite GetItemSprite(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Medkit:
                return GameManager.Instance.pingMedkitSprite;
            case ItemType.Helmet:
                return GameManager.Instance.pingHelmetSprite;
            case ItemType.Armor:
                return GameManager.Instance.pingArmorSprite;
        }
    }

    public Color GetItemColor(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Medkit:
                return Color.white;
            case ItemType.Helmet:
                return GameManager.Instance.pingHelmetColor;
            case ItemType.Armor:
                return GameManager.Instance.pingArmorColor;
        }
    }
}

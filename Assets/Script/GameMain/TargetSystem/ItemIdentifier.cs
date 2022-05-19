using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物体标识符
/// </summary>
public class ItemIdentifier : SingletonMono_Temp<ItemIdentifier>
{
    public enum ItemType
    {
        Medkit,
        Helmet,
        Armor,
    }

    public ItemType itemType;

    /// <summary>
    /// 设置图标的图片
    /// </summary>
    /// <param name="itemType"></param>
    /// <returns></returns>
    public Sprite GetItemSprite(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Medkit:
                return Res_Sprite_Manage.Instance.Get_sprite(ESprite.MedkitPing); 
            case ItemType.Helmet:
                return Res_Sprite_Manage.Instance.Get_sprite(ESprite.ArmorPing); 
            case ItemType.Armor:
                return Res_Sprite_Manage.Instance.Get_sprite(ESprite.HelmetPing); 
        }
    }

    /// <summary>
    /// 设置图标的颜色
    /// </summary>
    /// <param name="itemType"></param>
    /// <returns></returns>
    public Color GetItemColor(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Medkit:
                return Config_Color.CMedkit;
            case ItemType.Helmet:
                return Config_Color.CHelmet;
            case ItemType.Armor:
                return Config_Color.CArmor;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item
{
    public EItemType itemType;
    public int amount;//数量

    public enum EItemType
    {
        Normal,
        Sword,
        HealthPotion,
        ManaPotion,
        Coin,
        Medkit,
    }

    //获取精灵图片
    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case EItemType.Sword:         return Manage_Res_Sprite.Instance.Get_sprite(ESprite.Sword);
            case EItemType.HealthPotion:  return Manage_Res_Sprite.Instance.Get_sprite(ESprite.HealthPotion);
            case EItemType.ManaPotion:    return Manage_Res_Sprite.Instance.Get_sprite(ESprite.ManaPotion);
            case EItemType.Coin:          return Manage_Res_Sprite.Instance.Get_sprite(ESprite.Coin);
            case EItemType.Medkit:        return Manage_Res_Sprite.Instance.Get_sprite(ESprite.Medkit);
        }
    }

    /// <summary>
    /// 获取颜色
    /// </summary>
    /// <returns></returns>
    public Color GetColor()
    {
        switch (itemType)
        {
            default:
            case EItemType.Sword:        return Config_Color.CShineSword;
            case EItemType.HealthPotion: return Config_Color.CShineHealthPotion;
            case EItemType.ManaPotion:   return Config_Color.CShineManaPotion;
            case EItemType.Coin:         return Config_Color.CShineCoin;
            case EItemType.Medkit:       return Config_Color.CShineMedkit;
        }
    }

    /// <summary>
    /// 是否可以堆叠？
    /// </summary>
    /// <returns></returns>
    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case EItemType.Coin:
            case EItemType.HealthPotion:
            case EItemType.ManaPotion:
            case EItemType.Medkit:
                return true;
            case EItemType.Sword:
                return false;
        }
    }
}

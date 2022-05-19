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


    //获取精灵图片
    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword:
                return Res_Sprite_Manage.Instance.Get_sprite(ESprite.Sword);
            case ItemType.HealthPotion:
                return Res_Sprite_Manage.Instance.Get_sprite(ESprite.HealthPotion);
            case ItemType.ManaPotion:
                return Res_Sprite_Manage.Instance.Get_sprite(ESprite.ManaPotion);
            case ItemType.Coin:
                return Res_Sprite_Manage.Instance.Get_sprite(ESprite.Coin);
            case ItemType.Medkit:
                return Res_Sprite_Manage.Instance.Get_sprite(ESprite.Medkit);

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
            case ItemType.Sword:
                return Config_Color.CShineSword;
            case ItemType.HealthPotion:
                return Config_Color.CShineHealthPotion;
            case ItemType.ManaPotion:
                return Config_Color.CShineManaPotion;
            case ItemType.Coin:
                return Config_Color.CShineCoin;
            case ItemType.Medkit:
                return Config_Color.CShineMedkit;
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

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
                //return GameManager.Instance.swordSprite;
                return MainGame_Res_Sprite_Manage.Instance.Get_sprite("Sword");
            case ItemType.HealthPotion:
                //return GameManager.Instance.healthPotionSprite;
                return MainGame_Res_Sprite_Manage.Instance.Get_sprite("HealthPotion");

            case ItemType.ManaPotion:
                //return GameManager.Instance.manaPotionSprite;
                return MainGame_Res_Sprite_Manage.Instance.Get_sprite("ManaPotion");

            case ItemType.Coin:
                //return GameManager.Instance.coinSprite;
                return MainGame_Res_Sprite_Manage.Instance.Get_sprite("Coin");

            case ItemType.Medkit:
                //return GameManager.Instance.medkitSprite;
                return MainGame_Res_Sprite_Manage.Instance.Get_sprite("Medkit");

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

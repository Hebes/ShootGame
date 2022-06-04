using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity中的简单库存系统(存储，使用，堆叠和掉落道具)
/// Simple Inventory System in Unity (Store, Use, Stack and Drop Items)
/// 参考：https://www.youtube.com/watch?v=2WnAOV7nHW0
/// </summary>
public class Inventory1
{
    ///<summary>物品列表交换触发的事件</summary>
    public event EventHandler OnItemListChanged;
    public Action<ConfigItemData> userItemAction;
    private List<ConfigItemData> itemDataList;

    public List<ConfigItemData> GetItemDataList => itemDataList;
    public Inventory1() => itemDataList = new List<ConfigItemData>();
    /// <summary>
    /// 添加物品
    /// </summary>
    /// <param name="item1"></param>
    public void AddItemOnInventory(ConfigItemData item1)
    {
        if (item1.isStackable)//判断是否可堆叠
        {
            bool itemAlreadyInInventory = false;//库存中的物品
            foreach (var itemData in itemDataList)
            {
                if (itemData.iconName == item1.iconName)
                {
                    itemData.amount += item1.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
                itemDataList.Add(item1);//直接添加
        }
        else
            itemDataList.Add(item1);//直接添加
        OnItemListChanged?.Invoke(this,EventArgs.Empty);//刷新
    }

    /// <summary>
    /// 减少物品
    /// </summary>
    /// <param name="item1"></param>
    public void RemoveItemOnInventory(ConfigItemData item1)
    {
        if (item1.isStackable)//不能堆叠的话直接删除
        {
            ConfigItemData item1Temp = null;

            for (int i = 0; i < itemDataList.Count; i++)
            {
                if (itemDataList[i].iconName == item1.iconName)
                {
                    itemDataList[i].amount -= item1.amount;
                    item1Temp = itemDataList[i];
                }
            }
            if (item1Temp != null && item1Temp.amount <= 0)
                itemDataList.Remove(item1Temp);
        }
        else
            itemDataList.Remove(item1);//TUDO  有一个不能堆叠 但是删除数量不对的BUG
        OnItemListChanged?.Invoke(this, EventArgs.Empty);//刷新
    }

    /// <summary>
    /// 使用物品
    /// </summary>
    public void UseItemOnInventory(ConfigItemData item1) => userItemAction(item1);
}

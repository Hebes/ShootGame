using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Unity中的简单库存系统(存储，使用，堆叠和掉落道具)
/// Simple Inventory System in Unity (Store, Use, Stack and Drop Items)
/// 参考：https://www.youtube.com/watch?v=2WnAOV7nHW0
/// </summary>
public class Inventory
{
    public event EventHandler OnItemListChanged;//物品列表交换触发的事件
    private Action<Item> userItemAction;//使用物品后触发的事件

    private List<Item> itemList;

    public Inventory(Action<Item> userItemAction)
    {
        this.userItemAction = userItemAction;
        itemList = new List<Item>();
    }

    /// <summary>
    /// 增加物品
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(Item item)
    {
        if (item.IsStackable())//是否可以堆叠
        {
            bool itemAlreadyInInventory = false;//库存中的物品

            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)//TUDO 需要添加更多的条件判断
                {
                    inventoryItem.amount += item.amount;//存在数量++
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory) itemList.Add(item);
        }
        else//不可以堆叠的话直接添加
            itemList.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);//物品列表交换触发的事件
    }

    public List<Item> GetItemList => itemList;

    /// <summary>
    /// 减少物品
    /// </summary>
    /// <param name="item"></param>
    internal void RemoveItem(Item item)
    {
        if (item.IsStackable())//是否可以堆叠
        {
            Item itemInInventory = null;

            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemList.Remove(itemInInventory);
            }
        }
        else
        {
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    internal void UserItem(Item item) => userItemAction(item);
}

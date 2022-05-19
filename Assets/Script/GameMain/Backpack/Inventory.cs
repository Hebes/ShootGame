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
    public event EventHandler OnItemListChanged;
    private Action<Item> userItemAction;

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
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;

            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList
    {
        get { return itemList; }
    }

    /// <summary>
    /// 减少物品
    /// </summary>
    /// <param name="item"></param>
    internal void RemoveItem(Item item)
    {
        if (item.IsStackable())
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
            if (itemInInventory!=null&& itemInInventory.amount<=0)
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

    internal void UserItem(Item item)
    {
        userItemAction(item);
    }
}

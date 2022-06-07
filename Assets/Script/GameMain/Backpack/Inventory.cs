using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity中的简单库存系统(存储，使用，堆叠和掉落道具)
/// Simple Inventory System in Unity (Store, Use, Stack and Drop Items)
/// 参考：https://www.youtube.com/watch?v=2WnAOV7nHW0
/// </summary>
public class Inventory : IItemHolder
{
    ///<summary>物品列表交换触发的事件</summary>
    public event EventHandler OnItemListChanged;
    public Action<Item> userItemAction;
    private List<Item> itemDataList;
    private InventorySlot[] inventorySlotArray;//热键库存槽

    public List<Item> GetItemDataList => itemDataList;
    public void UseItem(Item item) => userItemAction(item);// 使用物品
    public InventorySlot[] GetInventorySlotArray => inventorySlotArray;
    public Inventory(int inventorySlotCount)
    {
        itemDataList = new List<Item>();

        inventorySlotArray = new InventorySlot[inventorySlotCount];
        for (int i = 0; i < inventorySlotCount; i++)
            inventorySlotArray[i] = new InventorySlot(i);
    }

    /// <summary>
    /// 获得空的物品槽
    /// </summary>
    /// <returns></returns>
    public InventorySlot GetEmptyInventorySlot()
    {
        foreach (InventorySlot inventorySlot in inventorySlotArray)
            if (inventorySlot.IsEmpty()) 
                return inventorySlot;
        Debug.LogError("没有找到一个空的物品槽!");
        return null;
    }
    /// <summary>
    /// 获取物品槽里面的物体
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public InventorySlot GetInventorySlotWithItem(Item item)
    {
        foreach (InventorySlot inventorySlot in inventorySlotArray)
            if (inventorySlot.GetItem == item) return inventorySlot;
        Debug.LogError("没有找到物体 " + item + " 在物品槽里面!");
        return null;
    }

    /// <summary>
    /// 交换物品
    /// </summary>
    /// <param name="oldInventorySlot">旧的库存槽</param>
    /// <param name="newInventorySlot">新的库存槽</param>
    public void ChangeInventorySlotWithItem(InventorySlot oldInventorySlot,InventorySlot newInventorySlot)
    {
        Item tmpItem = oldInventorySlot.GetItem;
        oldInventorySlot.SetItem(newInventorySlot.GetItem);
        newInventorySlot.SetItem(tmpItem);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);   //执行刷新方法
    }

    /// <summary>
    /// 添加物品
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(Item item)
    {
        itemDataList.Add(item);                             //物品数据添加到列表
        item.itemHolder = this;                           //将这个物品设置3个方法
        GetEmptyInventorySlot().SetItem(item);              //设置物品属性
        OnItemListChanged?.Invoke(this, EventArgs.Empty);   //执行刷新方法
    }

    /// <summary>
    /// 添加物品
    /// </summary>
    /// <param name="item"></param>
    /// <param name="inventorySlot">第几个格子</param>
    public void AddItem(Item item, InventorySlot inventorySlot)
    {
        // Add Item to a specific Inventory slot
        itemDataList.Add(item);                             //物品数据添加到列表
        item.itemHolder = this;                           //将这个物品设置3个方法
        inventorySlot.SetItem(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);   //执行刷新方法
    }
    /// <summary>
    /// 删除物品
    /// </summary>
    /// <param name="item"></param>
    public void RemoveItem(Item item)
    {
        GetInventorySlotWithItem(item).RemoveItem();        //移除物品，数据置空
        itemDataList.Remove(item);                          //列表删除物品数据
        OnItemListChanged?.Invoke(this, EventArgs.Empty);   //执行刷新方法
    }
    /// <summary>
    /// 是否能添加物品
    /// </summary>
    /// <returns></returns>
    public bool CanAddItem() => GetEmptyInventorySlot() != null;

    /// <summary>
    /// 添加物品
    /// </summary>
    /// <param name="item"></param>
    public void AddItemDateOnAmount(Item item)
    {
        if (item.GetConfigItemData.isStackable)           //判断是否可堆叠
        {
            bool itemIsAlreadyTemp = false;                 //物品是否存在(临时)
            foreach (var itemData in itemDataList)
            {
                if (itemData.GetConfigItemData.iconName == item.GetConfigItemData.iconName)
                {
                    itemData.GetConfigItemData.amount += item.GetConfigItemData.amount;
                    itemIsAlreadyTemp = true;
                }
            }
            if (!itemIsAlreadyTemp)
            {
                itemDataList.Add(item);//直接添加
                item.itemHolder = this;
                GetEmptyInventorySlot().SetItem(item);
            }
        }
        else
        {
            itemDataList.Add(item);//直接添加
            item.itemHolder = this;
            GetEmptyInventorySlot().SetItem(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);//刷新
    }

    /// <summary>
    /// 减少物品
    /// </summary>
    /// <param name="item"></param>
    public void RemoveItemDateOnAmount(Item item)
    {
        if (item.GetConfigItemData.isStackable)//不能堆叠的话直接删除
        {
            Item item1Temp = null;

            for (int i = 0; i < itemDataList.Count; i++)
            {
                if (itemDataList[i].GetConfigItemData.iconName == item.GetConfigItemData.iconName)
                {
                    itemDataList[i].GetConfigItemData.amount -= item.GetConfigItemData.amount;
                    item1Temp = itemDataList[i];
                }
            }
            if (item1Temp != null && item1Temp.GetConfigItemData.amount <= 0)
            {
                GetInventorySlotWithItem(item1Temp).RemoveItem();
                itemDataList.Remove(item1Temp);
            }
        }
        else
        {
            GetInventorySlotWithItem(item).RemoveItem();
            itemDataList.Remove(item);//TUDO  有一个不能堆叠 但是删除数量不对的BUG
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);//刷新
    }
}

/// <summary>
/// 表示单个库存槽
/// </summary>
public class InventorySlot
{
    private int index;
    private Item item;
    public Transform transform;

    public InventorySlot(int index) => this.index = index;
    public Item SetItem(Item item) => this.item = item;
    public Item GetItem => item;
    public void RemoveItem() => item = null;
    public bool IsEmpty() => item == null;
}

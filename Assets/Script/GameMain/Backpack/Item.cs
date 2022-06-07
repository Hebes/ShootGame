using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品类型
/// </summary>
public enum EItemType
{

}
/// <summary>
/// 装备类型
/// </summary>
public enum EEquipSlot
{
    None,

}

[Serializable]
public class Item
{
    [SerializeField]
    private ConfigItemData configItemData;
    public EItemType eItemType;
    public EEquipSlot eEquipSlot;
    public IItemHolder itemHolder;

    public ConfigItemData SetConfigItemData(ConfigItemData configItemData) => this.configItemData = configItemData;
    public ConfigItemData GetConfigItemData => configItemData;

    public void RemoveFromItemHolder()
    {
        if (itemHolder != null)
            itemHolder.RemoveItem(this);// Remove from current Item Holder 从当前物品持有人中移除
    }
}

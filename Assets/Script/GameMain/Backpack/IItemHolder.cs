using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// item的持有者
/// </summary>
public interface IItemHolder
{
    void RemoveItem(ConfigItemData item);
    void AddItem(ConfigItemData item);
    bool CanAddItem();
}

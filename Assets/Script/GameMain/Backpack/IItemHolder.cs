using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// item็ๆๆ่
/// </summary>
public interface IItemHolder
{
    void RemoveItem(Item item);
    void AddItem(Item item);
    bool CanAddItem();
}

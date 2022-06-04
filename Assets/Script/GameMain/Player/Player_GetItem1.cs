using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GetItem1 : MonoBehaviour
{
    private Inventory1 inventory1;
    private UI_Inventory1 uI_Inventory1;

    private void Awake()
    {
        inventory1 = new Inventory1();
        uI_Inventory1 = GameObject.Find("UI_Inventory1").GetComponent<UI_Inventory1>();
        uI_Inventory1.SetInventory1(inventory1);
        inventory1.userItemAction = UseItem;
    }

    /// <summary>
    /// 捡起物品
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld1 itemWorld1 = collision.GetComponent<ItemWorld1>();
        if (itemWorld1 != null)
        {
            print(itemWorld1.name);
            ConfigItemData item1 = itemWorld1.GetItem();
            inventory1.AddItemOnInventory(item1);
            itemWorld1.DestroySelf();
        }
    }
    /// <summary>
    /// 玩家使用物品
    /// </summary>
    /// <param name="item"></param>
    private void UseItem(ConfigItemData item1)
    {
        Debug.Log("使用了物品：" + item1.iconName);
        inventory1.RemoveItemOnInventory(new ConfigItemData()
        {
            id = item1.id,
            name = item1.name,
            iconName = item1.iconName,
            explain = item1.explain,
            isStackable = item1.isStackable,
            amount = 1,
        });
    }
}

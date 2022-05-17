using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GetItem : MonoBehaviour
{
    #region 背包系统
    private Inventory inventory;
    //测试代码
    [SerializeField]
    private UI_Inventory uI_Inventory;
    #endregion

    private void Awake() => inventory = new Inventory(UseItem);

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.HealthPotion:
                //TDUO 这里可以添加吃药水得特效
                Debug.Log("吃了HP药水,HP回复了!(PS:这里可以添加吃药水得特效)");
                inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
                break;
            case Item.ItemType.ManaPotion:
                //TDUO 这里可以添加吃药水得特效
                Debug.Log("吃了MP药水,MP回复了!(PS:这里可以添加吃药水得特效)");
                inventory.RemoveItem(new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
                break;
            case Item.ItemType.Medkit:
                //TDUO 这里可以添加吃药水得特效
                Debug.Log("吃了HP药包,HP回复了!(PS:这里可以添加吃药包得特效)");
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Medkit, amount = 1 });
                break;
        }
    }

    private void Start()
    {
        uI_Inventory.SetPlayer(this);
        uI_Inventory.SetInventory(inventory);
    }

    /// <summary>
    /// 捡起物品
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if (itemWorld!=null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}

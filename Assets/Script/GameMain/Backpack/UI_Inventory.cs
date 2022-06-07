using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;
using System;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;

public enum EItemComponents
{
    Item_Icon,
    Item_amount,
    Content,
}

public class UI_Inventory : MonoBehaviour
{
    [SerializeField]
    private Transform tfItem;//需要实例化的物体
    [SerializeField]
    private Transform tfItemBG;//需要实例化的背景物体格子
    private Transform uI_Inventory_Content;
    private List<Transform> tfItemBGsList = new List<Transform>();
    private Player_Components player_Components;

    private Inventory inventory1;

    private void Awake() => FindComponents();
    private void Start() => RefreshInventoryItems();
    private void Update() => test1();//测试代码
    private void OnDestroy() => inventory1.OnItemListChanged -= Inventory_OnItemListChanged;

    public void SetInventory1(Inventory inventory1)
    {
        this.inventory1 = inventory1;
        inventory1.OnItemListChanged += Inventory_OnItemListChanged;
    }
    /// <summary>
    /// 寻找组件
    /// </summary>
    private void FindComponents()
    {
        player_Components = GameObject.Find("Player").GetComponent<Player_Components>();
        uI_Inventory_Content = transform.Find_Child<Transform>(EItemComponents.Content.ToString());
        AddItemBG(12);
        Test1();
    }

    /// <summary>
    /// 测试代码
    /// </summary>
    private void Test1()
    {
        //inventory1.GetItemDataList.Add(new Item() { id = 1, name = "血药", iconName = "HealthPotion", explain = "aeqweq", isStackable = true, amount = 2 });
    }

    /// <summary>
    /// 测试代码
    /// </summary>
    private void test1()
    {
        //if (Input.GetKeyDown(KeyCode.H))//添加
        //{
        //    inventory1.AddItemDateOnAmount(new Item() { id = 2, name = "魔药", iconName = "ManaPotion", explain = "可以补10点魔法", isStackable = true, amount = 2 });
        //}
        //if (Input.GetKeyDown(KeyCode.N))//删除
        //{
        //    inventory1.RemoveItemDateOnAmount(new Item() { id = 1, name = "血药", iconName = "HealthPotion", explain = "aeqweq", isStackable = true, amount = 1 });
        //}
        //if (Input.GetKeyDown(KeyCode.G))//删除
        //{
        //    inventory1.AddItemDateOnAmount(new Item() { id = 1, name = "血药", iconName = "HealthPotion", explain = "aeqweq", isStackable = true, amount = 1 });
        //}
    }
    /// <summary>
    /// 刷新库存物品
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Inventory_OnItemListChanged(object sender, EventArgs e) => RefreshInventoryItems();

    /// <summary>
    /// 刷新
    /// </summary>
    public void RefreshInventoryItems()
    {
        ClearAllItem();
        //if (inventory1.GetItemDataList.Count > tfItemBGsList.Count) AddItemBG(4);

        //for (int i = 0; i < inventory1.GetItemDataList.Count; i++)
        //{
        //    Transform tfItemTemp = Instantiate(tfItem, tfItemBGsList[i]);

        //    //设置图片和数量
        //    tfItemTemp.Find_Child<Image>(EItemComponents.Item_Icon.ToString()).sprite
        //        = Manage_Res_Sprite.Instance.Get_sprite(inventory1.GetItemDataList[i].iconName);
        //    tfItemTemp.Find_Child<TextMeshProUGUI>(EItemComponents.Item_amount.ToString()).text
        //        = inventory1.GetItemDataList[i].amount > 1 ? inventory1.GetItemDataList[i].amount.ToString() : string.Empty;

        //    Item itemData = inventory1.GetItemDataList[i];
        //    //鼠标点击事件
        //    tfItemTemp.GetComponent<Button_UI>().ClickFunc = () =>//使用物品
        //    {
        //        //Use item
        //        inventory1.UseItem(itemData);
        //    };

        //    tfItemTemp.GetComponent<Button_UI>().MouseRightClickFunc = () => //丢弃
        //    {
        //        Item duplicateItem = new Item()//确保itemData的引用空间不知向同一个
        //        {
        //            id = itemData.id,
        //            name = itemData.name,
        //            iconName = itemData.iconName,
        //            explain = itemData.explain,
        //            isStackable = itemData.isStackable,
        //            amount = itemData.amount,
        //        };
        //        ItemWorld.DropItem(player_Components.Player_Transform.position, duplicateItem);
        //        inventory1.RemoveItemDateOnAmount(itemData);

        //    };//右击
        //}
    }

    /// <summary>
    /// 循环查找单个物体的下面是否有物体，并且删除
    /// </summary>
    private void ClearAllItem()
    {
        for (int i = 0; i < tfItemBGsList.Count; i++)//删除所有
        {
            if (tfItemBGsList[i].childCount > 0)
                Destroy(tfItemBGsList[i].GetChild(0).gameObject);//删除所有
        }
    }

    /// <summary>
    /// 添加背景格子
    /// </summary>
    private void AddItemBG(int count)
    {
        for (int i = 0; i < count; i++)
            tfItemBGsList.Add(Instantiate(tfItemBG, uI_Inventory_Content));
    }
}

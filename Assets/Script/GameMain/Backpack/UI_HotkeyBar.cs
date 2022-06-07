using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;
using UnityEngine.UI;
using TMPro;
using System;
using CodeMonkey.Utils;

enum EUI_HotkeyBarComponent { Content, }


public class UI_HotkeyBar : MonoBehaviour
{
    [SerializeField]
    private Transform hotkeyBarItemBG;
    [SerializeField]
    private Transform tfHotkeyBarItem;
    private Transform tfContent;
    private List<Transform> HotkeyBarItemBGList;
    private Inventory inventory;

    private void Awake() => AwakeInit();
    private void Start() => StartInit();
    private void OnDestroy() => inventory.OnItemListChanged -= HotkeyBar_OnItemListChanged;
    private void HotkeyBar_OnItemListChanged(object sender, EventArgs e) => RefreshHotkeyBar();

    private void AwakeInit()
    {
        HotkeyBarItemBGList = new List<Transform>();
        inventory = new Inventory(6);
        tfContent = transform.Find_Child<Transform>(EUI_HotkeyBarComponent.Content.ToString());
        AddHotkeyBarItemBG(6);
    }

    private void StartInit()
    {
        inventory.OnItemListChanged += HotkeyBar_OnItemListChanged;
        Test();
    }
    /// <summary>
    /// 添加背景物品格子
    /// </summary>
    /// <param name="count"></param>
    private void AddHotkeyBarItemBG(int count)
    {
        ClearTfContents();
        for (int i = 0; i < count; i++)
        {
            Transform hotkeyBarItemTemp = Instantiate(hotkeyBarItemBG, tfContent);
            hotkeyBarItemTemp.name = hotkeyBarItemTemp + i.ToString();
            hotkeyBarItemTemp.GetComponent<HotkeyBarItem>().GetTextMeshProUGUI.SetText((i + 1).ToString());//设置编号
            RectTransform rectTransform = hotkeyBarItemTemp.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = (i == 0) ? new Vector2(100 * i, 0f) : new Vector2((100 * i) + (10 * i), 0f);//生成格子间距
            HotkeyBarItemBGList.Add(hotkeyBarItemTemp);//添加格子到列表
        }
    }

    /// <summary>
    /// 刷新快捷键栏 数据
    /// </summary>
    public void RefreshHotkeyBar()
    {
        ClearHotkeyBarItemListchild();

        int i = 0;
        foreach (InventorySlot inventorySlot in inventory.GetInventorySlotArray)
        {
            Item item = inventorySlot.GetItem;

            //设置数据
            if (!inventorySlot.IsEmpty())
            {
                //Not Empty, has Item 不为空 有物体
                Transform hotkeyBarItemTemp = Instantiate(tfHotkeyBarItem, HotkeyBarItemBGList[i]);//实例化

                UI_Item uiItem = hotkeyBarItemTemp.GetComponent<UI_Item>();
                uiItem.SetConfigItemData(inventorySlot.GetItem);

                //添加监听
                hotkeyBarItemTemp.GetComponent<Button_UI>().ClickFunc = () =>
                {
                    Debug.Log("快捷栏 使用了药品");
                };
                hotkeyBarItemTemp.GetComponent<Button_UI>().MouseRightClickFunc = () =>
                {
                    Debug.Log("快捷栏 拆分了药品");
                };
            }

            //设置物品和物品数据交换
            HotkeyBarItem tmpHotkeyBarItem = HotkeyBarItemBGList[i].GetComponent<HotkeyBarItem>();
            tmpHotkeyBarItem.SetOnDropAction(() =>
            {
                Item draggedItem = UI_ItemDrag.Instance.GetItem();//临时数据存储获得
                InventorySlot tmpInventorySlot = inventory.GetInventorySlotWithItem(draggedItem);//存储被拖拽的临时信息

                if (inventorySlot.GetItem != null)
                {
                    //两个物体一样，并且可添加的  比如药品
                    if (draggedItem.GetConfigItemData.iconName == inventorySlot.GetItem.GetConfigItemData.iconName
                    && draggedItem.GetConfigItemData.isStackable && inventorySlot.GetItem.GetConfigItemData.isStackable)
                        draggedItem.GetConfigItemData.amount += inventorySlot.GetItem.GetConfigItemData.amount;
                    //两个物体不一样
                    if (draggedItem.GetConfigItemData.iconName != inventorySlot.GetItem.GetConfigItemData.iconName)
                        inventory.ChangeInventorySlotWithItem(tmpInventorySlot, inventorySlot);
                }
                Debug.Log("测试");
                draggedItem.RemoveFromItemHolder();
                inventory.AddItem(draggedItem, inventorySlot);
            });
            i++;
        }
    }

    private void ClearTfContents()
    {
        foreach (Transform child in tfContent)
            Destroy(child.gameObject);
    }

    /// <summary>
    /// 清空HotkeyBarItemBGList的子物体
    /// </summary>
    private void ClearHotkeyBarItemListchild()
    {
        foreach (Transform child in HotkeyBarItemBGList)
        {
            foreach (Transform item in child)
            {
                if (item.name == "Number") continue;
                Destroy(item.gameObject);
            }
        }
    }

    /// <summary>
    /// 测试代码
    /// </summary>
    private void Test()
    {
        List<Item> test = new List<Item>();

        Item item1 = new Item();
        item1.SetConfigItemData(new ConfigItemData() { id = 2, name = "魔药", iconName = "ManaPotion", explain = "可以补10点魔法", isStackable = true, amount = 2 });
        test.Add(item1);

        Item item2 = new Item();
        item2.SetConfigItemData(new ConfigItemData() { id = 1, name = "血药", iconName = "HealthPotion", explain = "可以补10点血", isStackable = true, amount = 2 });
        test.Add(item2);

        Item item3 = new Item();
        item3.SetConfigItemData(Manage_JsonRead.Instance.GetitemDataDic(1001));//TUDO 有个可堆叠的BUG
        test.Add(item3);

        foreach (Item item in test)
        {
            Debug.Log(item.GetConfigItemData.iconName);
            inventory.AddItem(item);
        }
    }
}

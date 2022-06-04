using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;
using UnityEngine.UI;
using TMPro;

enum EUI_HotkeyBarComponent { Content, }


public class UI_HotkeyBar : MonoBehaviour
{
    [SerializeField]
    private Transform hotkeyBarItemBG;
    [SerializeField]
    private Transform tfHotkeyBarItem;

    private List<Transform> HotkeyBarItemList = new List<Transform>();

    private Inventory1 inventory1 = new Inventory1();
    private List<ConfigItemData> itemDataList;
    private Transform tfContent;


    private void Awake()
    {
        tfContent = transform.Find_Child<Transform>(EUI_HotkeyBarComponent.Content.ToString());
        AddHotkeyBarItem(6);//添加格子
        Test();
    }

    /// <summary>
    /// 添加物品格子
    /// </summary>
    /// <param name="count"></param>
    private void AddHotkeyBarItem(int count)
    {
        Claer();

        for (int i = 0; i < count; i++)
        {
            Transform hotkeyBarItemTemp
                = Instantiate(hotkeyBarItemBG, tfContent);
            hotkeyBarItemTemp.GetComponent<HotkeyBarItem>().GetTextMeshProUGUI.SetText((i + 1).ToString());
            RectTransform rectTransform = hotkeyBarItemTemp.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = (i == 0) ? new Vector2(100 * i, 0f) : new Vector2((100 * i) + (10 * i), 0f);//生成格子间距
            HotkeyBarItemList.Add(hotkeyBarItemTemp);


        }
    }

    /// <summary>
    /// 刷新快捷键栏 数据
    /// </summary>
    public void RefreshHotkeyBar()
    {
        itemDataList = inventory1.GetItemDataList;
        for (int i = 0; i < inventory1.GetItemDataList.Count; i++)
        {
            if (inventory1.GetItemDataList[i] == null) continue;

            Transform hotkeyBarItemTemp = Instantiate(tfHotkeyBarItem, HotkeyBarItemList[i]);//实例化
            UI_Item uI_Item_temp = hotkeyBarItemTemp.GetComponent<UI_Item>();//获取组件
            uI_Item_temp.SetItemSprite(Manage_Res_Sprite.Instance.Get_sprite(inventory1.GetItemDataList[i].iconName));//设置名字
            uI_Item_temp.SetitemAmountText(inventory1.GetItemDataList[i].amount);//设置数量
            uI_Item_temp.SetConfigItemData(inventory1.GetItemDataList[i]);//设置数据

            HotkeyBarItem hotkeyBarItem = hotkeyBarItemTemp.GetComponent<HotkeyBarItem>();
            hotkeyBarItem.SetOnDropAction(() =>
            {
                ConfigItemData configItemData = UI_ItemDrag.Instance.GetConfigItemData();//在这个UI道具槽上被删除
                
            });
        }
    }

    /// <summary>
    /// 测试代码
    /// </summary>
    private void Test()
    {
        inventory1.GetItemDataList.Add(null);
        inventory1.GetItemDataList.Add(new ConfigItemData()
        { id = 1, name = "血药", iconName = "HealthPotion", explain = "aeqweq", isStackable = true, amount = 2 });

        //GameObject.Find("pfUI_ItemDrag").GetComponent<UI_ItemDrag>().Hide();
    }

    private void Claer()
    {
        foreach (Transform child in tfContent)
        {
            //if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
    }
}

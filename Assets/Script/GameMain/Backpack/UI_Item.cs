using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

enum EUI_Item_Component
{
    Item_Icon,
    Item_amount,
}
/*
 * IPointerDownHandler  鼠标是否按下
 * IBeginDragHandler    鼠标是否开始拖拽
 * IEndDragHandler      鼠标是否结束拖拽
 * IDragHandler         鼠标是否拖拽
 */
public class UI_Item : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public ConfigItemData configItemData;

    private Image ItemImage;
    private TextMeshProUGUI ItemAmountText;
    private CanvasGroup canvasGroup;

    public void SetItemSprite(Sprite sprite) => ItemImage.sprite = sprite;
    public void SetitemAmountText(int amount) => ItemAmountText.text = amount <= 1 ? string.Empty : amount.ToString();
    public void SetConfigItemData(ConfigItemData configItemData)
    {
        this.configItemData = configItemData;
        SetItemSprite(Manage_Res_Sprite.Instance.Get_sprite(configItemData.iconName));
        SetitemAmountText(configItemData.amount);
    }
    private void Awake() => FindComponent();

    private void FindComponent()
    {
        ItemImage = transform.Find_Child<Image>(EUI_Item_Component.Item_Icon.ToString());
        ItemAmountText = transform.Find_Child<TextMeshProUGUI>(EUI_Item_Component.Item_amount.ToString());
        canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// 鼠标是否按下
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        //待定 拆分功能
        if (eventData.button == PointerEventData.InputButton.Right)//鼠标的点击事件是右键点击的话
        {
            //有configItemData ， 可堆叠 ， 数量大于1
            if (configItemData != null && configItemData.isStackable && configItemData.amount > 1)
            {
                if (configItemData.itemHolder.CanAddItem())
                {
                    // Can split 可以拆分
                    int splitAmount = Mathf.FloorToInt(configItemData.amount / 2f);
                    configItemData.amount -= splitAmount;
                    ConfigItemData duplicateItem 
                        = new ConfigItemData 
                        { 
                            id = configItemData.id,
                            name= configItemData.name,
                            iconName = configItemData.iconName,
                            explain = configItemData.explain,
                            isStackable = configItemData.isStackable,
                            amount = splitAmount 
                        };
                    configItemData.itemHolder.AddItem(duplicateItem);
                }
            }
        }
    }
    /// <summary>
    /// 鼠标是否开始拖拽
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .5f;
        canvasGroup.blocksRaycasts = false;
        UI_ItemDrag.Instance.Show(configItemData);
    }
    /// <summary>
    /// 鼠标是否拖拽
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {

    }
    /// <summary>
    /// 鼠标是否结束拖拽
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        UI_ItemDrag.Instance.Hide();
    }
}

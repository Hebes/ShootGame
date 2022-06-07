using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Tool;
using System;

enum EUI_ItemDrag_Component
{
    Item_Icon,
    Item_amount,
}

/// <summary>
/// 被拖拽物体的临时存储
/// </summary>
public class UI_ItemDrag : SingletonMono_Temp<UI_ItemDrag>
{
    private RectTransform parentRectTransform;
    private Image itemDragImage;
    private TextMeshProUGUI itemDragAmountText;
    [SerializeField]
    private Item item;

    public Item GetItem() => item;//获取数据
    public void SetItem(Item item) => this.item = item;//设置数据
    public void SetItemSprite(Sprite sprite) => itemDragImage.sprite = sprite;//设置图片
    public void SetitemAmountText(int amount) => itemDragAmountText.text = amount <= 1 ? string.Empty : amount.ToString();//设置文本数量
    public void IsHide(bool isHide) => gameObject.SetActive(isHide);//关闭显示
    public void Show(Item item)//显示物体并设置数据
    {
        IsHide(true);
        SetItem(item);
        SetItemSprite(Manage_Res_Sprite.Instance.Get_sprite(item.GetConfigItemData.iconName));
        SetitemAmountText(item.GetConfigItemData.amount);
        UpdatePosition();
    }

    protected override void Awake()
    {
        base.Awake();
        FindComponent();
        IsHide(false);
    }
    private void Update() => UpdatePosition();

    /// <summary>
    /// 寻找组件
    /// </summary>
    private void FindComponent()
    {
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        itemDragImage = transform.Find_Child<Image>(EUI_ItemDrag_Component.Item_Icon.ToString());
        itemDragAmountText = transform.Find_Child<TextMeshProUGUI>(EUI_ItemDrag_Component.Item_amount.ToString());
    }

    /// <summary>
    /// 更新图标位置
    /// </summary>
    private void UpdatePosition()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, Input.mousePosition, null, out Vector2 localPoint);
        transform.localPosition = localPoint;
    }
}

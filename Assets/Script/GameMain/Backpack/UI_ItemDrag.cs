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

public class UI_ItemDrag : SingletonMono_Temp<UI_ItemDrag>
{
    private RectTransform parentRectTransform;
    private Image itemDragImage;
    private TextMeshProUGUI itemDragAmountText;

    private ConfigItemData configItemData;

    public ConfigItemData GetConfigItemData() => configItemData;
    public void SetConfigItemData(ConfigItemData configItemData) => this.configItemData = configItemData;
    public void SetItemSprite(Sprite sprite) => itemDragImage.sprite = sprite;
    public void SetitemAmountText(int amount) => itemDragAmountText.text = amount <= 1 ? string.Empty : amount.ToString();
    public void Hide() => gameObject.SetActive(false);
    public void Show(ConfigItemData configItemData)
    {
        gameObject.SetActive(true);
        SetConfigItemData(configItemData);
        SetItemSprite(Manage_Res_Sprite.Instance.Get_sprite(configItemData.iconName));
        SetitemAmountText(configItemData.amount);
        UpdatePosition();
    }

    protected override void Awake()
    {
        base.Awake();
        FindComponent();
        Hide();
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

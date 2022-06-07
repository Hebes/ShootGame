using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Tool;
using UnityEngine.EventSystems;
using System;

enum EHotkeyBarItemComponent
{
    Number,
}

public class HotkeyBarItem : MonoBehaviour, IDropHandler
{
    /// <summary>
    /// 设置拖拽完毕事件
    /// </summary>
    private Action onDropAction;

    public TextMeshProUGUI GetTextMeshProUGUI => transform.Find_Child<TextMeshProUGUI>(EHotkeyBarItemComponent.Number.ToString());
    public void SetOnDropAction(Action onDropAction) => this.onDropAction = onDropAction;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("HotkeyBarItem,拖拽结束触发");
        UI_ItemDrag.Instance.IsHide(false);//关闭显示
        onDropAction();
    }
}

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
    private Action onDropAction;

    public TextMeshProUGUI GetTextMeshProUGUI => transform.Find_Child<TextMeshProUGUI>(EHotkeyBarItemComponent.Number.ToString());
    public void SetOnDropAction(Action onDropAction) => this.onDropAction = onDropAction;

    public void OnDrop(PointerEventData eventData)
    {
        UI_ItemDrag.Instance.Hide();
        onDropAction();
    }
}

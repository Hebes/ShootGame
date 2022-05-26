using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorObject : MonoBehaviour
{
    [SerializeField] private ECursorType cursorType;
    public ECursorType SetCursorType { set => cursorType = value; }
    private void OnMouseEnter() => CursorManager.Instance.SetActiveCursorType(cursorType);
    private void OnMouseExit() => CursorManager.Instance.SetActiveCursorType(ECursorType.Arrow);
}

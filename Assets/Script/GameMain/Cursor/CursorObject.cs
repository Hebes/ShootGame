using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorObject : MonoBehaviour
{
    [SerializeField] private ECursorType cursorType;
    public ECursorType SetCursorType { set => cursorType = value; }
    private void OnMouseEnter() => Manage_Cursor.Instance.SetActiveCursorType(cursorType);
    private void OnMouseExit() => Manage_Cursor.Instance.SetActiveCursorType(ECursorType.Arrow);
}

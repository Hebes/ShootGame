using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorObject : MonoBehaviour
{
    [SerializeField] private CursorType cursorType;

    private void OnMouseEnter()
    {
        CursorManager.Instance.SetActiveCursorType(cursorType);
    }
    private void OnMouseExit()
    {
        CursorManager.Instance.SetActiveCursorType(CursorType.Arrow);
    }

}

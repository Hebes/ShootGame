using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetMouseButtonDown(Config_Key.Key_Mouse_Right))
        {
            TargetSystem.Instance.AddPing(UtilsClass.GetMouseWorldPosition(), transform);
        }
        TargetSystem.Instance.Update();
    }
}

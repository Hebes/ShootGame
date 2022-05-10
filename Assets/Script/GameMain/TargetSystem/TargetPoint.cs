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
            //TargetSystem.Instance.AddPing((obj) => {
            //    obj.transform.position = UtilsClass.GetMouseWorldPosition();
            //    obj.transform.SetParent(transform);
            //    UI_TargetWindow.Instance.AddPing(obj.transform.position);
            //});

            TargetSystem.Instance.AddPing(
                new TargetSystem.Ping(TargetSystem.Ping.Type.Move,UtilsClass.GetMouseWorldPosition()),transform);
        }

        if (Input.GetMouseButtonDown(2))
        {
            TargetSystem.Instance.AddPing(
                new TargetSystem.Ping(TargetSystem.Ping.Type.Enemy, UtilsClass.GetMouseWorldPosition()), transform);
        }
    }
}

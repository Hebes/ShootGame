using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Tool;

/// <summary>
/// 加载TargetUI的脚本代码
/// </summary>
public class UI_TargetWindow : SingletonMono_Temp<UI_TargetWindow>
{
    public void AddPing(TargetSystem.Ping ping)
    {
        GameObject gameObject = Manage_Res_pf.Instance.GetAndInstantiate("UI_Target", transform);
        gameObject.transform.GetComponent<UI_TargetUI>().Ping(ping);//设置TargetUI位置
    }

}

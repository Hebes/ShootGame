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
        GameObject gameObject = MainGame_Res_pf_Manage.Instance.GetAndInstantiate("TargetUI", transform);
        gameObject.transform.GetComponent<UI_TargetUI>().Ping(ping);//设置TargetUI位置
    }

}

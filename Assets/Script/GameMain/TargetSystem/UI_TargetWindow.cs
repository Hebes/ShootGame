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
        ResMgr.Instance.LoadResAysn<GameObject>(Config_ResLoadPaths.targetSystem_UI_TargetUI, (obj) =>//加载并创建
        {
            obj.transform.SetParent(transform);//设置TargetUI父物体
            obj.transform.GetComponent<UI_TargetWindow_TargetUI>().Ping(ping);//设置TargetUI位置
        });
    }

}

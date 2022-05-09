using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摄像机的表现效果
/// </summary>
public class Camera_Effect : MonoBehaviour
{

    private void Awake()
    {
        EventCenter.Instance.AddEventListener<OnShootEvnentArgs>(Config_Player.player_Event_Shoot, PlayerShootOverEffer);
    }

    /// <summary>
    /// 玩家射击完毕后需要处理的事件
    /// </summary>
    private void PlayerShootOverEffer(OnShootEvnentArgs onShootEvnentArgs)
    {
        //屏幕抖动
        UtilsClass.ShakeCamera(.5f, .05f);
    }
}

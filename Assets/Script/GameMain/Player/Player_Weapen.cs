using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 枪口射击射线事件
/// </summary>
public class OnShootEvnentArgs
{
    public Vector3 gunEndPointPosition;
    public Vector3 shootPosition;
    public Vector3 shellPosition;
}
public class Player_Weapen : MonoBehaviour
{
    /// <summary>
    /// 用来添加枪射击完毕后需要处理的事情，比如屏幕抖动
    /// </summary>
    //public event EventHandler<OnShootEvnentArgs> OnShoot;

    private OnShootEvnentArgs onShootEvnentArgs;

    void Update()
    {
        HandleAiming();
        HandleShooting();
    }

    /// <summary>
    /// 射击的方法
    /// </summary>
    private void HandleShooting()
    {
        //TUDO 在安卓手机中可能不能运行
        //以下方法适用PC
        if (Input.GetMouseButtonDown(0))
        {
            //播放枪械攻击动击动画
            Player_Manager.Instance.Player_Gun_PlayerGun_Animator.SetTrigger(Config_Animator.gun_Trigger_Animator_Shoot);//枪械左边攻

            EventCenter.Instance.EventTrigger<OnShootEvnentArgs>(Config_Player.player_Event_Shoot, onShootEvnentArgs);

            //OnShoot?.Invoke(this, new OnShootEvnentArgs
            //{
            //    //gunEndPointPosition = gunEndPointTransform.position,
            //    //shootPosition = mousePosition,
            //    //shellPosition = shellPositionTransform.position
            //});
        }
    }

    /// <summary>
    /// 处理瞄准目标的方法
    /// </summary>
    private void HandleAiming()
    {
        ////设置枪械变换朝向
        if (MouseRightOrLeft(out float angle, out Vector3 mousePos))
        {
            Player_Manager.Instance.Player_Gun_PlayerGun_Transform.localScale = new Vector2(-1, -1);
            Player_Manager.Instance.Player_Transform.localScale = new Vector2(-1, 1);
        }
        else
            Player_Manager.Instance.Player_Transform.localScale =
                Player_Manager.Instance.Player_Gun_PlayerGun_Transform.localScale = Vector2.one;

        Player_Manager.Instance.Player_Gun_PlayerGun_Transform.eulerAngles = new Vector3(0, 0, angle);
    }

    /// <summary>
    /// 判断鼠标是左边还是右边
    /// </summary>
    /// <returns></returns>
    private bool MouseRightOrLeft(out float vector, out Vector3 mousePos)
    {
        //鼠标位置
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

        Vector3 aimDir = mousePos = (mousePosition - Player_Manager.Instance.Player_Transform.position).normalized;
        float angle = vector = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;

        if (angle > 90 || angle < -90) return true;
        else return false;
    }
}

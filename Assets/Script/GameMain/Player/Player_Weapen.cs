using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;
/// <summary>
/// 枪口射击射线事件
/// Aim at Mouse in Unity 2D (Shoot Weapon, Unity Tutorial for Beginners)
/// 在 Unity 2D 中瞄准鼠标（射击武器，Unity 初学者教程）
/// 参考：https://www.youtube.com/watch?v=fuGQFdhSPg4&t=527s
/// </summary>
public class OnShootEvnentArgs
{
    public Transform tfShootPoint;
    public Transform tfGunEndPoint;
    public Vector3 shellPosition;
}
/// <summary>
/// 武器朝向
/// </summary>
public class Player_Weapen : MonoBehaviour
{
    /// <summary>
    /// 用来添加枪射击完毕后需要处理的事情，比如屏幕抖动
    /// </summary>
    private OnShootEvnentArgs onShootEvnentArgs;

    private float shootTimer;
    private bool canShoot = true;
    private Player_Components player_Components;

    private void Awake()
    {
        player_Components = GetComponent<Player_Components>();
    }

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
        if (canShoot && Input.GetMouseButton(Config_Key.Key_Mouse_Left))
        {
            // Shoot
            canShoot = false;

            //播放枪械攻击动击动画
            player_Components.Player_Gun_PlayerGun_Animator.SetTrigger(Config_Animator.gun_Trigger_Animator_Shoot);

            //发布攻击事件
            EventCenter.Instance.EventTrigger(Config_Player.player_Event_Shoot, new OnShootEvnentArgs
            {
                tfGunEndPoint = player_Components.Player_Gun_PlayerGun_EndPoint,
                tfShootPoint = player_Components.Player_Gun_PlayerGun_ShootPoint,
            });

            StartCoroutine(enumerator());//开火间隔
        }
    }
    //武器的开火间隔
    IEnumerator enumerator()
    {
        yield return new WaitForSeconds(0.1f);
        canShoot = true;
    }

    /// <summary>
    /// 处理瞄准目标的方法
    /// </summary>
    private void HandleAiming()
    {
        ////设置枪械变换朝向
        if (MouseRightOrLeft(out float angle, out Vector3 mousePos))
        {
            player_Components.Player_Gun_PlayerGun_Transform.localScale = new Vector2(-1, -1);//枪旋转

            player_Components.Player_Hp_PlayerHPBar.localScale = //血条旋转
               player_Components.Player_Transform.localScale = new Vector2(-1, 1);//玩家旋转

        }
        else
            player_Components.Player_Hp_PlayerHPBar.localScale =
                player_Components.Player_Transform.localScale =
                player_Components.Player_Gun_PlayerGun_Transform.localScale = Vector2.one;
        player_Components.Player_Gun_PlayerGun_Transform.eulerAngles = new Vector3(0, 0, angle);
    }

    /// <summary>
    /// 判断鼠标是左边还是右边
    /// </summary>
    /// <returns></returns>
    private bool MouseRightOrLeft(out float vector, out Vector3 mousePos)
    {
        //鼠标在屏幕的位置
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

        Vector3 aimDir = mousePos = (mousePosition - player_Components.Player_Transform.position).normalized;
        float angle = vector = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;

        return (angle > 90 || angle < -90) ? true : false;
    }
    /// <summary>
    /// 射击间隔
    /// </summary>
    private bool ShootTimer
    {
        get
        {
            if (shootTimer >= .15f)
            {
                shootTimer = 0;
                return true;
            }
            else
            {
                shootTimer += Time.deltaTime;
                return false;
            }
        }
    }
}

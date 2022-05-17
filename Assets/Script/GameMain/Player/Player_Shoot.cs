using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

/// <summary>
/// 枪械射击脚本
/// 参考：https://www.youtube.com/watch?v=Nke5JKPiQTw  3 Ways to Shoot Projectiles in Unity! Unity中的3种射击投射物的方法!
/// </summary>
public class Player_Shoot : MonoBehaviour
{
    private delegate void ShootActionDelegate(Transform gunEndPointPosition, Transform shootPosition);
    private ShootActionDelegate shootAction;

    private void Awake()
    {
        shootAction = ShootPhysics;
        EventCenter.Instance.AddEventListener<OnShootEvnentArgs>(Config_Event.Player_Shoot, ShootingOverTrigger);
    }

    private void Update()
    {   
        if (Input.GetKeyDown(Config_Key.key_T)) shootAction = ShootTransform;
        if (Input.GetKeyDown(Config_Key.key_Y)) shootAction = ShootPhysics;
        if (Input.GetKeyDown(Config_Key.key_U)) shootAction = ShootRaycast;
    }
    /// <summary>
    /// 射击完毕后触发
    /// </summary>
    /// <param name="arg0"></param>
    private void ShootingOverTrigger(OnShootEvnentArgs e)
    {
        shootAction(e.tfGunEndPoint, e.tfShootPoint);

        #region 第一种射击方法 
        ////池子中加载物体
        //PoolMgr.Instance.GetObj(Config_ResLoadPaths.bullent_pf_Gun_Bullet, (obj) =>
        //{
        //    //以下为开火后调用的方法
        //    obj.transform.position = e.tfShootPoint.position;//开火点
        //    Vector3 shootDir = (e.tfShootPoint.position - e.tfGunEndPoint.position).normalized;
        //    obj.GetComponent<Bullet_Common>().Setup(shootDir);
        //});
        #endregion

        #region 第二种射击方法 子弹基于物理学
        //PoolMgr.Instance.GetObj(Config_ResLoadPaths.bullent_pf_Gun_BulletPhysics, (obj) =>
        //{
        //    //以下为开火后调用的方法
        //    obj.transform.position = e.tfShootPoint.position;//开火点
        //    Vector3 shootDir = (e.tfShootPoint.position - e.tfGunEndPoint.position).normalized;
        //    obj.GetComponent<Bullet_Physics>().Setup(shootDir);
        //});
        #endregion

        #region 第三种射击方法 射线射击
        //Vector3 shootDir = (e.tfShootPoint.position - e.tfGunEndPoint.position).normalized;
        //Bullet_Raycast.Instance.Shoot(e.tfGunEndPoint.position, shootDir);
        //#endregion

        //WeaponTracer.Create(e.tfGunEndPoint, UtilsClass.GetMouseWorldPosition());//鼠标射线
        #endregion
    }
    //第一种射击方法
    private void ShootTransform(Transform tfGunEndPoint, Transform tfShootPoint)
    {
        //池子中加载物体
        PoolMgr.Instance.GetObj(Config_ResLoadPaths.bullent_pf_Gun_Bullet, (obj) =>
        {
            //以下为开火后调用的方法
            obj.transform.position = tfShootPoint.position;//开火点
            Vector3 shootDir = (tfShootPoint.position - tfGunEndPoint.position).normalized;
            obj.GetComponent<IBulletSetup>().Setup(shootDir);
        });
    }

    //第二种射击方法 子弹基于物理学
    private void ShootPhysics(Transform tfGunEndPoint, Transform tfShootPoint)
    {
        #region 第二种射击方法 子弹基于物理学
        PoolMgr.Instance.GetObj(Config_ResLoadPaths.bullent_pf_Gun_BulletPhysics, (obj) =>
        {
            //以下为开火后调用的方法
            obj.transform.position = tfShootPoint.position;//开火点 子弹生成的位置
            Vector3 shootDir = (tfShootPoint.position - tfGunEndPoint.position).normalized;//子弹的朝向
            obj.GetComponent<IBulletSetup>().Setup(shootDir);
        });
        #endregion
    }

    //第三种射击方法 射线射击
    private void ShootRaycast(Transform tfGunEndPoint, Transform tfShootPoint)
    {
        #region 第三种射击方法 射线射击
        Vector3 shootDir = (tfShootPoint.position - tfGunEndPoint.position).normalized;
        Debug.Log("射线距离：" + shootDir);
        Bullet_Raycast.Instance.Shoot(tfGunEndPoint.position, shootDir);
        #endregion
    }
}

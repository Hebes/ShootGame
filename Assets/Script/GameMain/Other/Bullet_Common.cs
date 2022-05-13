using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.Events;
using System;

/// <summary>
/// 子弹脚本
/// 参考视频：https://www.youtube.com/watch?v=Nke5JKPiQTw
/// </summary>
public class Bullet_Common : MonoBehaviour, IBullet
{
    
    private Vector3 shootDir;
    private float moveSpeed = 100f;
    ///// <summary>
    ///// 命中检测大小
    ///// </summary>
    //private float hitDeteCtionSize = 4f;

    /// <summary>
    /// 设置
    /// </summary>
    /// <param name="shootdir">射击方向</param>
    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir));//子弹生成的朝向
    }

    private void Update()
    {
        transform.position += shootDir * Time.deltaTime * moveSpeed;//子弹移动

        #region 第一种Damage方式 利用Vector3和范围检测的给与伤害 缺点：如果大量会造成性能问题
        //GunTarget gunTarget = GunTarget.GetClosest(transform.position, hitDeteCtionSize);
        //if (gunTarget != null)
        //{
        //    gunTarget.Damage();
        //    StartCoroutine(Push(() => { Push(); }, 0));
        //}
        #endregion
        StartCoroutine(Push(() => { Push(); }, 2));
    }
   

    

    #region 第二种Damage方式 碰撞体  如需使用第一种 请注释以下
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GunTarget gunTarget = collision.GetComponent<GunTarget>();
        if (gunTarget != null)
        {
            gunTarget.Damage();
            StartCoroutine(Push(() => { Push(); }, 0));
        }
    }
    #endregion


    /// <summary>
    /// 销毁子弹到对象池
    /// </summary>
    IEnumerator Push(Action action, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        action?.Invoke();
    }

    private void Push()
    {
        PoolMgr.Instance.PushObj(this.gameObject.name, this.gameObject);
    }
}

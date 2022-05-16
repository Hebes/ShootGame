using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹物理学
/// </summary>
public class Bullet_Physics : MonoBehaviour, IBulletSetup
{
    #region 第二种子弹运动方式  子弹也是基于物理学
    private float moveSpeed = 100f;

    public void Setup(Vector3 shootDir)
    {
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(shootDir * moveSpeed, ForceMode2D.Impulse);//添加推力 和 Bullet_Common 的区别

        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir));//子弹生成的朝向

        StartCoroutine(Push(() => { Push(); }, 2));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GunTarget gunTarget = collision.GetComponent<GunTarget>();
        if (gunTarget != null)
        {
            gunTarget.Damage(300);
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

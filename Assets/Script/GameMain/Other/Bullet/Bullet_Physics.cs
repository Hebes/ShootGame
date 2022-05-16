using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using Tool;
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
        rigidbody2D.AddForce(shootDir * moveSpeed, ForceMode2D.Impulse);//添加推力 和 Bullet_Common 移动的区别

        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir));//子弹生成的朝向

        StartCoroutine(Push(() => { Push(); }, 2));
    }

    private void Update() => StartCoroutine(Push(() => { Push(); }, 2));

    private void OnTriggerEnter2D(Collider2D collision)
    {

        ICommonCollide target = collision.GetComponent<ICommonCollide>();
        if (target != null)
        {
            // Hit enemy 敌人伤害
            int damageAmount = UnityEngine.Random.Range(100, 200);//随机伤害
            bool isCritical = UnityEngine.Random.Range(0, 100) < 30;//是否重击
            if (isCritical) damageAmount *= 2;//重击伤害*2

            target.Damage(damageAmount);
            StartCoroutine(Push(() => { Push(); }, 0));

            //显示伤害文字效果
            Component_Helper.Show_pf_Damage(collision.transform.position, damageAmount, isCritical);
            //加载特效
            Component_Helper.LoadEffect(Config_ResLoadPaths.Gun_pf_Effect, collision.transform.position);
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

    private void Push() => PoolMgr.Instance.PushObj(gameObject.name, gameObject);
}

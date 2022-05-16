using CodeMonkey.Utils;
using System;
using System.Collections;
using Tool;
using UnityEngine;

/// <summary>
/// 子弹脚本
/// 参考视频：https://www.youtube.com/watch?v=Nke5JKPiQTw
/// </summary>
public class Bullet_Common : MonoBehaviour, IBulletSetup
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
        //没有碰撞到物体的在外面飞的子弹的销毁
        StartCoroutine(Push(() => { Push(); }, 2));
    }




    #region 第二种Damage方式 碰撞体  如需使用第一种 请注释以下
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICommonCollide gunTarget = collision.GetComponent<ICommonCollide>();
        if (gunTarget != null)
        {
            
            // Hit enemy 敌人伤害
            int damageAmount = UnityEngine.Random.Range(100, 200);//随机伤害
            bool isCritical = UnityEngine.Random.Range(0, 100) < 30;//是否重击
            if (isCritical) damageAmount *= 2;//重击伤害*2

            gunTarget.Damage(damageAmount);
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

    private void Push()
    {
        PoolMgr.Instance.PushObj(this.gameObject.name, this.gameObject);
    }
}

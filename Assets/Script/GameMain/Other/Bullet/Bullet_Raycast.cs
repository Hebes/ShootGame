using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;

public class Bullet_Raycast : BaseManager<Bullet_Raycast>
{
    public void Shoot(Vector3 shootPosition, Vector3 shootDirection)
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(shootPosition, shootDirection);//发射点   发射距离
        if (raycastHit2D.collider != null)
        {
            // Hit!
            ICommonCollide target = raycastHit2D.collider.GetComponent<ICommonCollide>();
            if (target != null)
            {
                Debug.Log("ShowMessage:" + target);
                // Hit enemy 敌人伤害
                int damageAmount = UnityEngine.Random.Range(100, 200);//随机伤害
                bool isCritical = UnityEngine.Random.Range(0, 100) < 30;//是否重击
                if (isCritical) damageAmount *= 2;//重击伤害*2

                // Deal damage 处理伤害
                //TUDO　显示位置错误
                target.Damage(damageAmount);//给与伤害

                Component_Helper.Show_pf_Damage(UtilsClass.GetMouseWorldPosition(), damageAmount, isCritical);//显示伤害文字效果

                //加载特效
                Component_Helper.LoadEffect(Config_ResLoadPaths.Gun_pf_Effect, UtilsClass.GetMouseWorldPosition());
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;

public class Enemy_HP : Enemy_Components, ICommonCollide
{
    private Bar_HealthSystem enemy_HealthSystem;

    private void Awake()
    {
        enemy_HealthSystem = new Bar_HealthSystem(10000);//初始化气血
        transform.Find_Child<Bar_Health>(Config_Common.HP_pf_BarHP).Setup(enemy_HealthSystem);
    }

    public void Damage(int damageAmount)
    {
        //血条减少
        enemy_HealthSystem.Damage(damageAmount);//血条扣血
    }
}

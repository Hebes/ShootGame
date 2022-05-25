using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;

public class Enemy_HP : MonoBehaviour, ICommonCollide
{
    private Bar_HealthSystem enemy_HealthSystem;

    private void Awake()
    {
        enemy_HealthSystem = new Bar_HealthSystem(10000);//初始化气血
        transform.Find_Child<Bar_Health>(Config_Common.HP_pf_BarHP).Setup(enemy_HealthSystem);
    }

    //血条减少
    public void Damage(int damageAmount) => enemy_HealthSystem.Damage(damageAmount);//血条扣血
}

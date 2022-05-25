using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;

public class Enemy_HP : MonoBehaviour, ICommonCollide
{
    private Enemy_Components enemy_Components;
    private Bar_HealthSystem enemy_HealthSystem;
    private void Awake()
    {
        //enemy_Components = GetComponent<Enemy_Components>();
        enemy_HealthSystem = new Bar_HealthSystem(10000);
        transform.Find_Child<Bar_Health>("Bar_HP").Setup(enemy_HealthSystem);//初始化气血
    }

    public void Damage(int damageAmount) => enemy_HealthSystem.Damage(damageAmount);//血条扣血
}

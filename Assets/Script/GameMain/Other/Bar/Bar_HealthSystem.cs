using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar_HealthSystem : BaseManager<Bar_HealthSystem>
{
    /// <summary>
    /// 当前健康值
    /// </summary>
    private int health;
    /// <summary>
    /// 最大健康值
    /// </summary>
    private int healthMax;

    /// <summary>
    /// 获取当前的血量比例
    /// </summary>
    /// <returns></returns>
    public float GetHealthPercent
    {
        get { return (float)health / healthMax; }
    }

    /// <summary>
    /// 初始化设置气血值
    /// </summary>
    /// <param name="healthMax"></param>
    public void InitSetHealth(int healthMax)
    {
        health = healthMax;
        this.healthMax = healthMax;
    }

    

    /// <summary>
    /// 扣血
    /// </summary>
    /// <param name="damageAmount"></param>
    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health < 0) health = 0;

        EventCenter.Instance.EventTrigger(Config_Common.EventName_HPEvent);//执行监听方法
    }

    /// <summary>
    /// 加血
    /// </summary>
    /// <param name="healAmount"></param>
    public void Heal(int healAmount)
    {
        health += healAmount;

        if (health > healthMax) health = healthMax;

        EventCenter.Instance.EventTrigger(Config_Common.EventName_HPEvent);//执行监听方法
    }
}

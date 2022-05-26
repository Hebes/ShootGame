using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 血条系统
/// How to make a Health System | Unity Tutorial
/// 参考：https://www.youtube.com/watch?v=0T5ei9jN63M&list=PLzDRvYVwl53trtRTXjUS-ot2uRKaJtLy3&index=15
/// </summary>
public class Bar_HealthSystem
{
    /// <summary>
    /// 血量变化触发的事件
    /// </summary>
    public event EventHandler OnHpChanged;
    /// <summary>
    /// 死亡触发的事件
    /// </summary>
    public event EventHandler OnDead;
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
    public float GetHealthPercent => (float)health / healthMax;

    /// <summary>
    /// 初始化设置气血值
    /// </summary>
    /// <param name="healthMax"></param>
    public Bar_HealthSystem(int healthMax)
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
        if (health < 0)
        {
            health = 0;
            Die();
        }

        OnHpChanged?.Invoke(this, EventArgs.Empty);//扣血之后触发的事件
    }

    private void Die() => OnDead?.Invoke(this, EventArgs.Empty);

    /// <summary>
    /// 加血
    /// </summary>
    /// <param name="healAmount"></param>
    public void Heal(int healAmount)
    {
        health += healAmount;

        if (health > healthMax) health = healthMax;

        OnHpChanged?.Invoke(this, EventArgs.Empty);//加血之后触发的事件
    }
}

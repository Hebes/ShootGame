using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;
using System;

public class Bar_Health : MonoBehaviour
{
    private Bar_HealthSystem bar_HealthSystem;

    public void Setup(Bar_HealthSystem bar_HealthSystem)
    {
        this.bar_HealthSystem = bar_HealthSystem;
        bar_HealthSystem.OnHpChanged += Bar_HealthSystem_OnHealthChange;
        UpdateHealthBar();
    }

    /// <summary>
    /// 设置气血条的比例
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Bar_HealthSystem_OnHealthChange(object sender, EventArgs e)
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        transform.Find(Config_Common.HP_pf_Bar).localScale = new Vector2(bar_HealthSystem.GetHealthPercent, 1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;

public class Bar_Health : MonoBehaviour
{
    private Bar_HealthSystem bar_HealthSystem;

    public void Setup(Bar_HealthSystem bar_HealthSystem)
    {
        this.bar_HealthSystem = bar_HealthSystem;
        EventCenter.Instance.AddEventListener(Config_Common.EventName_HPEvent, Bar_HealthSystem_OnHealthChange);
    }

    /// <summary>
    /// 设置气血条的比例
    /// </summary>
    private void Bar_HealthSystem_OnHealthChange()
    {
        Player_Manager.Instance.Player_Hp_Bar.localScale= new Vector3(bar_HealthSystem.GetHealthPercent, 1f, 1f);
    }
}

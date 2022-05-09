using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家健康值-气血
/// </summary>
public class Player_Helth : MonoBehaviour
{
    private Bar_HealthSystem bar_HealthSystem;
    private Bar_Health bar_Health;

    private void Awake()
    {
        bar_HealthSystem = Bar_HealthSystem.Instance;
        bar_Health = Player_Manager.Instance.Player_Hp_BarHealth;
    }

    private void Start()
    {
        bar_HealthSystem.InitSetHealth(100);
        bar_Health.Setup(bar_HealthSystem);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            bar_HealthSystem.Damage(10);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            bar_HealthSystem.Heal(10);
        }
    }
}

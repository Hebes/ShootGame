using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Update : MonoBehaviour
{
    private Enemy_Components enemy_Components;
    private void Awake() => enemy_Components = GetComponent<Enemy_Components>();

    // Update is called once per frame
    void Update()
    {
        enemy_Components.Update_Enemy_Talk.Update_Enemy_Talk(enemy_Components);//敌人说话驱动
        enemy_Components.Update_Enemy_FSM.Update_Enemy_FSM();//敌人状态机驱动
        enemy_Components.Update_TargetingSystem_PhysicsOverlap.Update_TargetingSystem_PhysicsOverlap(enemy_Components);//检测敌人驱动
    }
}

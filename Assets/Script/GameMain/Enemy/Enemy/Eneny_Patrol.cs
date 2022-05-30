using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 巡逻状态
/// </summary>
public class Eneny_Patrol : FSMState
{
    private Enemy_Components enemy_Components;

    private int moveSpeed = 10;
    private float m_Timer;
    private bool isMove = true;

    private Vector3 pos;
    private Vector3 body_localScale;

    public Eneny_Patrol(FSMSystem fsm) : base(fsm)
    {
        enemy_Components = m_FSM.OwnerGo.GetComponent<Enemy_Components>();
        body_localScale = enemy_Components.Enemy_Body.localScale;
    }

    public override void Action()
    {
        m_Timer += Time.deltaTime;

        if (m_Timer > 2)
        {
            if (isMove)
            {
                pos = Move_v3().normalized * 2;
                isMove = false;
            }

            enemy_Components.Eneny_Transform.position += pos * moveSpeed * Time.deltaTime;
            enemy_Components.Enemy_Animator.SetBool("Idle_Move", true);
        }

        if (m_Timer > 4)
        {
            isMove = true;
            m_Timer = 0;
            enemy_Components.Enemy_Animator.SetBool("Idle_Move", false);
        }

    }

    public override void Check()
    {

    }

    private Vector3 Move_v3()
    {
        int x= Random.Range(-50, 50);
        int y= Random.Range(-30, 30);
        enemy_Components.Enemy_Body.localScale= x < 0? new Vector3(-body_localScale.x, body_localScale.y):new Vector3(body_localScale.x, body_localScale.y); 
        return new Vector3(x, y);
    }
}

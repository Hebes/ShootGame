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

    private enum Enemy_Orientation
    {
        Up = 0,//上
        Down,//下
        Left,//左
        Right,//右
        LeftUp,//左上
        LeftDown,//左下
        RightUp,//右上
        RightDown,//右下
    }

    public override void Action()
    {
        m_Timer += Time.deltaTime;

        if (m_Timer > 2)
        {
            if (isMove)
            {
                pos = Move_v3();
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
        //设置下一次移动的方位
        switch ((Enemy_Orientation)Random.Range(0, (int)Enemy_Orientation.RightDown))
        {
            default:
            case Enemy_Orientation.Up:
                enemy_Components.Enemy_Body.localScale = new Vector3(body_localScale.x, body_localScale.y);
                return new Vector3(0, 1).normalized;
            case Enemy_Orientation.Down:
                enemy_Components.Enemy_Body.localScale = new Vector3(body_localScale.x, body_localScale.y);
                return new Vector3(0, -1).normalized;
            case Enemy_Orientation.Left:
                enemy_Components.Enemy_Body.localScale = new Vector3(-body_localScale.x, body_localScale.y);
                return new Vector3(-1, 0).normalized;
            case Enemy_Orientation.Right:
                enemy_Components.Enemy_Body.localScale = new Vector3(body_localScale.x, body_localScale.y);
                return new Vector3(1, 0).normalized;
            case Enemy_Orientation.LeftUp:
                enemy_Components.Enemy_Body.localScale = new Vector3(-body_localScale.x, body_localScale.y);
                return new Vector3(-1, 1).normalized;
            case Enemy_Orientation.LeftDown:
                enemy_Components.Enemy_Body.localScale = new Vector3(-body_localScale.x, body_localScale.y);
                return new Vector3(-1, -1).normalized;
            case Enemy_Orientation.RightUp:
                enemy_Components.Enemy_Body.localScale = new Vector3(body_localScale.x, body_localScale.y);
                return new Vector3(1, 1).normalized;
            case Enemy_Orientation.RightDown:
                enemy_Components.Enemy_Body.localScale = new Vector3(body_localScale.x, body_localScale.y);
                return new Vector3(1, -1).normalized;
        }
    }
}

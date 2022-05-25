using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FSM : MonoBehaviour
{
    // 有限状态机
    private FSMSystem m_FSM = null;

    private void Start()
    {
        // 实例化有限状态机
        m_FSM = new FSMSystem(gameObject);
        // 添加状态到有限状态机中
        m_FSM.AddState(StateID.Patrol, new Eneny_Patrol(m_FSM));
        // m_FSM.AddState(StateID.Rotate, new RotateState(m_FSM));
    }


    // 调用有限状态机中的Update方法
    private void Update() => m_FSM?.Update();
}

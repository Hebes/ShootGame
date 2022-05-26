using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;
using System;

public enum EEnemy_Component
{
    Enemy_Body,
    Enemy_ChatBubble,
    Bar_HP,
}

/// <summary>
/// 组件管理类
/// </summary>
public class Enemy_Components : CursorObject
{

    private Animator enemy_Animator;
    private Transform enemy_Body;
    private Transform enemy_ChatBubble;
    private Bar_Health enemy_HP;
    //[SerializeField]
    private Transform enemy_Target;

    private void Awake()
    {
        enemy_Animator = transform.Find_Child<Animator>(EEnemy_Component.Enemy_Body.ToString());
        enemy_Body = transform.Find_Child<Transform>(EEnemy_Component.Enemy_Body.ToString());
        enemy_ChatBubble = transform.Find_Child<Transform>(EEnemy_Component.Enemy_ChatBubble.ToString());//用来冒泡聊天的
        enemy_HP = transform.Find_Child<Bar_Health>(EEnemy_Component.Bar_HP.ToString());

        SetCursorType = ECursorType.Attack;
    }

    public Animator Enemy_Animator => enemy_Animator;
    public Transform Eneny_Transform => transform;
    public Transform Enemy_Body => enemy_Body;
    public Transform Enemy_ChatBubble => enemy_ChatBubble;
    public Bar_Health Enemy_HP => enemy_HP;
    public Transform Enemy_Target { set { enemy_Target = value; } private get { return enemy_Target; } }
}

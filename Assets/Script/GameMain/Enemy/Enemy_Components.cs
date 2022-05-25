using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;
using System;

public enum Enemy_Component
{
    Enemy_Body,
    Enemy_ChatBubble,
    Bar_HP,
}

/// <summary>
/// 组件管理类
/// </summary>
public class Enemy_Components : MonoBehaviour
{

    private Animator enemy_Animator;
    private Transform enemy_Body;
    private Transform enemy_ChatBubble;
    private Bar_Health enemy_HP;

    private void Awake()
    {
        enemy_Animator = transform.Find_Child<Animator>(Enemy_Component.Enemy_Body.ToString());
        enemy_Body = transform.Find_Child<Transform>(Enemy_Component.Enemy_Body.ToString());
        enemy_ChatBubble = transform.Find_Child<Transform>(Enemy_Component.Enemy_ChatBubble.ToString());//用来冒泡聊天的
        enemy_HP = transform.Find_Child<Bar_Health>(Enemy_Component.Bar_HP.ToString());
    }

    public Animator Enemy_Animator => enemy_Animator;
    public Transform Eneny_Transform => transform;
    public Transform Enemy_Body => enemy_Body;
    public Transform Enemy_ChatBubble => enemy_ChatBubble;
    public Bar_Health Enemy_HP => enemy_HP;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;
using System;

/// <summary>
/// 组件管理类
/// </summary>
public class Enemy_Components : MonoBehaviour
{

    private Bar_Health enemy_HPBar;

    private void Awake()
    {
        //enemy_HPBar = transform.Find_Child<Bar_Health>(Config_Common.HPbar_pf_BarHP);
    }

    protected Bar_Health Enemy_HPBar
    {
        get { return enemy_HPBar; }
    }
}

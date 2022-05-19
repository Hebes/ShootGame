using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;

public class Player_Components : MonoBehaviour
{
    //玩家本体
    private Animator player_Animator;
    private Rigidbody2D player_Rigidbody2D;
    private Animator player_MiniMap_Animator;
    private Transform player_Rotation;

    //玩家枪
    private Transform player_Gun_EndPoint;
    private Transform player_Gun_ShootPoint;
    private Transform player_Gun_Transform;
    private Animator player_Gun_Animator;

    // 玩家血条
    private Transform player_HP_HPBar;

    //玩家冒泡框
    private Transform player_ChatBubble_transform;

    private void Awake() => Player_GetComponent();

    /// <summary>
    /// 获取玩家组件
    /// </summary>
    private void Player_GetComponent()
    {
        //一个物体获取单个组件
        player_Rotation = transform.Find_Child<Transform>("Rotation");
        player_Animator = transform.Find_Child<Animator>("Player_Body");
        player_Rigidbody2D = transform.GetComponent<Rigidbody2D>();
        player_MiniMap_Animator = transform.Find_Child<Animator>("MiniMap");

        player_Gun_EndPoint = transform.Find_Child<Transform>("Gun_EndPoint");
        player_Gun_ShootPoint = transform.Find_Child<Transform>("Gun_ShootPoint");

        player_HP_HPBar = transform.Find_Child<Transform>("Player_HPBar");

        player_ChatBubble_transform = transform.Find_Child<Transform>("Player_ChatBubble");

        //一个物体获取多个组件
        player_Gun_Transform = transform.Find_Child<Transform>("Player_Gun");
        player_Gun_Animator = transform.Find_Child<Animator>("Player_Gun");
    }



    public Animator Player_Animator => player_Animator;
    public Rigidbody2D Player_Rigidbody2D => player_Rigidbody2D;
    public Transform Player_Transform => transform;
    public Animator Player_MiniMap_Animator => player_MiniMap_Animator;
    public Transform Player_Gun_Transform => player_Gun_Transform;
    public Animator Player_Gun_Animator => player_Gun_Animator;
    public Transform Player_HP_HPBar => player_HP_HPBar;
    public Transform Player_Gun_EndPoint => player_Gun_EndPoint;
    public Transform Player_Gun_ShootPoint => player_Gun_ShootPoint;
    public Transform Player_ChatBubble_transform => player_ChatBubble_transform;
    public Transform Player_Rotation => player_Rotation;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;

public class Player_Manager : SingletonMono<Player_Manager>
{
    private Animator player_Animator;
    private Rigidbody2D player_Rigidbody2D;
    private Transform player_Transform;//有刚体的物体才能移动


    protected override void Init_Awake()
    {
        base.Init_Awake();
        player_Animator = transform.Find_Child<Animator>(Config_Player.player_Prefab_Body);
        player_Rigidbody2D = transform.Find_Child<Rigidbody2D>(Config_Player.player_Prefab_Body);
        player_Transform = transform.Find_Child_Transform(Config_Player.player_Prefab_Body);
    }

    public Animator Player_Animator
    {
        get { return player_Animator; }
    }
    public Rigidbody2D Player_Rigidbody2D
    {
        get { return player_Rigidbody2D; }
    }
    public Transform Player_Position
    {
        get { return player_Transform; }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;

public class Player_Manager : SingletonMono<Player_Manager>
{
    #region Player_Body
    private Animator player_Animator;
    private Rigidbody2D player_Rigidbody2D;
    private Animator player_MiniMapIcon_Animator;
    #endregion

    #region Player_Gun
    private Transform player_Gun_PlayerGun_Transform;
    private Animator player_Gun_PlayerGun_Animator;
    #endregion


    protected override void Init_Awake()
    {
        base.Init_Awake();

        player_Animator = transform.Find_Child<Animator>(Config_Player.player_Prefab_Body);
        player_Rigidbody2D = transform.GetComponent<Rigidbody2D>();
        player_MiniMapIcon_Animator = transform.Find_Child<Animator>(Config_Player.player_Prefab_MiniMap);

        player_Gun_PlayerGun_Transform = transform.Find_Child<Transform>(Config_Player.player_Prefab_Gun_PlayerGun);
        player_Gun_PlayerGun_Animator = transform.Find_Child<Animator>(Config_Player.player_Prefab_Gun_PlayerGun);
    }

    public Animator Player_Animator
    {
        get { return player_Animator; }
    }
    public Rigidbody2D Player_Rigidbody2D
    {
        get { return player_Rigidbody2D; }
    }
    public Transform Player_Transform
    {
        get { return transform; }
    }
    public Animator Player_MiniMapIcon_Animator
    {
        get { return player_MiniMapIcon_Animator; }
    }
    public Transform Player_Gun_PlayerGun_Transform
    {
        set {  player_Gun_PlayerGun_Transform = value; }
        get { return player_Gun_PlayerGun_Transform; }
    }
    public Animator Player_Gun_PlayerGun_Animator
    {
        get { return player_Gun_PlayerGun_Animator; }
    }
}

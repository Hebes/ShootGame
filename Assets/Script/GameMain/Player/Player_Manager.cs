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
    private Transform player_Gun_PlayerGun_EndPoint;
    private Transform player_Gun_PlayerGun_ShootPoint;
    private Animator player_Gun_PlayerGun_Animator;
    #endregion

    #region Player_HP
    private Transform player_Hp_Bar;
    private Bar_Health player_Hp_BarHealth;
    private Transform player_Hp_PlayerHPBar;
    #endregion

    protected override void Init()
    {
        base.Init();
        //角色
        player_Animator = transform.Find_Child<Animator>(Config_Player.player_pf_Body);
        player_Rigidbody2D = transform.GetComponent<Rigidbody2D>();
        player_MiniMapIcon_Animator = transform.Find_Child<Animator>(Config_Player.player_pf_MiniMap);

        //角色的枪
        player_Gun_PlayerGun_Transform = transform.Find_Child<Transform>(Config_Player.player_pf_Gun_PlayerGun);
        player_Gun_PlayerGun_Animator = transform.Find_Child<Animator>(Config_Player.player_pf_Gun_PlayerGun);
        player_Gun_PlayerGun_EndPoint = transform.Find_Child<Transform>(Config_Player.player_pf_Gun_PlayerGun_EndPoint);
        player_Gun_PlayerGun_ShootPoint = transform.Find_Child<Transform>(Config_Player.player_pf_Gun_PlayerGun_ShootPoint);

        //角色的血条
        player_Hp_Bar = transform.Find_Child<Transform>(Config_Common.HPbar_Prefab_Bar);
        player_Hp_BarHealth = transform.Find_Child<Bar_Health>(Config_Player.player_HP_WorldBar);
        player_Hp_PlayerHPBar = transform.Find_Child<Transform>(Config_Player.player_HP_PlayerHPBar);
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
    public Transform Player_Hp_Bar
    {
        get { return player_Hp_Bar; }
    }
    public Bar_Health Player_Hp_BarHealth
    {
        get { return player_Hp_BarHealth; }
    }
    public Transform Player_Hp_PlayerHPBar
    {
        get { return player_Hp_PlayerHPBar; }
    }
    public Transform Player_Gun_PlayerGun_EndPoint
    {
        get { return player_Gun_PlayerGun_EndPoint; }
    }
    public Transform Player_Gun_PlayerGun_ShootPoint
    {
        get { return player_Gun_PlayerGun_ShootPoint; }
    }

}

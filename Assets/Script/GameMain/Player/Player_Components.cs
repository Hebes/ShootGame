using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;

public class Player_Components : MonoBehaviour
{
    #region Player_Body
    private Animator player_Animator;
    private Rigidbody2D player_Rigidbody2D;
    private Animator player_MiniMapIcon_Animator;
    #endregion

    #region Player_Gun
    private Transform player_Gun_PlayerGun_EndPoint;
    private Transform player_Gun_PlayerGun_ShootPoint;
    private Animator player_Gun_PlayerGun_Animator;
    #endregion

    #region Player_HP
    
    private Transform player_Hp_PlayerHPBar;
    #endregion

    private void Awake() => Player_GetComponent();

    /// <summary>
    /// 获取玩家组件
    /// </summary>
    private void Player_GetComponent()
    {
        //角色
        player_Animator = transform.Find_Child<Animator>(Config_Player.player_pf_Body);
        player_Rigidbody2D = transform.GetComponent<Rigidbody2D>();
        player_MiniMapIcon_Animator = transform.Find_Child<Animator>(Config_Player.player_pf_MiniMap);

        //角色的枪
        Player_Gun_PlayerGun_Transform = transform.Find_Child<Transform>(Config_Player.player_pf_Gun_PlayerGun);
        player_Gun_PlayerGun_Animator = transform.Find_Child<Animator>(Config_Player.player_pf_Gun_PlayerGun);
        player_Gun_PlayerGun_EndPoint = transform.Find_Child<Transform>(Config_Player.player_pf_Gun_PlayerGun_EndPoint);
        player_Gun_PlayerGun_ShootPoint = transform.Find_Child<Transform>(Config_Player.player_pf_Gun_PlayerGun_ShootPoint);

        //角色的血条
        player_Hp_PlayerHPBar = transform.Find_Child<Transform>(Config_Player.player_HP_PlayerHPBar);
    }



    public Animator Player_Animator => player_Animator;
    public Rigidbody2D Player_Rigidbody2D => player_Rigidbody2D;
    public Transform Player_Transform => transform;
    public Animator Player_MiniMapIcon_Animator => player_MiniMapIcon_Animator;
    public Transform Player_Gun_PlayerGun_Transform { set; get; }
    public Animator Player_Gun_PlayerGun_Animator => player_Gun_PlayerGun_Animator;
    public Transform Player_Hp_PlayerHPBar => player_Hp_PlayerHPBar;
    public Transform Player_Gun_PlayerGun_EndPoint => player_Gun_PlayerGun_EndPoint;
    public Transform Player_Gun_PlayerGun_ShootPoint => player_Gun_PlayerGun_ShootPoint;

}

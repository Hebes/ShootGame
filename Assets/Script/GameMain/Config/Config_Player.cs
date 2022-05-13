using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config_Player 
{
    //pf->Prefab

    #region 玩家物体
    public const string player_pf_Body = "Player_Body";
    public const string player_pf_MiniMap = "MiniMap";
    public const string player_pf_Gun_PlayerGun = "Player_Gun";
    public const string player_pf_Gun_PlayerGun_EndPoint = "Gun_EndPoint";
    public const string player_pf_Gun_PlayerGun_ShootPoint = "Gun_ShootPoint";
    #endregion

    #region 玩家动画
    public const string player_Animator_Bool = "IsMoving";
    #endregion

    #region 玩家射击事件
    public const string player_Event_Shoot = "Player_Shoot";
    #endregion

    #region 玩家气血条物体
    public const string player_HP_WorldBar = "World_Bar";
    public const string player_HP_PlayerHPBar = "Player_HPBar";
    #endregion
}

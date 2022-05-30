using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 事件配置表
/// </summary>
 public enum EEvent 
{
    //UI事件
    Loading,
    
    //玩家事件
    Player_Shoot,
     

    //按键事件
    KeyInputDown,
    KeyInputUp,

}




/// <summary>
/// Tag配置
/// </summary>
enum ETags
{
    /// <summary>玩家</summary>
    Player,
    /// <summary>敌人</summary>
    Enemy,
    /// <summary>子弹</summary>
    Bullet,
    /// <summary>特效</summary>
    Effect,
}

#region 场景
public enum EScenes
{
    None,
    MenuScene,
    MainGame
}
#endregion




#region enemy
/// <summary>
/// 转换条件
/// </summary>
public enum Transition
{
    NullTransition = 0,//空的转换条件
    SeePlayer,//看到玩家
    LostPlayer,//追赶过程中遗失目标玩家
}

/// <summary>
/// 当前状态
/// </summary>
public enum StateID
{
    NullState,//空的状态
    Patrol,//巡逻状态
    Chase,//追赶状态
}
#endregion

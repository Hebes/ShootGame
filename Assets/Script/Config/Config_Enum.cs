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




#region 资源加载
/// <summary>
/// 物体的类型
/// </summary>
public enum EResourceType
{
    prefab,
    sprite,
}
/// <summary>
/// 文本名字
/// </summary>
public enum ETextName
{
    enumConfigMap,
    pfConfigMap,
    spritConfigMap,
}
/// <summary>
/// 物体名称
/// </summary>
public enum EpfName {
    MedicalBox,
    ChatBubble,
    pfBullet,
    pfBulletPhysics,
    pfSmoke,
    Bar_HP,
    pfDamagePopup,
    TargetPrefab,
    UI_Target,
}
#endregion



#region Sprite
public enum ESprite
{
    Border,
    BorderGrey,
    Coin,
    GradientVertical,
    HealthPotion,
    ManaPotion,
    Medkit,
    Sword,
    ApexMedkit,
    ArmorPing,
    ArmorTier3,
    AttackCircleIcon,
    AttackPingOutline,
    AttackPing,
    AttackingHereCircleIcon,
    DefendCircleIcon,
    EnemySeenCircleIcon,
    HelmetPing,
    HelmetTier2,
    LootingCircleIcon,
    MedkitPing,
    MoveCircleIcon,
    MovePingOutline,
    MovePingSide,
    MovePing,
    PingPole,
    WatchingHereCircleIcon,
}
#endregion




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

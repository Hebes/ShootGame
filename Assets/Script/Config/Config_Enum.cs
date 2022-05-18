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
}
/// <summary>
/// 文本名字
/// </summary>
public enum ETextName
{
    enumConfigMap,
    pfConfigMap,
}
/// <summary>
/// 物体名称
/// </summary>
public enum EpfName {
    MedicalBox,
    pfBullet,
    pfBulletPhysics,
    pfSmoke,
    Bar_HP,
    pfDamagePopup,
    TargetPrefab,
    UI_Target,
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
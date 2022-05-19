using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 颜色表
/// 参考：https://www.csdn.net/tags/MtTaIgwsODE4MzU1LWJsb2cO0O0O.html
/// </summary>
public static class Config_Color
{
    #region 图标和距离文字颜色
    /// <summary>
    /// 头盔颜色 Color.Aqua
    /// </summary>
    public static Color CHelmet = new Color(0 / 255f, 255 / 255f, 255 / 255f);
    /// <summary>
    /// 盔甲颜色 Color.Lime
    /// </summary>
    public static Color CArmor = new Color(0 / 255f, 255 / 255f, 0 / 255f);
    /// <summary>
    /// 医疗箱颜色  Color.Brown
    /// </summary>
    public static Color CMedkit = new Color(165 / 255f, 42 / 255f, 42 / 255f);
    /// <summary>
    /// 移动颜色  Color.CornflowerBlue
    /// </summary>
    public static Color CMove = new Color(100 / 255f, 149 / 255f, 237 / 255f);
    /// <summary>
    /// 敌人颜色   Color.Olive
    /// </summary>
    public static Color CEnemy = new Color(128 / 255f, 128 / 255f, 0 / 255f);
    /// <summary>
    /// 抢劫颜色  Color.DarkSlateGray
    /// </summary>
    public static Color CLooting = new Color(40 / 255f, 79 / 255f, 79 / 255f);
    /// <summary>
    /// 攻击颜色 Color.Firebrick
    /// </summary>
    public static Color CAttacking = new Color(178 / 255f, 34 / 255f, 34 / 255f);
    /// <summary>
    /// 会在这里颜色 Color.Indigo
    /// </summary>
    public static Color CGoingHere = new Color(75 / 255f, 0 / 255f, 130 / 255f);
    /// <summary>
    /// 防御颜色 Color.DarkSlateBlue
    /// </summary>
    public static Color CDefend = new Color(72 / 255f, 61 / 255f, 139 / 255f);
    /// <summary>
    /// 查看颜色 Color.DodgerBlue30, 144, 255
    /// </summary>
    public static Color CWatching =  new Color(30 / 255f, 144 / 255f, 255 / 255f);
    /// <summary>
    /// 敌对颜色 Color.Sienna
    /// </summary>
    public static Color CEnemyseen = new Color(160 / 255f, 82 / 255f, 45 / 255f);
    #endregion


    #region 物体发光的颜色
    /// <summary>
    /// 金币发光颜色 
    /// </summary>
    public static Color CShineCoin = new Color(1, 1, 0);
    /// <summary>
    /// 魔法药水发光颜色
    /// </summary>
    public static Color CShineManaPotion = new Color(0, 0, 1);
    /// <summary>
    /// 气血药水发光颜色
    /// </summary>
    public static Color CShineHealthPotion = new Color(1, 0, 0);
    /// <summary>
    /// 剑发光颜色
    /// </summary>
    public static Color CShineSword = new Color(0, 0, 0);
    /// <summary>
    /// 医疗箱发光颜色  Color.Brown
    /// </summary>
    public static Color CShineMedkit = new Color(1, 1, 0);
    #endregion

}

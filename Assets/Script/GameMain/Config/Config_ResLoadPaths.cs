

public static class Config_ResLoadPaths
{
    //const效率高，readonly更灵活
    //区别：
    //1、const是一个编译期常量，readonly是一个运行时常量,const都是静态的，不能使用static修饰。
    //2、const只能修饰基元类型、枚举类型或字符串类型，readonly没有限制,eadonly可以用static修饰，也可以不用。

    #region Canvas界面层级
    public const string CANVAS_ONELAYER = "OneLayer";
    public const string CANVAS_TWOLAYER = "TwoLayer";
    public const string CANVAS_THREELAYER = "ThreeLayer";
    public const string CANVAS_SYSTEMLAYER = "SystemLayer";
    #endregion

    #region Resource资源路径相关
    #region Resource-UI面板
    //UI名字
    public const string UI_GAMESTART = "UI_GameStart";//游戏开始面板
    public const string UI_LOADING = "UI_Loading";//加载面板
    public const string UI_SETTING = "UI_Setting";//设置面板

    //第一层级
    //public const string PREFAB = "Prefabs/";//prefab
    //第二层级
    public const string PREFAB_UI = "UI/";//prefab_ui
                                          //第三层级
    public const string PREFAB_UI_BASE = PREFAB_UI + "UI_Base/";
    public const string PREFAB_UI_UIPANEL = PREFAB_UI + "UI_Panel/";//prefab_ui_UIPanel
                                                                    //第四层级
    public const string PREFAB_UI_BASE_CANVAS = PREFAB_UI_BASE + "Canvas";//UI基类面板
    public const string PREFAB_UI_BASE_EVENTSYSTEM = PREFAB_UI_BASE + "EventSystem";//UI基类面板
    #endregion
    #endregion



    #region 通用加载路径
    //一级路径
    private const string CommonPath = "Common";
    #endregion

    #region TargetSystem 系统相关加载资源
    //一级路径
    public const string targetSystem_Prefab_TargetSystemIcon = CommonPath + "/TargetPointIcon";
    //二级加载路径
    public const string targetSystem_Prefab_TargetSystemIcon_Prefab = targetSystem_Prefab_TargetSystemIcon + "/Prefab";
    //三级加载路径
    public const string targetSystem_Prefab_TargetSystemIcon_Prefab_TargetSystemPrefab = targetSystem_Prefab_TargetSystemIcon_Prefab + "/TargetPrefab";
    public const string targetSystem_UI_TargetUI = targetSystem_Prefab_TargetSystemIcon_Prefab + "/TargetUI";
    #endregion

    #region 鼠标光标加载资源
    //二级路径
    public const string cursor_Icons = CommonPath + "/CursorIcons";
    //三级加载路径
    public const string cursor_Icons_Arrow = cursor_Icons + "/Arrow";//默认
    public const string cursor_Icons_Attack = cursor_Icons + "/Attack";//默认
    public const string cursor_Icons_Grab = cursor_Icons + "/Grab";//默认
    public const string cursor_Icons_Move = cursor_Icons + "/Move";//默认
    public const string cursor_Icons_Unit = cursor_Icons + "/Unit";//默认
    #endregion

    #region 枪 靶子 子弹物体 pf->Prefab缩写
    public const string bullent_pf_Gun = CommonPath + "/Gun";
    public const string bullent_pf_Gun_Bullet = bullent_pf_Gun + "/Bullet/Prefab/pfBullet";
    public const string bullent_pf_Gun_BulletPhysics = bullent_pf_Gun + "/Bullet/Prefab/pfBulletPhysics";
    public const string Gun_pf_Effect = bullent_pf_Gun + "/Effect/pfSmoke";
    #endregion

    #region 伤害物体
    public const string damage_pf = CommonPath + "/Other/pfDamagePopup/Prefab/pfDamagePopup";
    #endregion

    #region 血条相关
    public const string BarHP_pf = CommonPath + "/HPBar/Prefab/Bar_HP";
    #endregion

    #region 物品相关
    public const string item_pf_Sample = CommonPath + "/Backpack/Prefab/";
    #endregion
}

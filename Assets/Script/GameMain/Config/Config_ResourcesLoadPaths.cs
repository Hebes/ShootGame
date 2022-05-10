

public static class Config_ResourcesLoadPaths
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
    private const string CommonPath = "Common";
    #endregion

    #region TargetSystem 系统相关加载资源
    //一级路径
    public const string targetSystem_Prefab_TargetSystemIcon = CommonPath+"/TargetSystemIcon";
    //二级加载路径
    public const string targetSystem_Prefab_TargetSystemIcon_Prefab = targetSystem_Prefab_TargetSystemIcon + "/Prefab";
    //三级加载路径
    public const string targetSystem_Prefab_TargetSystemIcon_Prefab_TargetSystemPrefab = targetSystem_Prefab_TargetSystemIcon_Prefab + "/TargetSystemPrefab";
    public const string targetSystem_UI_TargetUI = targetSystem_Prefab_TargetSystemIcon_Prefab + "/TargetUI";
    #endregion
}

using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/*
1、AddComponentMenu 导航栏菜单
2、ContextMenu 右键菜单
3、HeaderAttribute
4、HideInInspector 可以让public变量在Inspector上隐藏，无法在Editor中进行编辑
5、MultilineAttribute 支持输入多行文本
6、RangeAttribute 限定输入值的范围
7、RequireComponent 组件依赖，使用该组件后自动添加依赖组件
8、RuntimeInitializeOnLoadMethodAttribute
9、SerializeField 强制对变量进行序列化，即使变量是private
10、SpaceAttribute 增加空位
11、TooltipAttribute 提示信息，当鼠标移到Inspector上时显示相应的提示
12、InitializeOnLoadAttribute
13、InitializeOnLoadMethodAttribute
14、MenuItem 导航栏的菜单项
 */

public class GameManager : SingletonMono_Temp<GameManager>
{
    
    #region 第一种加载比Awake快的方法
    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    //static void InitRsv()
    //{
    //    //初始化资源加载
    //    List<MainGame_Init> mainGame_Inits = new List<MainGame_Init>()
    //    {
    //        MainGame_Res_pf_Manage.Instance,
    //        MainGame_Res_Sprite_Manage.Instance,
    //    };

    //    foreach (var item in mainGame_Inits)
    //    {
    //        item.Init();
    //    }
    //}
    #endregion


    protected override void Awake()
    {
        #region 更改Edit->Project Setting->Script Execution Order的脚本顺序
        //初始化资源加载  正常因放在第一场景加载
        List<MainGame_Init> mainGame_Inits = new List<MainGame_Init>()
        {
            MainGame_Res_pf_Manage.Instance,
            MainGame_Res_Sprite_Manage.Instance,
        };

        foreach (MainGame_Init item in mainGame_Inits)
            item.Init();
        #endregion
    }


    //以下为测试代码
    public Color pingMoveColor;
    public Color pingEnemyColor;
    public Sprite pingMoveSprite;//移动图标
    public Sprite pingEnemySprite;//敌人图标


    public Color pingHelmetColor;
    public Color pingArmorColor;
    public Sprite pingMedkitSprite;//医疗箱的图标
    public Sprite pingHelmetSprite;//头盔的图标
    public Sprite pingArmorSprite;//盔甲的图标


    public Sprite pingLootingSprite;//敌人图标
    public Sprite pingAttackingSprite;//敌人图标
    public Sprite pingGoingHereSprite;//敌人图标    
    public Sprite pingDefendSprite;//敌人图标
    public Sprite pingWatchingSprite;//敌人图标
    public Sprite pingEnemyseenSprite;//敌人图标

}

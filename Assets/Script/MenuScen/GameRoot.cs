using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
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
public class GameRoot : MonoBehaviour
{
    //测试 切换场景
    public void ChangeScene()
    {
        SceneManager.LoadScene(EScenes.MainGame.ToString());
    }

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

    /// <summary>
    /// 确保第一个场景为MenuScene
    /// </summary>
        [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        if (SceneManager.GetActiveScene().name == EScenes.MenuScene.ToString()) return;
        SceneManager.LoadScene(EScenes.MenuScene.ToString());
    }


    private async void Awake()
    {
        string str = await InitRsv();
        Debug.Log(str);
    }

    /// <summary>
    /// 初始化资源加载
    /// </summary>
    /// <returns></returns>
    private async Task<string> InitRsv()
    {
        #region 更改Edit->Project Setting->Script Execution Order的脚本顺序
        //初始化资源加载  正常因放在第一场景加载
        List<MainGame_Init> mainGame_Inits = new List<MainGame_Init>()
        {
            Manage_Res_pf.Instance,
            Res_Sprite_Manage.Instance,
        };

        foreach (MainGame_Init item in mainGame_Inits)
        {
            item.Init();
            await Task.Delay(TimeSpan.FromSeconds(0.5f));
        }
        #endregion

        return "初始化加载完成";//InitRsvOver
    }
}

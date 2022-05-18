using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            MainGame_Res_pf_Manage.Instance,
            MainGame_Res_Sprite_Manage.Instance,
        };

        foreach (MainGame_Init item in mainGame_Inits)
        {
            item.Init();
            await Task.Delay(TimeSpan.FromSeconds(0.5f));
        }
        #endregion

        return "InitRsvOver";
    }
}

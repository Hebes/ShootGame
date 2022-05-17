using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景切换模块
/// 知识点
/// 1.场景异步加载
/// 2.协程
/// 3.委托
/// </summary>
public class ScenesMgr : BaseManager<ScenesMgr>
{

    /// <summary>
    /// 切换场景 同步
    /// </summary>
    /// <param name="name"></param>
    public void LoadScene(string name, UnityAction fun = null)
    {
        //场景同步加载
        SceneManager.LoadScene(name);
        //加载完成过后 才会去执行fun

        fun?.Invoke();
    }
    /// <summary>
    /// 加载当前场景并显示另外一个场景
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fun"></param>
    public void LoadSceneAndShowOtherScene(string name, LoadSceneMode loadSceneMode = LoadSceneMode.Additive, UnityAction fun = null)
    {
        //场景同步加载
        SceneManager.LoadScene(name, loadSceneMode);
        //加载完成过后 才会去执行fun

        fun?.Invoke();
    }

    /// <summary>
    /// 提供给外部的 异步加载的接口方法
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fun"></param>
    public void LoadSceneAsyn(string name, UnityAction fun = null)
    {
        MonoMgr.Instance.StartCoroutine(ReallyLoadSceneAsyn(name, fun));
    }

    /// <summary>
    /// 协程异步加载场景
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fun"></param>
    /// <returns></returns>
    private IEnumerator ReallyLoadSceneAsyn(string name, UnityAction fun = null)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.Log("路径为空!!");
            yield break;//终止协程
        }

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name);
        asyncOperation.allowSceneActivation = false;

        int displayProgress = 0;
        //可以得到场景加载的一个进度
        while (asyncOperation.progress < 0.9f)
        {
            while (displayProgress < (int)asyncOperation.progress * 100)
            {
                ++displayProgress;
                EventCenter.Instance.EventTrigger(Config_Event.Loading, displayProgress);
                yield return displayProgress;
            }
        }
        asyncOperation.allowSceneActivation = true;


        while (displayProgress < 100)
        {
            ++displayProgress;
            //事件中心 向外分发 进度情况  外面想用就用
            EventCenter.Instance.EventTrigger(Config_Event.Loading, displayProgress);
            //这里面去更新进度条
            yield return displayProgress;
        }

        //加载完成过后 才会去执行fun
        fun?.Invoke();
    }
}

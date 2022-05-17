using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 1.可以提供给外部添加帧更新事件的方法
/// 2.可以提供给外部添加 协程的方
/// 3.变成单例模式
/// </summary>
public class MonoMgr : BaseManager<MonoMgr>
{
    private MonoController controller;

    protected override void BaseManager_Init() => controller = MonoController.Instance;

    #region isRun 用于暂停画面等
    /// <summary>
    /// 用于暂停画面等
    /// </summary>
    public void StopRun()
    {
        controller.IsRun = false;
    }
    /// <summary>
    /// 用于开始画面等
    /// </summary>
    public void StartRun()
    {
        controller.IsRun = true;
    }
    #endregion

    #region Update
    /// <summary>
    /// 给外部提供的 添加帧更新事件的函数
    /// </summary>
    /// <param name="fun"></param>
    public void AddUpdateListener(UnityAction fun)
    {
        controller.AddUpdateListener(fun);
    }

    /// <summary>
    /// 提供给外部 用于移除帧更新事件函数
    /// </summary>
    /// <param name="fun"></param>
    public void RemoveUpdateListener(UnityAction fun)
    {
        controller.RemoveUpdateListener(fun);
    }
    #endregion

    #region 协程
    /// <summary>
    /// 携程方法的使用
    /// </summary>
    /// <param name="routine"></param>
    /// <returns></returns>
    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return controller.StartCoroutine(routine);
    }

    public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
    {
        return controller.StartCoroutine(methodName, value);
    }

    public Coroutine StartCoroutine(string methodName)
    {
        return controller.StartCoroutine(methodName);
    }
    #endregion
}

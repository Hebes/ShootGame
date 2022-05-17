using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Mono的管理者
/// 1.声明周期函数
/// 2.事件 
/// 3.协程
/// </summary>
public class MonoController : SingletonMono_Continue<MonoController>
{
    private event UnityAction updateEvent;
    public bool IsRun { set; get; } = true;

    // Update is called once per frame
    void Update()
    {
        if (!IsRun) return;

        if (updateEvent != null)
            updateEvent();
    }

    #region UpdateEvent
    /// <summary>
    /// 给外部提供的 添加帧更新事件的函数
    /// </summary>
    /// <param name="UpdateFun"></param>
    public void AddUpdateListener(UnityAction updateFun)
    {
        updateEvent += updateFun;
    }

    /// <summary>
    /// 提供给外部 用于移除帧更新事件函数
    /// </summary>
    /// <param name="UpdateFun"></param>
    public void RemoveUpdateListener(UnityAction updateFun)
    {
        updateEvent -= updateFun;
    }
    #endregion


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public interface IEventInfo
{

}

public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;

    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}

public class EventInfo : IEventInfo
{
    public UnityAction actions;

    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}


/// <summary>
/// 事件中心 单例模式对象
/// 1.Dictionary
/// 2.委托
/// 3.观察者设计模式
/// 4.泛型
/// </summary>
public class EventCenter : BaseManager<EventCenter>
{
    //key —— 事件的名字（比如：怪物死亡，玩家死亡，通关 等等）
    //value —— 对应的是 监听这个事件 对应的委托函数们
    private Dictionary<EEvent, IEventInfo> eventDic = new Dictionary<EEvent, IEventInfo>();

    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="name">事件的名字</param>
    /// <param name="action">准备用来处理事件 的委托函数</param>
    public void AddEventListener<T>(EEvent config_Event, UnityAction<T> action)
    {
        //有没有对应的事件监听
        //有的情况
        if (eventDic.ContainsKey(config_Event))
            (eventDic[config_Event] as EventInfo<T>).actions += action;
        //没有的情况
        else
            eventDic.Add(config_Event, new EventInfo<T>(action));
    }

    /// <summary>
    /// 监听不需要参数传递的事件
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public void AddEventListener(EEvent config_Event, UnityAction action)
    {
        //有没有对应的事件监听
        //有的情况
        if (eventDic.ContainsKey(config_Event))
            (eventDic[config_Event] as EventInfo).actions += action;
        //没有的情况
        else
            eventDic.Add(config_Event, new EventInfo(action));
    }


    /// <summary>
    /// 移除对应的事件监听
    /// </summary>
    /// <param name="name">事件的名字</param>
    /// <param name="action">对应之前添加的委托函数</param>
    public void RemoveEventListener<T>(EEvent config_Event, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(config_Event))
            (eventDic[config_Event] as EventInfo<T>).actions -= action;
    }

    /// <summary>
    /// 移除不需要参数的事件
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public void RemoveEventListener(EEvent config_Event, UnityAction action)
    {
        if (eventDic.ContainsKey(config_Event))
            (eventDic[config_Event] as EventInfo).actions -= action;
    }

    /// <summary>
    /// 事件触发
    /// </summary>
    /// <param name="name">哪一个名字的事件触发了</param>
    public void EventTrigger<T>(EEvent config_Event, T info)
    {
        //有没有对应的事件监听
        //有的情况
        if (eventDic.ContainsKey(config_Event))
        {
            //eventDic[name]();
            //if ((eventDic[config_Event] as EventInfo<T>).actions != null)
            (eventDic[config_Event] as EventInfo<T>).actions?.Invoke(info);
            //eventDic[name].Invoke(info);
        }
    }

    /// <summary>
    /// 事件触发（不需要参数的）
    /// </summary>
    /// <param name="name"></param>
    public void EventTrigger(EEvent config_Event)
    {
        //有没有对应的事件监听
        //有的情况
        if (eventDic.ContainsKey(config_Event))
        {
            //eventDic[name]();
            //if ((eventDic[config_Event] as EventInfo).actions != null)
            (eventDic[config_Event] as EventInfo).actions?.Invoke();
            //eventDic[name].Invoke(info);
        }
    }

    /// <summary>
    /// 清空事件中心
    /// 主要用在 场景切换时
    /// </summary>
    public void Clear() => eventDic.Clear();
}

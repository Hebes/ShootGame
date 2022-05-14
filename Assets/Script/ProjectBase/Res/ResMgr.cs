using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 资源加载模块
/// 1.异步加载
/// 2.委托和 lambda表达式
/// 3.协程
/// 4.泛型
/// </summary>
public class ResMgr : BaseManager<ResMgr>
{
    #region 加载单个资源
    public T Load<T>(string path, Vector3 position, Quaternion quaternion, Transform parent) where T : Object
    {
        T res = Resources.Load<T>(path);
        return (IsCreate(res)) ? Object.Instantiate(res, position, quaternion, parent) : res as T;
    }

    public T Load<T>(string path, Transform parent) where T : Object
    {
        T res = Resources.Load<T>(path);
        return (IsCreate(res)) ? Object.Instantiate(res, parent) : res as T;
    }

    //同步加载资源
    public T Load<T>(string path) where T : Object
    {
        T res = Resources.Load<T>(path);
        //如果对象是一个GameObject类型的 我把他实例化后 再返回出去 外部 直接使用即可
        return IsCreate(res) ? Object.Instantiate(res) : res;
    }
    #endregion

    #region 加载一个文件夹下的资源
    public T[] LoadAll<T>(string name) where T : Object
    {
        return Resources.LoadAll<T>(name);
    }
    #endregion

    #region 异步加载资源
    //异步加载资源
    public void LoadAsync<T>(string name, UnityAction<T> callback) where T : Object
    {
        //开启异步加载的协程
        MonoMgr.Instance.StartCoroutine(ReallyLoadAsync(name, callback));
    }

    //真正的协同程序函数  用于 开启异步加载对应的资源
    private IEnumerator ReallyLoadAsync<T>(string name, UnityAction<T> callback) where T : Object
    {
        ResourceRequest r = Resources.LoadAsync<T>(name);

        yield return r;//判断是否加载成功

        if (IsCreate(r.asset))
            callback?.Invoke(Object.Instantiate(r.asset) as T);
        else
            callback?.Invoke(r.asset as T);
    }
    #endregion

    #region 通用判断方法
    /// <summary>
    /// 判断是否需要生成
    /// </summary>
    private bool IsCreate<T>(T obj) where T : Object
    {
        //判断需要生成的物体可以在(obj is GameObject)添加例如(obj is GameObject|| obj is Transform)
        return (obj is GameObject) ? true : false;
    }
    #endregion
}

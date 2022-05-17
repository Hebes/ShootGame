using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

/// <summary>
/// 资源加载模块
/// 1.异步加载
/// 2.委托和 lambda表达式
/// 3.协程
/// 4.泛型
/// </summary>
public class ResMgr : BaseManager<ResMgr>
{
    private ILoader loader;
    protected override void BaseManager_Init() => loader = new ResLoader();

    #region 同步泛型加载
    /// <summary>
    /// 加载泛型物体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T LoadRes<T>(string path) where T : UnityEngine.Object
        => loader.LoadRes<T>(path);
    /// <summary>
    /// 加载所有的泛型物体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T[] LoadAllRes<T>(string path) where T : UnityEngine.Object
        => loader.LoadAllRes<T>(path);
    #endregion

    #region 异步泛型加载
    /// <summary>
    /// 异步加载单个资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="callback"></param>
    public void LoadResAysn<T>(string path, Action<T> callback) where T : UnityEngine.Object
        => loader.LoadResAysn(path, callback);
    /// <summary>
    /// 异步加载所有的发行物体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="callback"></param>
    public void LoadAllResAysn<T>(string path, Action<T[]> callback) where T : UnityEngine.Object
        => loader.LoadAllResAysn(path, callback);
    #endregion
    
    /// <summary>
    /// 加载物体
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public GameObject Load_pf(string path)
        => loader.LoadPrefab(path);
    /// <summary>
    /// 加载物体和设置父物体，并且实例化
    /// </summary>
    /// <param name="path"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject Load_pf_Instantiate(string path, Transform parent = null)
        => loader.LoadPrefabAndInstantiate(path, parent);
    public GameObject Load_pf_Instantiate(string path, Vector3 position, Quaternion rotation, Transform parent = null)
        => loader.LoadPrefabAndInstantiate(path, position, rotation, parent);


    #region 以下为旧代码
    #region 加载单个资源
    //public T Load<T>(string path, Vector3 position, Quaternion quaternion, Transform parent) where T : Object
    //{
    //    T res = Resources.Load<T>(path);
    //    return (IsCreate(res)) ? Object.Instantiate(res, position, quaternion, parent) : res as T;
    //}

    //public T Load<T>(string path, Transform parent) where T : Object
    //{
    //    T res = Resources.Load<T>(path);
    //    return (IsCreate(res)) ? Object.Instantiate(res, parent) : res as T;
    //}

    ////同步加载资源
    //public T Load<T>(string path) where T : Object
    //{
    //    T res = Resources.Load<T>(path);
    //    //如果对象是一个GameObject类型的 我把他实例化后 再返回出去 外部 直接使用即可
    //    return IsCreate(res) ? Object.Instantiate(res) : res;
    //}
    #endregion

    #region 加载一个文件夹下的资源
    //public T[] LoadAll<T>(string name) where T : Object
    //{
    //    return Resources.LoadAll<T>(name);
    //}
    #endregion

    #region 异步加载资源
    ////异步加载资源
    //public void LoadAsync<T>(string name, UnityAction<T> callback) where T : Object
    //{
    //    //开启异步加载的协程
    //    MonoMgr.Instance.StartCoroutine(ReallyLoadAsync(name, callback));
    //}

    ////真正的协同程序函数  用于 开启异步加载对应的资源
    //private IEnumerator ReallyLoadAsync<T>(string name, UnityAction<T> callback) where T : Object
    //{
    //    ResourceRequest r = Resources.LoadAsync<T>(name);

    //    yield return r;//判断是否加载成功

    //    if (IsCreate(r.asset))
    //        callback?.Invoke(Object.Instantiate(r.asset) as T);
    //    else
    //        callback?.Invoke(r.asset as T);
    //}
    #endregion

    #region 通用判断方法
    /// <summary>
    /// 判断是否需要生成
    /// </summary>
    /// //判断需要生成的物体可以在(obj is GameObject)添加例如(obj is GameObject|| obj is Transform)
    //private bool IsCreate<T>(T obj) where T : Object => obj is GameObject;
    #endregion
    #endregion
}

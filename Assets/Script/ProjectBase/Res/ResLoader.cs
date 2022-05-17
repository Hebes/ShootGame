using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ResLoader : ILoader
{

    #region 泛型同步加载
    /// <summary>
    /// 加载单个资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T LoadRes<T>(string path) where T : UnityEngine.Object
    {
        var res = Resources.Load<T>(path);

        if (res == null)
        {
            Debug.LogError($"加载资源失败:失败路径为:{path}");
            return null;
        }
        return res;
    }

    /// <summary>
    /// 加载一个文件夹下的资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T[] LoadAllRes<T>(string path) where T : UnityEngine.Object
    {
        var res = Resources.LoadAll<T>(path);
        if (res == null || res.Length == 0)
        {
            Debug.LogError($"加载资源失败:失败路径为:{path}");
            return null;
        }
        return res;
    }
    #endregion

    #region 泛型异步加载
    /// <summary>
    /// 异步加载单个资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="callback"></param>
    public void LoadResAysn<T>(string path, Action<T> callback) where T : UnityEngine.Object
        => CoroutineMgr.Instance.ExcuteOne(RealLoadResAsyn(path, callback));

    private IEnumerator RealLoadResAsyn<T>(string path, Action<T> callback) where T : Object
    {
        var request = Resources.LoadAsync<T>(path);
        yield return request;

        if (IsCreate(request.asset))
            callback?.Invoke(Object.Instantiate(request.asset) as T);
        else
            callback?.Invoke(request.asset as T);
    }

    /// <summary>
    /// 异步加载所有
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="callback"></param>
    public void LoadAllResAysn<T>(string path, Action<T[]> callback) where T : UnityEngine.Object
        => CoroutineMgr.Instance.ExcuteOne(RealLoadAllResAsyn(path, callback));

    private IEnumerator RealLoadAllResAsyn<T>(string path, Action<T[]> callback) where T : Object
    {
        var request = Resources.LoadAsync<T>(path);
        yield return request;
        callback?.Invoke(request.asset as T[]);
    }
    #endregion

    public GameObject LoadPrefab(string path)
        => LoadRes<GameObject>(path);

    #region 同步加载并且设置实例化的参数
    public GameObject LoadPrefabAndInstantiate(string path, Transform parent = null)
        => Object.Instantiate(LoadRes<GameObject>(path), parent);
    public GameObject LoadPrefabAndInstantiate(string path, Vector3 position, Quaternion rotation, Transform parent = null)
        => Object.Instantiate(LoadRes<GameObject>(path), position, rotation, parent);
    #endregion


    #region 通用判断方法
    /// <summary>
    /// 判断是否需要生成
    /// </summary>
    /// //判断需要生成的物体可以在(obj is GameObject)添加例如(obj is GameObject|| obj is Transform)
    private bool IsCreate<T>(T obj) where T : Object => obj is GameObject;
    #endregion
}

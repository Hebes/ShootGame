using System;
using UnityEngine;
using Object = UnityEngine.Object;

interface ILoader
{
    #region 泛型同步加载
    T LoadRes<T>(string path) where T : Object;//同步加载单个资源
    T[] LoadAllRes<T>(string path) where T : Object;//同步加载多个资源
    #endregion

    #region 泛型异步回调加载
    void LoadResAysn<T>(string path, Action<T> callback) where T : Object;//异步加载单个资源
    void LoadAllResAysn<T>(string path, Action<T[]> callback) where T : Object;//异步加载多个资源
    #endregion


    GameObject LoadPrefab(string path);

    #region 同步加载并且设置实例化的参数
    GameObject LoadPrefabAndInstantiate(string path, Transform parent = null);
    GameObject LoadPrefabAndInstantiate(string path, Vector3 position, Quaternion rotation, Transform parent = null);
    #endregion
}

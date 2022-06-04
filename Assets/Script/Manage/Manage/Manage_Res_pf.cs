using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// MainGame场景的 物体 资源加载管理器
/// pf=prefab
/// </summary>
public class Manage_Res_pf :BaseManager<Manage_Res_pf>, Manage_Init
{
    private Dictionary<string, GameObject> res_pf_Dic = new Dictionary<string, GameObject>();

    public void Init()
    {
        MonoMgr.Instance.StartCoroutine(Load());
        Debug.Log("物体资源加载完毕！");
    }

    IEnumerator Load()
    {
        Dictionary<string, string> temp_dic = ResourceMappingSystem.Instance.ResourceDic(ETextName.ConfigMap_pf);
        yield return temp_dic;
        //添加到物体字典
        foreach (var item in temp_dic)
            res_pf_Dic[item.Key] = ResMgr.Instance.LoadRes<GameObject>(item.Value);
    }
    
    public GameObject Get_pf(string pfName)
    {
        GameObject pf = null;
        if (!res_pf_Dic.TryGetValue(pfName, out pf))
            Debug.LogError($"未找到名为:{pfName}的GameObject");
        return pf;
    }

    #region 实例化
    public GameObject GetAndInstantiate(EpfName pfName, Transform parent)
        => UnityEngine.Object.Instantiate(Get_pf(pfName.ToString()), parent);
    public GameObject GetAndInstantiate(EpfName pfName, Vector3 positon, Quaternion quaternion, Transform parent = null)
        => UnityEngine.Object.Instantiate(Get_pf(pfName.ToString()), positon, quaternion, parent);
    #endregion

}

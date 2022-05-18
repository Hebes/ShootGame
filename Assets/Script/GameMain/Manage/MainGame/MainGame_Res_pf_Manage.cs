using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// MainGame场景的 物体 资源加载管理器
/// pf=prefab
/// </summary>
public class MainGame_Res_pf_Manage :BaseManager<MainGame_Res_pf_Manage>, MainGame_Init
{
    private Dictionary<string, GameObject> res_pf_Dic;

    public void Init()
    {
        res_pf_Dic = new Dictionary<string, GameObject>();

        Add_ResPfDic(Config_ResLoadPaths.item_pf_Sample);
        Add_ResPfDic(Config_ResLoadPaths.ts_pf_path_Prefab);
    }

    /// <summary>
    /// 从Resources加载item物品
    /// </summary>
    /// <param name="path"></param>
    private void Add_ResPfDic(string path)
    {
        

        GameObject[] go = ResMgr.Instance.LoadAllRes<GameObject>(path);

        foreach (var item in go)
            res_pf_Dic[item.name] = item;
    }
    
    private GameObject Get_pf(string pfName)
    {
        GameObject pf = null;
        if (!res_pf_Dic.TryGetValue(pfName, out pf))
            Debug.LogError($"未找到名为:{pfName}的GameObject");
        return pf;
    }

    public GameObject GetAndInstantiate(string pfName, Transform parent) => Object.Instantiate(Get_pf(pfName), parent);
    public GameObject GetAndInstantiate(string pfName,Vector3 positon,Quaternion quaternion, Transform parent=null) 
        => Object.Instantiate(Get_pf(pfName), positon, quaternion, parent);
}

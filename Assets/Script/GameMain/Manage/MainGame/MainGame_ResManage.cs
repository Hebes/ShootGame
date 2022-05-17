using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于场景资源初始化加载
/// </summary>
public class MainGame_ResManage :BaseManager<MainGame_ResManage>, MainGame_Init
{
    private Dictionary<string, List<GameObject>> resPfDic;

    public void Init()
    {
        Add_ResPfDic(Config_ResLoadPaths.item_pf_Sample);
    }

    /// <summary>
    /// 从Resources加载item物品
    /// </summary>
    /// <param name="path"></param>
    private void Add_ResPfDic(string path)
    {
        resPfDic = new Dictionary<string, List<GameObject>>();

        GameObject[] go = ResMgr.Instance.LoadAllRes<GameObject>(path);

        foreach (GameObject item in go)
        {
            if (resPfDic.ContainsKey(path))
                resPfDic[path].Add(item);
            else
                resPfDic.Add(path, new List<GameObject>() { item });
        }
    }

    private void GetFrom_resPfDic()
    {

    }
}

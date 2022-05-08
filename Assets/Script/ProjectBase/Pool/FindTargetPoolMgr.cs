using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollector
{
    public GameObject[] Targets;//敌人数组
    public bool IsActive;//是否活动状态

    public TargetCollector(string tag)
    {
        SetTarget(tag);
    }

    /// <summary>
    /// 设置目标
    /// </summary>
    /// <param name="tag"></param>
	public void SetTarget(string tag)
    {
        Targets = (GameObject[])GameObject.FindGameObjectsWithTag(tag);//找到所有这个标签的敌人
    }

}

public class FindTargetPoolMgr : BaseManager<FindTargetPoolMgr>
{
    public Dictionary<string, TargetCollector> TargetList = new Dictionary<string, TargetCollector>();
    public int TargetTypeCount = 0;//敌人的类型数量

    /// <summary>
    /// 清空
    /// </summary>
    public void Clear()
    {
        TargetList.Clear();
        //TargetList = new Dictionary<string, TargetCollector>(1);
    }

    /// <summary>
    /// 通过标签找到敌人
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public TargetCollector FindTargetTag(string tag)
    {
        if (TargetList.ContainsKey(tag))
        {
            TargetCollector targetcollector;
            if (TargetList.TryGetValue(tag, out targetcollector))//判断是否存在
            {
                targetcollector.IsActive = true;
                return targetcollector;
            }
            else
                return null;
        }
        else
            TargetList.Add(tag, new TargetCollector(tag));
        return null;
    }

    //TUDO 敌人池子 没写完  请参考武器系统源码 FinderPool
}


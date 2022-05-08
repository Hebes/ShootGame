using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollector
{
    public GameObject[] Targets;//��������
    public bool IsActive;//�Ƿ�״̬

    public TargetCollector(string tag)
    {
        SetTarget(tag);
    }

    /// <summary>
    /// ����Ŀ��
    /// </summary>
    /// <param name="tag"></param>
	public void SetTarget(string tag)
    {
        Targets = (GameObject[])GameObject.FindGameObjectsWithTag(tag);//�ҵ����������ǩ�ĵ���
    }

}

public class FindTargetPoolMgr : BaseManager<FindTargetPoolMgr>
{
    public Dictionary<string, TargetCollector> TargetList = new Dictionary<string, TargetCollector>();
    public int TargetTypeCount = 0;//���˵���������

    /// <summary>
    /// ���
    /// </summary>
    public void Clear()
    {
        TargetList.Clear();
        //TargetList = new Dictionary<string, TargetCollector>(1);
    }

    /// <summary>
    /// ͨ����ǩ�ҵ�����
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public TargetCollector FindTargetTag(string tag)
    {
        if (TargetList.ContainsKey(tag))
        {
            TargetCollector targetcollector;
            if (TargetList.TryGetValue(tag, out targetcollector))//�ж��Ƿ����
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

    //TUDO ���˳��� ûд��  ��ο�����ϵͳԴ�� FinderPool
}


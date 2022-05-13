using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;

/// <summary>
/// 枪靶子脚本
/// </summary>
public class GunTarget : MonoBehaviour
{
    private static List<GunTarget> targetList;
    private Animation thisAnimation;

    public Vector3 GetPosition
    {
        get { return transform.position; }
         
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="position">子弹的位置</param>
    /// <param name="maxRange">范围</param>
    /// <returns></returns>
    public static GunTarget GetClosest(Vector3 position, float maxRange)
    {
        GunTarget closest = null;
        foreach (GunTarget target in targetList)
        {
            if (Vector3.Distance(position, target.GetPosition) <= maxRange)
            {
                if (closest == null)
                {
                    closest = target;
                    if (Vector3.Distance(position, target.GetPosition) < Vector3.Distance(position, closest.GetPosition))
                    {
                        closest = target;
                    }
                }                
            }
        }
        return closest;
    }


    private void Awake()
    {
        if (targetList == null) targetList = new List<GunTarget>();
        targetList.Add(this);
        thisAnimation = transform.Find_Child<Animation>(Config_Common.Gun_pf_Body);
    }

    public void Damage()
    {
        thisAnimation.Play();

        //加载特效
        PoolMgr.Instance.GetObj(Config_ResLoadPaths.Gun_pf_Effect, (tfObj) => {
            tfObj.transform.position = transform.position + new Vector3(0, 0f) + UtilsClass.GetRandomDir() * Random.Range(0f, 2.2f);
            tfObj.transform.rotation = Quaternion.identity;
        });

        //Instantiate(GameAssets.i.pfSmoke, transform.position + new Vector3(0, 7.35f) + UtilsClass.GetRandomDir() * Random.Range(0f, 2.2f), Quaternion.identity);
    }

    
}

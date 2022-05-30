using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;

/// <summary>
/// 枪靶子脚本
/// </summary>
public class GunTarget : MonoBehaviour,ICommonCollide
{
    private static List<GunTarget> targetList;
    private Animation thisAnimation;

    public Vector3 GetPosition => transform.position;

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
        #region 第一种范围判定的射击方案
        //这个是搭配第一种射击方案的。//不能删除
        //if (targetList == null) targetList = new List<GunTarget>();
        //targetList.Add(this);
        #endregion

        thisAnimation = transform.Find_Child<Animation>(Config_Common.Gun_pf_Body);
    }

    public void Damage(int damageAmount) => thisAnimation.Play();//播放枪靶子动画
}

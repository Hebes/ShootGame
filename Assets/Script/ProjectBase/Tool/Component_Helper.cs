using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


/// <summary>
/// 普通的
/// </summary>
namespace Tool
{
    /// <summary>
    /// 帮助类
    /// </summary>
    public static class Component_Helper
    {
        #region Transform 拓展方法
        /// <summary>
        /// 未知层级,查找后代指定名称的变换组件。
        /// </summary>
        /// <param name="currentTF">当前变换组件</param>
        /// <param name="childName">后代物体名称</param>
        /// <returns></returns>
        private static Transform tfGet(this Transform transform, string childName)
        {
            //递归:方法内部又调用自身的过程。
            //1.在子物体中查找
            Transform childTF = transform.Find(childName);
            if (childTF != null) return childTF;
            for (int i = 0; i < transform.childCount; i++)
            {
                // 2.将任务交给子物体
                childTF = tfGet(transform.GetChild(i), childName);
                if (childTF != null) return childTF;
            }
            return null;
        }
        #endregion

        #region 寻找脚本拓展方法
        /// <summary>
        /// 未知层级,查找后代指定名称挂在的组件。
        /// </summary>
        /// <param name="currentTF">当前变换组件</param>
        /// <param name="childName">后代物体名称</param>
        /// <returns></returns>
        public static T Find_Child<T>(this Transform transform, string childName) where T : Component
        {
            //递归:方法内部又调用自身的过程。
            //1.在子物体中查找
            return tfGet(transform, childName).GetComponent<T>();
        }

        /// <summary>
        /// 获取根物体的脚本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transform"></param>
        /// <param name="path"></param>
        /// <param name="Component"></param>
        /// <returns></returns>
       public static T Get_Component_Up<T>(this Transform transform) where T : Component
        {
            return transform.root.GetComponent<T>();
        }

        //TUDO 有需要请拓展往上查找组件方法
        #endregion

        #region 生成伤害显示物体
        /// <summary>
        /// 显示伤害
        /// </summary>
        /// <param name="position">显示伤害的位置</param>
        /// <param name="damageAmount">伤害的数值</param>
        public static void Show_pf_Damage(Vector3 position, int damageAmount, bool isCriticalHit)
        {
            PoolMgr.Instance.GetObj(Config_ResLoadPaths.damage_pf, (obj) =>
            {
                obj.transform.position = position;
                obj.GetComponent<DamagePopup>().Setup(damageAmount, isCriticalHit);
            });
        }
        #endregion

        #region 加载特效
        /// <summary>
        /// 加载特效
        /// </summary>
        public static void LoadEffect(string path, Vector3 position)
        {
            //加载特效
            PoolMgr.Instance.GetObj(path, (tfObj) =>
            {

                tfObj.transform.position = position + new Vector3(0, 0f) + UtilsClass.GetRandomDir() * Random.Range(0f, 2.2f);
                tfObj.transform.rotation = Quaternion.identity;
            });
        }
        #endregion
    }
}


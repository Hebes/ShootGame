using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//C#中 泛型知识点
//设计模式 单例模式的知识点
//继承了 MonoBehaviour 的 单例模式对象 需要我们自己保证它的位移性
/// <summary>
/// 临时的 当前场景 SingletonMono
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonMono_Temp<T> : MonoBehaviour where T : SingletonMono_Temp<T>
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                //在场景中查找引用
                instance = FindObjectOfType<T>();
                if (instance == null)//如果是空的话
                    //立即执行Awake
                    instance = new GameObject("Singleton of " + typeof(T)).AddComponent<T>();//创建并添加这个本脚本
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
    }
}

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

    public static T Instance => instance ?? FindObjectOfType<T>() ?? new GameObject("Singleton of " + typeof(T)).AddComponent<T>();
    protected virtual void Awake()
    {
        if (instance == null) instance = this as T;
    }
}

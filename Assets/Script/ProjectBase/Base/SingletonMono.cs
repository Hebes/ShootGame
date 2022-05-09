using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//C#中 泛型知识点
//设计模式 单例模式的知识点
//继承了 MonoBehaviour 的 单例模式对象 需要我们自己保证它的位移性
public class SingletonMono<T> : MonoBehaviour where T: SingletonMono<T>
{
    private static T instance;

    //public T Instance
    //{
    //    get
    //    {
    //        //继承了Mono的脚本 不能够直接new
    //        //只能通过拖动到对象上 或者 通过 加脚本的api AddComponent去加脚本
    //        //U3D内部帮助我们实例化它
    //        return instance;
    //    }
    //}

    ////public static T GetInstance()
    ////{
    ////    //继承了Mono的脚本 不能够直接new
    ////    //只能通过拖动到对象上 或者 通过 加脚本的api AddComponent去加脚本
    ////    //U3D内部帮助我们实例化它
    ////    return instance;
    ////}

    //protected virtual void Awake()
    //{
    //    instance = this as T;
    //}


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
                else
                    instance.Init();
            }
            return instance;
        }
    }

    protected void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            instance.Init();
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    protected virtual void Init()
    {

    }
	
}

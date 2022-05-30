using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

/// <summary>
/// 文本名字
/// </summary>
public enum ETextName
{
    ConfigMap_enum,
    ConfigMap_pf,
    ConfigMap_sprit,
}

/// <summary>
/// 资源映射管理器
/// </summary>
public class ResourceMappingSystem : BaseManager<ResourceMappingSystem>
{
    Dictionary<string, string> temp_dic = new Dictionary<string, string>();

    public Dictionary<string ,string > ResourceDic(ETextName fileName)
    {
        temp_dic.Clear();
        GetConfigFile(fileName, BuildMap);
        return temp_dic;
    }

    private void GetConfigFile(ETextName fileName, Action<string> handler)
    {
        string url;
#if UNITY_EDITOR || UNITY_STANDALONE//编译器或者PC
        url = "file://" + Application.dataPath + "/StreamingAssets/" + fileName.ToString() + ".txt";//路径
#elif UNITY_IPHONE
            url = "file://"+ Application.dataPath + "/Raw/" + fileName.ToString()+".txt";//iphone路径
#elif UNITY_ANDROID
            url = "jar:file://"+ Application.dataPath + "!/assets/" + fileName.ToString()+".txt";//Android路径
#endif

        MonoMgr.Instance.StartCoroutine(ImprotByURL(url, handler));
    }

    IEnumerator ImprotByURL(string url, Action<string> handler)
    {

        using (UnityWebRequest uwr = UnityWebRequest.Get(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(uwr.error);
                Debug.Log("必须检查配置文件，请用我自己的工具生成!!");
            }
            else
            {
                var text = uwr.downloadHandler.text;

                Reader(text, handler);
            }
            yield return null;
        }
    }

    /// <summary>
    /// 解析文件
    /// </summary>
    /// <param name="fileContent"></param>
    /// <param name="handler"></param>
    private void Reader(string fileContent, Action<string> handler)
    {

        //文件名=路径/r/n文件名 = 路径
        //StringReader字符串读取器，提供了逐行读取字符串功能
        using (StringReader reader = new StringReader(fileContent))
        {

            string line;
            //1.读一行，满足条件则解析
            while ((line = reader.ReadLine()) != null)
            {
                handler(line);
            }

        }//当程序退出using代码块，将自动调用reader.Dispose()方法释放内存空间
    }
    private void BuildMap(string line)
    {
        line = line.Trim();//去除空行
        string[] keyValue = line.Split('=');
        temp_dic.Add(keyValue[0].Replace(" ",""), keyValue[1]);
    }

    //TODU 缺少资源卸载的
    /*
    1.Resource.Load加载资源，会将其组件一起加载。
    2.Resources.UnloadUnusedAssets卸载所有没有引用的资源
    3.Resources.UnloadAsset卸载单个资源，只能卸载单个基础资源（如Texture，Sprite），不能卸载GameObject。
    4.Resources.UnloadAsset卸载，Sprite只会卸载Sprite组件，和Texture2D关联，不会卸载Texture2D图片，需要在调用Resources.UnloadUnusedAssets，才会卸载Texture2D图片。因此卸载Sprite后场景中引用了该Sprite的物体不受影响，只有再调用Resources.UnloadUnusedAssets后，图片信息才会丢失。
    5.Resources.UnloadAsset卸载，Texture2D图片会直接被卸载，场景的图片信息回直接丢失。
    6.Resources.UnloadUnusedAssets可以卸载内存中的GameObject，需要先把引用的变量设置为null
    7.已实例化的GameObject，用Destroy删除
    8.已经卸载的卸载单个基础资源，在内存中已经释放，但是引用还在，再次使用会自动加载
    ————————————————
    版权声明：本文为CSDN博主「夜小楼」的原创文章，遵循CC 4.0 BY-SA版权协议，转载请附上原文出处链接及本声明。
    原文链接：https://blog.csdn.net/wu87990686/article/details/106862525
    */
}

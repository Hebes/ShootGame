using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class UnityEditorHelper : Editor
{
    [MenuItem("我自己的工具/生成/prefab资源映射表")]
    static void Generate_pf_txt()
    {
        Generate(EResourceType.prefab, "prefab", new string[] { Config_ResLoadPaths.CommonPath_allRes }, ETextName.pfConfigMap);
    }

    [MenuItem("我自己的工具/生成/sprite资源映射表")]
    static void Generate_sprite_txt()
    {
        Generate(EResourceType.sprite, "png", new string[] { Config_ResLoadPaths.item_Sprite_Icon, Config_ResLoadPaths.item_Sprite_TargetPointIcon }, ETextName.spritConfigMap);
    }


    [MenuItem("我自己的工具/生成/enum生成")]
    [System.Obsolete]
    static void Generate_enum_txt()
    {
        string text = Generate_enum(ETextName.pfConfigMap, ETextName.enumConfigMap);
        BuildMap(text, ETextName.enumConfigMap);
    }


    /// <summary>
    /// 生成配置文件
    /// 资源映射表
    /// </summary>
    private static void Generate(EResourceType resourceType, string fileEnd, string[] path, ETextName fileName)
    {
        /*
         Unity AssetDatabase和Resources资源管理 https://blog.csdn.net/u014230923/article/details/51433455
         AssetDatabase :包含了只适用于编译器中操作资源的相关功能。
         StreamingAssets : Unity特殊目录之一, 该目录中的文件不会被压缩,平台通用文件夹
         该Application.persistentDataPath路径可以在运行时进行读写操作。
         */

        // 1.查找Resources文件夹下的prefab文件："t:prefab"，t固定prefab文件名称 "Assets/Resources"，Assets必须写，Resources需要找的文件夹
        string[] resFiles = AssetDatabase.FindAssets("t:" + resourceType.ToString(), path);

        for (int i = 0; i < resFiles.Length; i++)
        {
            resFiles[i] = AssetDatabase.GUIDToAssetPath(resFiles[i]);
            //2.生成对应关系
            string file_Name = Path.GetFileNameWithoutExtension(resFiles[i]);
            //Replace去除功能
            string filePath = resFiles[i].Replace("Assets/Resources/", string.Empty).Replace("." + fileEnd, string.Empty);
            resFiles[i] = file_Name.Replace(" ", "") + "=" + filePath;
        }
        if (Directory.Exists(Application.dataPath + "/StreamingAssets"))
        {
            Debug.Log("文件夹已存在");
            Debug.Log("文件已覆盖");
        }
        else
        {
            Debug.Log("文件夹不存在,正在创建中....");
            Directory.CreateDirectory(Application.dataPath + "/StreamingAssets");//创建
            Debug.Log("StreamingAssets,创建成功!");
            Debug.Log("文件已生成");
        }

        //3.写入文件
        //StreamingAssets个平台通用文件夹
        File.WriteAllLines(Application.dataPath + "/StreamingAssets/" + fileName.ToString() + ".txt", resFiles);

        //可写可不写  unity刷新
        AssetDatabase.Refresh();
    }


    /// <summary>
    /// enum生成专用
    /// </summary>
    [System.Obsolete]
    private static string Generate_enum(ETextName readFileName, ETextName outFileName)
    {

        string url;
#if UNITY_EDITOR || UNITY_STANDALONE//编译器或者PC
        url = "file://" + Application.dataPath + "/StreamingAssets/" + readFileName.ToString() + ".txt";//路径
#elif UNITY_IPHONE
            url = "file://"+ Application.dataPath + "/Raw/" + fileName.ToString()+".txt";//iphone路径
#elif UNITY_ANDROID
            url = "jar:file://"+ Application.dataPath + "!/assets/" + fileName.ToString()+".txt";//Android路径
#endif
        //读取
        WWW www = new WWW(url);
        while (true)
        {
            if (www.isDone)
                return www.text;
        }
    }

    /// <summary>
    /// 解析文件
    /// </summary>
    /// <param name="fileContent"></param>
    private static void BuildMap(string fileContent, ETextName outFileName)
    {

        //StringReader字符串读取器,提供了逐行读取字符串功能
        using (StringReader reader = new StringReader(fileContent))//当程序退出using代码块,将自动调用reader.Dispose();
        {
            string[] str = new string[100];
            string line;//先读一遍
            int a = 0;
            while ((line = reader.ReadLine()) != null)
            {
                string[] keyValue = line.Split('=');//解析
                str[a] = keyValue[0].Replace(" ", "") + ",";
                a++;
            }
            File.WriteAllLines(Application.dataPath + "/StreamingAssets/" + outFileName.ToString() + ".txt", str);
        }
    }

}

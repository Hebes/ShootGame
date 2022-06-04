using LitJson;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;
using System.Reflection;
using Newtonsoft.Json.Linq;

public class ConfigJsonDataCenter : BaseManager<ConfigJsonDataCenter>
{
    public Dictionary<int, ConfigItemData> configItemData;
}

public class Manage_JsonRead : SingletonMono_Continue<Manage_JsonRead>, Manage_Init
{
    private Dictionary<int, ConfigItemData> itemDataDic;

    private string outPath = "/Editor/Json/";//输出的路径
    private string json = "Config_Json/";

    public void Init()
    {
        ParsingJson();
        Debug.Log("Json加载完毕！");
    }

    public Dictionary<int, ConfigItemData> GetItemDataDic => itemDataDic;

    private void ParsingJson()
    {
        //参考：https://blog.csdn.net/U3DCoder/article/details/115464140
        string url = Application.dataPath + outPath + "Test1Dic.json";
        //Encoding endoning = Encoding.UTF8;//识别Json数据内容中文字段
        //StreamReader streamReader = new StreamReader(url, endoning);
        //string jsonData = streamReader.ReadToEnd();
        //Dictionary<string, List<Dictionary<string, object>>> jsonObject = JsonMapper.ToObject<Dictionary<string, List<Dictionary<string, object>>>>(jsonData);
        //Debug.Log(jsonData);

        //参考：https://www.shuzhiduo.com/A/x9J2nLYZJ6/ https://www.freesion.com/article/366528259/
        //StreamReader json = File.OpenText(url);
        //string input = json.ReadToEnd();
        //jsonObject = JsonMapper.ToObject<Dictionary<string, List<Test1>>>(input);
        //Debug.Log(input);

        var settings = new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        //TextAsset textAssets1 =Resources.Load<TextAsset>("Config/Test1Dic");
        //Debug.Log(textAssets1.name);
        TextAsset[] textAssets = ResMgr.Instance.LoadAllRes<TextAsset>(json);

        for (int i = 0; i < textAssets.Length; i++)
        {
            var o = JsonConvert.DeserializeObject<ConfigJsonDataCenter>(textAssets[i].text, settings);
            //反射 参考:https://blog.csdn.net/u013617851/article/details/124259124
            var type = typeof(ConfigJsonDataCenter);
            var fields = type.GetFields();
            for (var j = 0; j < fields.Length; j++)
            {
                FieldInfo field = fields[j];

                if (field.IsStatic) continue;

                var value = field.GetValue(o);

                if (value != null) field.SetValue(ConfigJsonDataCenter.Instance, value);
            }
        }
        itemDataDic = ConfigJsonDataCenter.Instance.configItemData;

        ConfigItemData configItemData = GetitemDataDic(1001);
    }
    //另外一种解析方法 参考：https://zhuanlan.zhihu.com/p/366711494


    /// <summary>
    /// 根据编号返回一个物体
    /// </summary>
    public ConfigItemData GetitemDataDic(int id)
    {
        foreach (var item in itemDataDic)
        {
            if (item.Value.id ==id)
                return item.Value;
        }
        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Res_Sprite_Manage :BaseManager<Res_Sprite_Manage>,MainGame_Init
{
    private Dictionary<string,Sprite> res_Sprite_Dic;

    public void Init()
    {
        res_Sprite_Dic = new Dictionary<string, Sprite>();
        MonoMgr.Instance.StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        Dictionary<string, string> temp_dic = ResourceMappingSystem.Instance.ResourceDic(ETextName.spritConfigMap);
        yield return temp_dic;
        //添加到物体字典
        foreach (var item in temp_dic)
            res_Sprite_Dic[item.Key] = ResMgr.Instance.LoadRes<Sprite>(item.Value);
    }

    public Sprite Get_sprite(ESprite eSprite)
    {
        if (!res_Sprite_Dic.TryGetValue(eSprite.ToString(), out Sprite sprite))
            Debug.LogError($"未找到名为:{eSprite.ToString()}的Sprite");
        return sprite;
    }
}

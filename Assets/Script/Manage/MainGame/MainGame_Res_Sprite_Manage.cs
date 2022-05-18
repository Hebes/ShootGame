using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame_Res_Sprite_Manage :BaseManager<MainGame_Res_Sprite_Manage>,MainGame_Init
{
    private Dictionary<string,Sprite> res_Sprite_Dic;

    public void Init()
    {
        res_Sprite_Dic = new Dictionary<string, Sprite>();

        Add_ResSpriteDic(Config_ResLoadPaths.item_Sprite_Icon);
        Add_ResSpriteDic(Config_ResLoadPaths.item_Sprite_Icon);
    }

    private void Add_ResSpriteDic(string path)
    {
        Sprite[] sprite = ResMgr.Instance.LoadAllRes<Sprite>(path);
        foreach (var item in sprite)
            res_Sprite_Dic[item.name] = item;
    }

    public Sprite Get_sprite(string spriteName)
    {
        if (!res_Sprite_Dic.TryGetValue(spriteName, out Sprite sprite))
            Debug.LogError($"未找到名为:{spriteName}的Sprite");
        return sprite;
    }
}

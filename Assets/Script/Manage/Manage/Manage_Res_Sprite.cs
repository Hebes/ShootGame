using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ESprite
{
    Border,
    BorderGrey,
    Coin,
    GradientVertical,
    HealthPotion,
    ManaPotion,
    Medkit,
    Sword,
    ApexMedkit,
    ArmorPing,
    ArmorTier3,
    AttackCircleIcon,
    AttackPingOutline,
    AttackPing,
    AttackingHereCircleIcon,
    DefendCircleIcon,
    EnemySeenCircleIcon,
    HelmetPing,
    HelmetTier2,
    LootingCircleIcon,
    MedkitPing,
    MoveCircleIcon,
    MovePingOutline,
    MovePingSide,
    MovePing,
    PingPole,
    WatchingHereCircleIcon,
}

public class Manage_Res_Sprite :BaseManager<Manage_Res_Sprite>,Manage_Init
{
    private Dictionary<string,Sprite> res_Sprite_Dic;

    public void Init()
    {
        res_Sprite_Dic = new Dictionary<string, Sprite>();
        MonoMgr.Instance.StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        Dictionary<string, string> temp_dic = ResourceMappingSystem.Instance.ResourceDic(ETextName.ConfigMap_sprit);
        yield return temp_dic;
        //添加到物体字典
        foreach (var item in temp_dic)
            res_Sprite_Dic[item.Key] = ResMgr.Instance.LoadRes<Sprite>(item.Value);
        Debug.Log("图片资源加载完毕！");
    }

    public Sprite Get_sprite(ESprite eSprite)
    {
        if (!res_Sprite_Dic.TryGetValue(eSprite.ToString(), out Sprite sprite))
            Debug.LogError($"未找到名为:{eSprite}的Sprite");
        return sprite;
    }

    public Sprite Get_sprite(string eSprite)
    {
        if (!res_Sprite_Dic.TryGetValue(eSprite, out Sprite sprite))
            Debug.LogError($"未找到名为:{eSprite}的Sprite");
        return sprite;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;
using System;
using CodeMonkey.Utils;
/// <summary>
/// 物体名称
/// </summary>
public enum EpfName
{
    MedicalBox,
    ChatBubble,
    pfBullet,
    pfBulletPhysics,
    pfSmoke,
    Bar_HP,
    pfDamagePopup,
    TargetPrefab,
    UI_Target,
}

enum EItemWorld
{
    Text,
}

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        ItemWorld itemWorld = 
            Manage_Res_pf.Instance.GetAndInstantiate(EpfName.MedicalBox, position, Quaternion.identity).GetComponent<ItemWorld>();
        itemWorld.SetItem(item);
        return itemWorld;
    }

    /// <summary>
    /// 拖拽物体
    /// </summary>
    /// <param name="dropPosition"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    {
        Vector3 randomDir = UtilsClass.GetRandomDir();
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir * 20f, item);//在世界中生成 20  丢的距离
        itemWorld.GetComponent<Rigidbody2D>().AddForce(randomDir * 40f, ForceMode2D.Impulse);//通过Rigidbody2D作用的力
        return itemWorld;
    }


    private Item item;
    private SpriteRenderer spriteRenderer;
    private Light2D light2D;
    private TextMeshPro textMeshPro;
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        light2D = GetComponentInChildren<Light2D>();
        textMeshPro = transform.Find(EItemWorld.Text.ToString()).GetComponent<TextMeshPro>();
    }
    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();//设置item图片
        light2D.color = item.GetColor();//设置item灯光颜色
        textMeshPro.text = item.amount > 1 ? item.amount.ToString() : string.Empty;
    }

    public Item GetItem() => item;
    public void DestroySelf() => Destroy(gameObject);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;
using System;
using CodeMonkey.Utils;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(GameManager.Instance.pfItemworld, position, Quaternion.identity);
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
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
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir * 8f, item);//在世界中生成
        itemWorld.GetComponent<Rigidbody2D>().AddForce(randomDir * 40f, ForceMode2D.Impulse);
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
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }
    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        textMeshPro.text = item.amount > 1 ? item.amount.ToString() : string.Empty;
    }
    public Item GetItem()
    {
        return item;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }


}

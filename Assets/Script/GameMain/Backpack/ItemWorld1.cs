using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
enum EItemWorld
{
    Text,
}
public class ItemWorld1 : MonoBehaviour
{
    public static ItemWorld1 SpawnItemWorld(Vector3 position, ConfigItemData item)
    {
        ItemWorld1 itemWorld =Instantiate(GameManager.Instance.itemWorld1, position, Quaternion.identity);
        itemWorld.name = item.iconName;
        itemWorld.SetItem(item);
        return itemWorld;
    }
    /// <summary>
    /// 拖拽物体
    /// </summary>
    /// <param name="dropPosition"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    public static ItemWorld1 DropItem(Vector3 dropPosition, ConfigItemData item)
    {
        Vector3 randomDir = UtilsClass.GetRandomDir();
        ItemWorld1 itemWorld = SpawnItemWorld(dropPosition + randomDir * 20f, item);//在世界中生成 20  丢的距离
        itemWorld.GetComponent<Rigidbody2D>().AddForce(randomDir * 40f, ForceMode2D.Impulse);//通过Rigidbody2D作用的力
        return itemWorld;
    }

    [SerializeField]
    private ConfigItemData item;
    private SpriteRenderer spriteRenderer;
    private Light2D light2D;
    private TextMeshPro textMeshPro;
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        light2D = GetComponentInChildren<Light2D>();
        textMeshPro = transform.Find(EItemWorld.Text.ToString()).GetComponent<TextMeshPro>();
    }
    /// <summary>
    /// 设置物品的属性
    /// </summary>
    /// <param name="item"></param>
    public void SetItem(ConfigItemData item)
    {
        this.item = item;
        spriteRenderer.sprite = Manage_Res_Sprite.Instance.Get_sprite(item.iconName);
        light2D.color = Config_Color.CShineHealthPotion;//设置item灯光颜色 
        textMeshPro.text = item.amount > 1 ? item.amount.ToString() : string.Empty;
    }

    public ConfigItemData GetItem() => item;
    public void DestroySelf() => Destroy(gameObject);
}

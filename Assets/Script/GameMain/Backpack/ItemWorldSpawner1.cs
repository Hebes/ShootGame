using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorldSpawner1 : MonoBehaviour
{
    [SerializeField]
    private int number;
    [SerializeField]
    private ConfigItemData item;

    private void Start()
    {
        ConfigItemData itemData = Manage_JsonRead.Instance.GetitemDataDic(number);
        if (!(itemData.amount <= 0)) ItemWorld1.SpawnItemWorld(transform.position, itemData);//设置物品
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorldSpawner : MonoBehaviour
{
    [SerializeField]
    private Item item;

    private void Start()
    {
        ItemWorld.SpawnItemWorld(transform.position, item);//设置物品
        Destroy(gameObject);
    }
}

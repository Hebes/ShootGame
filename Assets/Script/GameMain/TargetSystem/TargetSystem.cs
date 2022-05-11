using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CodeMonkey.Utils;
using TMPro;
using Tool;
using System;

public class TargetSystem : BaseManager<TargetSystem>
{
    private const float MOVE_ENEMY_DOUBLE_CLICK_TIME = .5f;
    private static float lastPingTime;

    private Ping lastPing;

    private List<Ping> pingList;

    public override void Init()
    {
        base.Init();
        pingList = new List<Ping>();
    }

    public void AddPing(Vector3 position, Transform parent)
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(UtilsClass.GetMouseWorldPosition(), Vector2.zero);//射线检测点击的
        if (raycastHit.collider != null)
        {
            // Hit something 
            ItemIdentifier itemIdentifier = raycastHit.collider.gameObject.GetComponent<ItemIdentifier>();
            if (itemIdentifier != null)
            {
                // Clicked on an Item 点击一个物体
                AddPing(new Ping(Ping.Type.Item, position, itemIdentifier.itemType), parent);
            }
        }
        else
        {
            if (lastPing != null && lastPing.GetPingType == Ping.Type.Move)
            {
                // Last ping was a Move ping 最后一个ping是移动ping
                if (Time.time < lastPingTime + MOVE_ENEMY_DOUBLE_CLICK_TIME)
                {
                    // Pings in quick succession 连续的ping信号
                    DestroyPing(lastPing);
                    AddPing(new Ping(Ping.Type.Enemy, position), parent);

                }
                else
                {
                    AddPing(new Ping(Ping.Type.Move, position), parent);
                }
            }
            else
            {
                AddPing(new Ping(Ping.Type.Move, position), parent);
            }
        }

        
    }

    /// <summary>
    /// 加载TargetSystemPrefab物体
    /// </summary>
    /// <param name="unityAction"></param>
    public void AddPing(Ping ping, Transform parent)
    {
        pingList.Add(ping);

        //TUDO 需要重构这段代码
        ResourceRequest goRR = Resources.LoadAsync<Transform>(Config_ResourcesLoadPaths.targetSystem_Prefab_TargetSystemIcon_Prefab_TargetSystemPrefab);

        Transform pingTransform = (Transform)UnityEngine.Object.Instantiate(goRR.asset, ping.GetPosition, Quaternion.identity, parent);

        switch (ping.GetPingType)
        {
            default:
            case Ping.Type.Move:
                break;
            case Ping.Type.Enemy:
                pingTransform.Find_Child<SpriteRenderer>("TargetBody").sprite = GameManager.Instance.pingEnemySprite;
                pingTransform.Find_Child<TextMeshPro>("Targe_Text").color = GameManager.Instance.pingEnemyColor;
                break;
            case Ping.Type.Item:
                pingTransform.Find_Child<SpriteRenderer>("TargetBody").sprite = ItemIdentifier.Instance.GetItemSprite(ping.GetItemType());
                pingTransform.Find_Child<TextMeshPro>("Targe_Text").color = ItemIdentifier.Instance.GetItemColor(ping.GetItemType());
                break;
        }

        ping.OnDestroyed += delegate (object sender, EventArgs e)
          {
              UnityEngine.Object.Destroy(pingTransform.gameObject);
          };


        UI_TargetWindow.Instance.AddPing(ping);

        lastPing = ping;
        lastPingTime = Time.time;
    }

    public void DestroyPing(Ping ping)
    {
        ping.DestroySelf();
        pingList.Remove(ping);
    }

    public void Update()
    {
        for (int i = 0; i < pingList.Count; i++)
        {
            Ping ping = pingList[i];
            if (Time.time > ping.GetDestroyTime())//大于10秒
            {
                // Time to destroy this ping
                DestroyPing(ping);//销毁并调用事件
                i--;
            }
        }
    }


    /*
    * Handles a single Ping
    * */
    public class Ping
    {
        public enum Type
        {
            Move,
            Enemy,
            Item,
        }
        public event EventHandler OnDestroyed;

        private Type type;
        private Vector3 position;
        private ItemIdentifier.ItemType? itemType;
        private bool isDestroyed;
        private float destroyTime;

        public Ping(Type type, Vector3 position, ItemIdentifier.ItemType? itemType = null)
        {
            this.type = type;
            this.position = position;
            isDestroyed = false;
            destroyTime = Time.time + 10f;//10秒后消失
            this.itemType = itemType;
        }
        //public Type GetPingType()
        //{
        //    return type;
        //}

        public Type GetPingType
        {
            get { return type; }
        }

        public Vector3 GetPosition
        {
            get { return position; }
        }
        /// <summary>
        /// 执行绑定的事件
        /// </summary>
        public void DestroySelf()
        {
            isDestroyed = true;
            OnDestroyed?.Invoke(this, EventArgs.Empty);
        }
        public bool IsDestroyed()
        {
            return isDestroyed;
        }

        public float GetDestroyTime()
        {
            return destroyTime;
        }

        public ItemIdentifier.ItemType GetItemType()
        {
            return (ItemIdentifier.ItemType)itemType;
        }
    }

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(Config_Key.Key_Mouse_Right))
    //    {
    //        ResMgr.Instance.LoadAsync<GameObject>(Config_ResourcesLoadPaths.targetSystem_Prefab_TargetSystemIcon_Prefab_TargetSystemPrefab,
    //            (obj) => {
    //                obj.transform.position = UtilsClass.GetMouseWorldPosition();
    //                obj.transform.SetParent(transform);
    //                UI_TargetWindow.Instance.AddPing(obj.transform.position);
    //            });

    //    }
    //}
}




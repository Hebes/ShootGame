using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CodeMonkey.Utils;
using TMPro;
using Tool;
using System;
/// <summary>
/// 目标标记系统
/// 参考：https://www.youtube.com/watch?v=vcQe_0rtvzc&list=PLzDRvYVwl53v8c8YAZI3cQ7HmFAWRJYgs&index=6
/// </summary>
public class TargetSystem : BaseManager<TargetSystem>
{
    private const float MOVE_ENEMY_DOUBLE_CLICK_TIME = .5f;
    /// <summary>
    /// 显示PingWheel 界面的时间
    /// </summary>
    private const float PING_BUTTON_HOLDDOWN_WHEEL_SHOW_TIME = .5f;


    private static float lastPingTime;

    private Ping lastPing;

    private List<Ping> pingList;

    private float pingButtonHoldDownTimer;

    protected override void BaseManager_Init()
    {
        base.BaseManager_Init();
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
        Transform pingTransform =
            Manage_Res_pf.Instance.GetAndInstantiate(EpfName.TargetPrefab.ToString(), ping.GetPosition, Quaternion.identity, parent).GetComponent<Transform>();

        switch (ping.GetPingType)
        {
            default:
            case Ping.Type.Move:
                break;
            case Ping.Type.Enemy:
                SetColor(pingTransform, Res_Sprite_Manage.Instance.Get_sprite(ESprite.AttackPing), Config_Color.CEnemy);
                break;
            case Ping.Type.Item:
                SetColor(pingTransform, ItemIdentifier.Instance.GetItemSprite(ping.GetItemType()), ItemIdentifier.Instance.GetItemColor(ping.GetItemType()));
                break;
            case Ping.Type.Looting:
                SetColor(pingTransform, Res_Sprite_Manage.Instance.Get_sprite(ESprite.LootingCircleIcon), Config_Color.CLooting);
                break;
            case Ping.Type.Attacking:
                SetColor(pingTransform, Res_Sprite_Manage.Instance.Get_sprite(ESprite.AttackingHereCircleIcon), Config_Color.CAttacking);
                break;
            case Ping.Type.GoingHere:
                SetColor(pingTransform, Res_Sprite_Manage.Instance.Get_sprite(ESprite.MoveCircleIcon), Config_Color.CGoingHere);
                break;
            case Ping.Type.Defend:
                SetColor(pingTransform, Res_Sprite_Manage.Instance.Get_sprite(ESprite.DefendCircleIcon), Config_Color.CDefend);
                break;
            case Ping.Type.Watching:
                SetColor(pingTransform, Res_Sprite_Manage.Instance.Get_sprite(ESprite.WatchingHereCircleIcon), Config_Color.CWatching);
                break;
            case Ping.Type.Enemyseen:
                SetColor(pingTransform, Res_Sprite_Manage.Instance.Get_sprite(ESprite.EnemySeenCircleIcon), Config_Color.CEnemyseen);
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

    private void SetColor(Transform transform ,Sprite sprite,Color color)
    {
        transform.Find_Child<SpriteRenderer>("TargetBody").sprite = sprite;
        transform.Find_Child<TextMeshPro>("Targe_Text").color = color;
    }

    public void DestroyLastPing()
    {
        DestroyPing(lastPing);
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
    public void PingButtonHeldDownUpdate()
    {
        pingButtonHoldDownTimer += Time.deltaTime;

        if (pingButtonHoldDownTimer> PING_BUTTON_HOLDDOWN_WHEEL_SHOW_TIME)
        {            
            if (!PingWheel.Instance.IsVisibleStatic)
                PingWheel.Instance.Show(UtilsClass.GetMouseWorldPosition());
        }
    }
    public void PingButtonReleased()
    {
        pingButtonHoldDownTimer = 0;
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
            Looting,
            Attacking,
            GoingHere,
            Defend,
            Watching,
            Enemyseen,

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
}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;

public class Player_Components : MonoBehaviour
{
    #region Player_Body
    private Animator player_Animator;
    private Rigidbody2D player_Rigidbody2D;
    private Animator player_MiniMapIcon_Animator;
    #endregion

    #region Player_Gun
    private Transform player_Gun_PlayerGun_Transform;
    private Transform player_Gun_PlayerGun_EndPoint;
    private Transform player_Gun_PlayerGun_ShootPoint;
    private Animator player_Gun_PlayerGun_Animator;
    #endregion

    #region Player_HP
    
    private Transform player_Hp_PlayerHPBar;
    #endregion

    #region 背包系统
    private Inventory inventory;
    //测试代码
    [SerializeField]
    private UI_Inventory uI_Inventory;
    #endregion

    private void Awake()
    {
        Player_GetComponent();

        //inventory = new Inventory();
        //uI_Inventory.SetInventory(inventory);

        //ItemWorld.SpawnItemWorld(new Vector3(20, 20), new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
        //ItemWorld.SpawnItemWorld(new Vector3(-20, 20), new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
        //ItemWorld.SpawnItemWorld(new Vector3(0, -20), new Item { itemType = Item.ItemType.Sword, amount = 1 });

    }

    /// <summary>
    /// 获取玩家组件
    /// </summary>
    private void Player_GetComponent()
    {
        //角色
        player_Animator = transform.Find_Child<Animator>(Config_Player.player_pf_Body);
        player_Rigidbody2D = transform.GetComponent<Rigidbody2D>();
        player_MiniMapIcon_Animator = transform.Find_Child<Animator>(Config_Player.player_pf_MiniMap);

        //角色的枪
        player_Gun_PlayerGun_Transform = transform.Find_Child<Transform>(Config_Player.player_pf_Gun_PlayerGun);
        player_Gun_PlayerGun_Animator = transform.Find_Child<Animator>(Config_Player.player_pf_Gun_PlayerGun);
        player_Gun_PlayerGun_EndPoint = transform.Find_Child<Transform>(Config_Player.player_pf_Gun_PlayerGun_EndPoint);
        player_Gun_PlayerGun_ShootPoint = transform.Find_Child<Transform>(Config_Player.player_pf_Gun_PlayerGun_ShootPoint);

        //角色的血条
        player_Hp_PlayerHPBar = transform.Find_Child<Transform>(Config_Player.player_HP_PlayerHPBar);
    }



    public Animator Player_Animator
    {
        get { return player_Animator; }
    }
    public Rigidbody2D Player_Rigidbody2D
    {
        get { return player_Rigidbody2D; }
    }
    public Transform Player_Transform
    {
        get { return transform; }
    }
    public Animator Player_MiniMapIcon_Animator
    {
        get { return player_MiniMapIcon_Animator; }
    }
    public Transform Player_Gun_PlayerGun_Transform
    {
        set {  player_Gun_PlayerGun_Transform = value; }
        get { return player_Gun_PlayerGun_Transform; }
    }
    public Animator Player_Gun_PlayerGun_Animator
    {
        get { return player_Gun_PlayerGun_Animator; }
    }
    public Transform Player_Hp_PlayerHPBar
    {
        get { return player_Hp_PlayerHPBar; }
    }
    public Transform Player_Gun_PlayerGun_EndPoint
    {
        get { return player_Gun_PlayerGun_EndPoint; }
    }
    public Transform Player_Gun_PlayerGun_ShootPoint
    {
        get { return player_Gun_PlayerGun_ShootPoint; }
    }

}

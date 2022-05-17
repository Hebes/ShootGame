using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMono_Temp<GameManager>
{
    private Camera_Follow cameraFollow;

    [Tooltip("摄像机的位置兼玩家位置")]
    private Transform followTransform;

    [SerializeField]
    [Tooltip("用鼠标定位摄像机")]
    private bool cameraPositionWithMouse;


    protected override void Awake()
    {
        cameraFollow = Camera_Follow.Instance;
        followTransform = GameObject.Find("Player").GetComponent<Player_Components>().Player_Transform;


        ////初始化资源加载
        //List<MainGame_Init> mainGame_Inits = new List<MainGame_Init>()
        //{
        //    MainGame_ResManage.Instance,
        //};

        //foreach (var item in mainGame_Inits)
        //{
        //    item.Init();
        //}
    }

    private void Start()
    {
        cameraFollow.Setup(GetCameraPosition, () => 70f, true, true);//70为摄像机的大小
    }

    /// <summary>
    /// 获取摄像机位置
    /// </summary>
    /// <returns></returns>
    private Vector3 GetCameraPosition()
    {
        if (cameraPositionWithMouse)
        {
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            Vector3 playerToMouseDirection = mousePosition - followTransform.position;
            return followTransform.position + playerToMouseDirection * .3f;
        }
        else
        {
            return followTransform.position;
        }
    }


    //以下为测试代码
    public Color pingMoveColor;
    public Color pingEnemyColor;
    public Sprite pingMoveSprite;//移动图标
    public Sprite pingEnemySprite;//敌人图标


    public Color pingHelmetColor;
    public Color pingArmorColor;
    public Sprite pingMedkitSprite;//医疗箱的图标
    public Sprite pingHelmetSprite;//头盔的图标
    public Sprite pingArmorSprite;//盔甲的图标


    public Sprite pingLootingSprite;//敌人图标
    public Sprite pingAttackingSprite;//敌人图标
    public Sprite pingGoingHereSprite;//敌人图标
    public Sprite pingDefendSprite;//敌人图标
    public Sprite pingWatchingSprite;//敌人图标
    public Sprite pingEnemyseenSprite;//敌人图标


    //以下为物品测试代码
    public Sprite swordSprite;
    public Sprite healthPotionSprite;
    public Sprite manaPotionSprite;
    public Sprite coinSprite;
    public Sprite medkitSprite;

    public Transform pfItemworld;

}

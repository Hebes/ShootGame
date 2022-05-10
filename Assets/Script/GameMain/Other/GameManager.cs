using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMono<GameManager>
{
    private Camera_Follow cameraFollow;

    [Tooltip("摄像机的位置兼玩家位置")]
    private Transform followTransform;

    [SerializeField]
    [Tooltip("用鼠标定位摄像机")]
    private bool cameraPositionWithMouse;

    protected override void Init()
    {
        base.Init();
        cameraFollow = Camera_Follow.Instance;
        followTransform = Player_Manager.Instance.Player_Transform;
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


    public Color pingMoveColor;
    public Color pingEnemyColor;

    //以下为测试代码
    public Sprite pingMoveSprite;//移动图标
    public Sprite pingEnemySprite;//敌人图标
}

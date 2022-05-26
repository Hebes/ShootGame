using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum ECursorType
{
    /// <summary>
    /// 默认箭头
    /// </summary>
    Arrow,
    /// <summary>
    /// 抓取
    /// </summary>
    Grab,
    /// <summary>
    /// 选择
    /// </summary>
    Select,
    /// <summary>
    /// 攻击
    /// </summary>
    Attack,
    /// <summary>
    /// 移动
    /// </summary>
    Move,
}

/// <summary>
/// 光标管理
/// 参考：https://www.youtube.com/watch?v=8Fm37H1Mwxw&list=RDCMUCFK6NCbuCIVzA6Yj1G_ZqCg&start_radio=1
/// </summary>
public class CursorManager : SingletonMono_Continue<CursorManager>
{
    private List<CursorAnimation> cursorList;
    private CursorAnimation cursorAnimation;

    private int currentFrame;
    private float frameTimer;
    private int frameCount;
        
    protected override void Awake()
    {
        cursorList = new List<CursorAnimation>() {
        new CursorAnimation{
            cursorType = ECursorType.Arrow,//默认鼠标图标
            textureArray = ResMgr.Instance.LoadAllRes<Texture2D>(Config_ResLoadPaths.cursor_Icons_Arrow),
            frameRate =.1f,
            offset = new Vector2(4,4)},//图片的最左上角到箭头的差距坐标 原先是10
        new CursorAnimation{
            cursorType = ECursorType.Grab,//抓取
            textureArray = ResMgr.Instance.LoadAllRes<Texture2D>(Config_ResLoadPaths.cursor_Icons_Grab),
            frameRate =.05f,
            offset = new Vector2(4,4)},//图片的最左上角到箭头的差距坐标 原先是16
        new CursorAnimation{
            cursorType = ECursorType.Select,//选择
            textureArray = ResMgr.Instance.LoadAllRes<Texture2D>(Config_ResLoadPaths.cursor_Icons_Unit),
            frameRate =1f,
            offset = new Vector2(4,4)},//图片的最左上角到箭头的差距坐标
        new CursorAnimation{
            cursorType = ECursorType.Attack,//攻击
            textureArray = ResMgr.Instance.LoadAllRes<Texture2D>(Config_ResLoadPaths.cursor_Icons_Attack),
            frameRate =.1f,
            offset = new Vector2(16,16)},//图片的最左上角到箭头的差距坐标
        new CursorAnimation{
            cursorType = ECursorType.Move,//移动
            textureArray = ResMgr.Instance.LoadAllRes<Texture2D>(Config_ResLoadPaths.cursor_Icons_Move),
            frameRate =.2f,
            offset = new Vector2(4,4)},//图片的最左上角到箭头的差距坐标
        };

        SetActiveCursorType(ECursorType.Arrow);//初始默认设置箭头图标
    }

    private void Update()
    {
        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0f)
        {
            frameTimer += cursorAnimation.frameRate;//帧频
            currentFrame = (currentFrame + 1) % frameCount;
            Cursor.SetCursor(cursorAnimation.textureArray[currentFrame], cursorAnimation.offset, CursorMode.Auto);//设置鼠标标签
        }
    }

    /// <summary>
    /// 设置活动的标签类型
    /// </summary>
    /// <param name="cursorType"></param>
    public void SetActiveCursorType(ECursorType cursorType) 
        => SetActiveCursorAnimation(GetCursorAnimation(cursorType));
    /// <summary>
    /// 获取标签的动画
    /// </summary>
    /// <param name="cursorType">标签类型</param>
    /// <returns></returns>
    private CursorAnimation GetCursorAnimation(ECursorType cursorType)
    {
        foreach (CursorAnimation cursorAnimation in cursorList)
            if (cursorAnimation.cursorType == cursorType) return cursorAnimation;
        // 找不到这个CursorType！
        return null;
    }


    /// <summary>
    /// 设置活动的标签动画
    /// </summary>
    /// <param name="cursorAnimation"></param>
    private void SetActiveCursorAnimation(CursorAnimation cursorAnimation)
    {
        this.cursorAnimation = cursorAnimation;
        currentFrame = 0;
        frameTimer = cursorAnimation.frameRate;
        frameCount = cursorAnimation.textureArray.Length;//动画的长度
    }


    [Serializable]
    public class CursorAnimation
    {
        public ECursorType cursorType;
        public Texture2D[] textureArray;
        public float frameRate;
        public Vector2 offset;
    }
}

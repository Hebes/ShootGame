using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// 光标管理
/// 参考：https://www.youtube.com/watch?v=8Fm37H1Mwxw&list=RDCMUCFK6NCbuCIVzA6Yj1G_ZqCg&start_radio=1
/// </summary>
public class CursorManager : SingletonMono<CursorManager>
{
    //public event EventHandler<OnCursorChangedEventArgs> OnCursorChanged;
    //public class OnCursorChangedEventArgs : EventArgs
    //{
    //    public CursorType cursorType;
    //}


    //TUDO 写一个光标全部加载的方法
    [SerializeField] private List<CursorAnimation> cursorAnimationList;
    private CursorAnimation cursorAnimation;

    private int currentFrame;
    private float frameTimer;
    private int frameCount;

    void Start()
    {
        SetActiveCursorType(CursorType.Arrow);//默认设置箭头图标
    }

    private void Update()
    {
        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0f)
        {
            frameTimer += cursorAnimation.frameRate;
            currentFrame = (currentFrame + 1) % frameCount;
            Cursor.SetCursor(cursorAnimation.textureArray[currentFrame], cursorAnimation.offset, CursorMode.Auto);
        }
    }

    public void SetActiveCursorType(CursorType cursorType)
    {
        SetActiveCursorAnimation(GetCursorAnimation(cursorType));
        //OnCursorChanged?.Invoke(this, new OnCursorChangedEventArgs { cursorType = cursorType });
    }
    private CursorAnimation GetCursorAnimation(CursorType cursorType)
    {
        foreach (CursorAnimation cursorAnimation in cursorAnimationList)
            if (cursorAnimation.cursorType == cursorType) return cursorAnimation;
        // Couldn't find this CursorType !
        return null;
    }


    private void SetActiveCursorAnimation(CursorAnimation cursorAnimation)
    {
        this.cursorAnimation = cursorAnimation;
        currentFrame = 0;
        frameTimer = cursorAnimation.frameRate;
        frameCount = cursorAnimation.textureArray.Length;//动画的长度
    }


    [System.Serializable]
    public class CursorAnimation
    {
        public CursorType cursorType;
        public Texture2D[] textureArray;
        public float frameRate;
        public Vector2 offset;
    }
}




public enum CursorType
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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// UI层级
/// </summary>
public enum E_UI_Layer
{
    OneLayer,
    TwoLayer,
    ThreeLayer,
    SystemLayer,
}

/// <summary>
/// UI管理器
/// 1.管理所有显示的面板
/// 2.提供给外部 显示和隐藏等等接口
/// </summary>
public class UIManager : BaseManager<UIManager>
{
    public Dictionary<string, BasePanel> panelDic ;

    private Transform OneLayer;
    private Transform TwoLayer;
    private Transform ThreeLayer;
    private Transform SystemLayer;

    //记录我们UI的Canvas父对象 方便以后外部可能会使用它
    public RectTransform canvas;

    public override void Init()
    {
        base.Init();
        panelDic = new Dictionary<string, BasePanel>();
        //创建Canvas 让其过场景的时候 不被移除
        GameObject obj = ResMgr.Instance.Load<GameObject>(Config_ResLoadPaths.PREFAB_UI_BASE_CANVAS);
        canvas = obj.transform as RectTransform;
        Object.DontDestroyOnLoad(obj);

        //找到各层
        OneLayer = canvas.Find(Config_ResLoadPaths.CANVAS_ONELAYER);
        TwoLayer = canvas.Find(Config_ResLoadPaths.CANVAS_TWOLAYER);
        ThreeLayer = canvas.Find(Config_ResLoadPaths.CANVAS_THREELAYER);
        SystemLayer = canvas.Find(Config_ResLoadPaths.CANVAS_SYSTEMLAYER);

        //创建EventSystem 让其过场景的时候 不被移除
        obj = ResMgr.Instance.Load<GameObject>(Config_ResLoadPaths.PREFAB_UI_BASE_EVENTSYSTEM);
        Object.DontDestroyOnLoad(obj);
    }

    /// <summary>
    /// 通过层级枚举 得到对应层级的父对象
    /// </summary>
    /// <param name="layer"></param>
    /// <returns></returns>
    public Transform UI_GetLayerFather(E_UI_Layer layer)
    {
        switch (layer)
        {
            case E_UI_Layer.OneLayer:
                return this.OneLayer;
            case E_UI_Layer.TwoLayer:
                return this.TwoLayer;
            case E_UI_Layer.ThreeLayer:
                return this.ThreeLayer;
            case E_UI_Layer.SystemLayer:
                return this.SystemLayer;
        }
        return null;
    }

    /// <summary>
    /// 显示面板
    /// </summary>
    /// <typeparam name="T">面板脚本类型</typeparam>
    /// <param name="panelName">面板名</param>
    /// <param name="layer">显示在哪一层</param>
    /// <param name="callBack">当面板预设体创建成功后你想做的事</param>
    public void UI_ShowPanel<T>(string panelName, E_UI_Layer layer = E_UI_Layer.TwoLayer, UnityAction<T> callBack = null) where T : BasePanel
    {
        if (panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].gameObject.SetActive(true);

            panelDic[panelName].ShowMe();

            // 处理面板创建完成后的逻辑
            callBack?.Invoke(panelDic[panelName] as T);

            //避免面板重复加载 如果存在该面板 即直接显示 调用回调函数后  直接return 不再处理后面的异步加载逻辑
            return;
        }

        ResMgr.Instance.LoadAsync<GameObject>(Config_ResLoadPaths.PREFAB_UI_UIPANEL + panelName, (obj) =>
        {
            //把他作为 Canvas的子对象
            //并且 要设置它的相对位置
            //找到父对象 你到底显示在哪一层
            Transform father = OneLayer;
            switch (layer)
            {
                case E_UI_Layer.TwoLayer:
                    father = TwoLayer;
                    break;
                case E_UI_Layer.ThreeLayer:
                    father = ThreeLayer;
                    break;
                case E_UI_Layer.SystemLayer:
                    father = SystemLayer;
                    break;
            }
            //设置父对象  设置相对位置和大小
            obj.transform.SetParent(father);

            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;

            (obj.transform as RectTransform).offsetMin = (obj.transform as RectTransform).offsetMax = Vector2.zero;

            if (obj.GetComponent<T>() == null) obj.AddComponent<T>();

            //得到预设体身上的面板脚本
            T panel = obj.GetComponent<T>();

            // 处理面板创建完成后的逻辑
            if (callBack != null) callBack(panel);

            panel.ShowMe();//调用ShowMe方法，如果子面板重写的把

            //把面板存起来
            panelDic.Add(panelName, panel);
        });
    }

    /// <summary>
    /// 移除面板
    /// </summary>
    /// <param name="panelName"></param>
    public void UI_RemovePanel(string panelName)
    {
        if (panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].RemoveMe();//如果子类重写那么调用子类重写的RemoveMe方法
            GameObject.Destroy(panelDic[panelName].gameObject);
            panelDic.Remove(panelName);
        }
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    public void UI_HidePanel(string panelName)
    {
        if (panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].HideMe();
            panelDic[panelName].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 得到某一个已经显示的面板 方便外部使用
    /// </summary>
    public T UI_GetPanel<T>(string name) where T : BasePanel
    {
        if (panelDic.ContainsKey(name)) return panelDic[name] as T;
        return null;
    }

    /// <summary>
    /// 移除其他所有面板，打开一个面板
    /// </summary>
    /// <typeparam name="T">面板脚本类型</typeparam>
    /// <param name="panelName">面板名</param>
    /// <param name="layer">显示在哪一层</param>
    /// <param name="callBack">当面板预设体创建成功后你想做的事</param>
    public void UI_OpenOneRemoveOhterPanel<T>(string panelName,
        E_UI_Layer layer = E_UI_Layer.TwoLayer,
        UnityAction<T> callBack = null) where T : BasePanel
    {
        //显示需要的那个
        UI_ShowPanel<T>(panelName, layer, callBack);
        //移除其他的

        for (int i = panelDic.Count - 1; i >= 0; i--)
        {
            var item = panelDic.ElementAt(i);
            UI_RemovePanel(item.Key);
        }
    }

    /// <summary>
    /// 隐藏其他所有面板，打开一个面板
    /// </summary>
    /// <typeparam name="T">面板脚本类型</typeparam>
    /// <param name="panelName">面板名</param>
    /// <param name="layer">显示在哪一层</param>
    /// <param name="callBack">当面板预设体创建成功后你想做的事</param>
    public void UI_OpenOneHideOhterPanel<T>(string panelName,
        E_UI_Layer layer = E_UI_Layer.TwoLayer,
        UnityAction<T> callBack = null) where T : BasePanel
    {
        UI_ShowPanel<T>(panelName, layer, callBack);
        for (int i = panelDic.Count - 1; i >= 0; i--)
        {
            var item = panelDic.ElementAt(i);
            UI_HidePanel(item.Key);
        }
    }

    /// <summary>
    /// 删除所有的UI面板
    /// </summary>
    public void UI_PanelAllClear()
    {
        for (int i = panelDic.Count - 1; i >= 0; i--)
        {
            var item = panelDic.ElementAt(i);
            GameObject.Destroy(panelDic[item.Key].gameObject);
            panelDic.Remove(item.Key);

        }
        panelDic.Clear();
    }
    /// <summary>
    /// 给控件添加自定义事件监听
    /// </summary>
    /// <param name="control">控件对象</param>
    /// <param name="type">事件类型</param>
    /// <param name="callBack">事件的响应函数</param>
    public static void AddCustomEventListener(UIBehaviour control, EventTriggerType type, UnityAction<BaseEventData> callBack)
    {
        EventTrigger trigger = control.GetComponent<EventTrigger>();

        if (trigger == null) trigger = control.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(callBack);

        trigger.triggers.Add(entry);
    }

}

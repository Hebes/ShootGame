using System;

[Serializable]
public class ConfigItemData
{
    /// <summary>
    /// 编号
    /// </summary>
    public int id;

    /// <summary>
    /// 物品名字
    /// </summary>
    public string name;

    /// <summary>
    /// 图标名称
    /// </summary>
    public string iconName;

    /// <summary>
    /// 物品介绍
    /// </summary>
    public string explain;

    /// <summary>
    /// 是否可堆叠
    /// </summary>
    public bool isStackable;

    /// <summary>
    /// 数量
    /// </summary>
    public int amount;

    public IItemHolder itemHolder;
}

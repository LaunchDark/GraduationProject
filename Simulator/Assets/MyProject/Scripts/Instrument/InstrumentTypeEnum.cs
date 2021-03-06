using System.ComponentModel;

public enum InstrumentTypeEnum
{ 
    /// <summary>
    /// 全部
    /// </summary>
    [Description("全部")]
    All = 0,

    /// <summary>
    /// 桌子
    /// </summary>
    [Description("桌子")]
    Desk,

    /// <summary>
    /// 椅子沙发
    /// </summary>
    [Description("椅子沙发")]
    Chair,

    /// <summary>
    /// 床
    /// </summary>
    [Description("床")]
    Bed,

    /// <summary>
    /// 橱窗
    /// </summary>
    [Description("橱窗")]
    ShopWindow,

    /// <summary>
    /// 灯饰
    /// </summary>
    [Description("灯饰")]
    Light,

    /// <summary>
    /// 其他
    /// </summary>
    [Description("其他")]
    Other,


}
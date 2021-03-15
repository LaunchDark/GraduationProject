using System.ComponentModel;

public enum InstrumentEnum
{
    [Description("空")]
    None = 0,

    #region 测试用
    /// <summary>
    /// 测试方块
    /// </summary>
    [Description("测试方块")]
    Cube =1,

    /// <summary>
    /// 测试小球
    /// </summary>
    [Description("测试小球")]
    Sphere =2,

    /// <summary>
    /// 测试复合体
    /// </summary>
    [Description("测试复合体")]
    Double = 3,

    /// <summary>
    /// 吊灯
    /// </summary>
    [Description("测试吊灯")]
    HangLight = 4,

    #endregion

    #region 床
    /// <summary>
    /// 抱枕1
    /// </summary>
    [Description("抱枕1")]
    抱枕1,

    /// <summary>
    /// 抱枕2
    /// </summary>
    [Description("抱枕2")]
    抱枕2,

    /// <summary>
    /// 枕头1
    /// </summary>
    [Description("枕头1")]
    枕头1,

    /// <summary>
    /// 床1
    /// </summary>
    [Description("床1")]
    床1,
    #endregion

    #region 柜子
    /// <summary>
    /// 床头柜1
    /// </summary>
    [Description("床头柜1")]
    床头柜1,

    /// <summary>
    /// 电视柜1
    /// </summary>
    [Description("电视柜1")]
    电视柜1,
    #endregion

    #region 桌子
    /// <summary>
    /// 长桌1
    /// </summary>
    [Description("长桌1")]
    长桌1,

    /// <summary>
    /// 圆桌1
    /// </summary>
    [Description("圆桌1")]
    圆桌1,
    #endregion

    #region 椅子
    /// <summary>
    /// 椅子1
    /// </summary>
    [Description("椅子1")]
    椅子1,

    /// <summary>
    /// 椅子2
    /// </summary>
    [Description("椅子2")]
    椅子2,

    /// <summary>
    /// 椅子3
    /// </summary>
    [Description("椅子3")]
    椅子3,

    /// <summary>
    /// 沙发1
    /// </summary>
    [Description("沙发1")]
    沙发1,
    #endregion

    #region 电器
    /// <summary>
    /// 电视1
    /// </summary>
    [Description("电视1")]
    电视1,

    /// <summary>
    /// 音响
    /// </summary>
    [Description("音响")]
    音响,
    #endregion

    #region 灯饰

    #endregion

    #region 饰品
    /// <summary>
    /// 画1
    /// </summary>
    [Description("画1")]
    画1,

    #endregion

    #region 其他
    /// <summary>
    /// 门
    /// </summary>
    [Description("门")]
    门,

    /// <summary>
    /// 墙
    /// </summary>
    [Description("墙")]
    墙,

    #endregion

}

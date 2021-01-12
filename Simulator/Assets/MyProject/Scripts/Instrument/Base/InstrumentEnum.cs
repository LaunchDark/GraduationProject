using System.ComponentModel;

public enum InstrumentEnum
{
    [Description("空")]
    None = 0,



    /// <summary>
    /// 基准站
    /// </summary>
    [Description("测试方块")]
    Cube =1,


    /// <summary>
    /// 移动站
    /// </summary>
    [Description("测试小球")]
    Sphere =2,


    /// <summary>
    /// 测钉
    /// </summary>
    [Description("测钉")]
    Nail = 3,


    /// <summary>
    /// 全站仪
    /// </summary>
    [Description("全站仪")]
    wzQzy =4,


    /// <summary>
    /// 棱镜
    /// </summary>
    [Description("棱镜")]
    Lj =5,


    /// <summary>
    /// 对中杆
    /// </summary>
    [Description("对中杆")]
    Dzg =6,


    /// <summary>
    /// 三脚架
    /// </summary>
    [Description("三脚架")]
    Sjj =7,


    /// <summary>
    /// 连接杆
    /// </summary>
    [Description("连接杆")]
    ConnectGun,


    /// <summary>
    /// 全站仪552R15
    /// </summary>
    [Description("全站仪552r15")]
    QZY_552r15,


    /// <summary>
    /// RTK银河1plush
    /// </summary>
    [Description("RTK")]
    RtkOnePlush,


    /// <summary>
    /// 创享RTK
    /// </summary>
    [Description("RTK")]
    RtkCreatEnjoy,

    /// <summary>
    /// 测高片
    /// </summary>
    [Description("测高片")]
    HeightPiece,

    /// <summary>
    /// 托架
    /// </summary>
    [Description("托架")]
    Bracket,

    /// <summary>
    /// 碳纤杆
    /// </summary>
    [Description("碳纤杆")]
    Stick,


    /// <summary>
    /// 装载在RTK底部的天线
    /// </summary>
    [Description("底部天线")]
    BottomAntenna,


    /// <summary>
    ///  装载在RTK顶部的天线
    /// </summary>
    [Description("顶部天线")]
    TopAntenna,

    /// <summary>
    ///  DSZ3
    /// </summary>
    [Description("DSZ3")]
    DSZ3,

    /// <summary>
    ///  完整水准仪
    /// </summary>
    [Description("水准仪")]
    WZ_DSZ3,

    /// <summary>
    ///  DL2007
    /// </summary>
    [Description("DL2007")]
    DL2007,

    /// <summary>
    ///  完整DL-2007水准仪
    /// </summary>
    [Description("DL-2007水准仪")]
    WZ_DL2007,

    /// <summary>
    ///  木制水准尺4687
    /// </summary>
    [Description("水准尺4687")]
    Ruler_4687,

    /// <summary>
    ///  木制水准尺4787
    /// </summary>
    [Description("水准尺4787")]
    Ruler_4787,

    /// <summary>
    ///  尺垫
    /// </summary>
    [Description("尺垫")]
    RulerPad,

    /// <summary>
    ///  铟钢尺垫
    /// </summary>
    [Description("铟钢尺垫")]
    SteelRulerPad,

    /// <summary>
    ///  条码铟钢尺
    /// </summary>
    [Description("条码铟钢尺")]
    BarCodeSteelRuler,
}

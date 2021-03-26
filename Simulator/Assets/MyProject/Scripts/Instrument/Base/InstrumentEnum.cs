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

    /// <summary>
    /// 测试墙沿
    /// </summary>
    [Description("测试墙沿")]
    WallDecorate,

    /// <summary>
    /// 测试墙沿2
    /// </summary>
    [Description("测试墙沿2")]
    WallDecorate2,

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
    /// 枕头2
    /// </summary>
    [Description("枕头2")]
    枕头2,

    /// <summary>
    /// 床1
    /// </summary>
    [Description("床1")]
    床1,

    /// <summary>
    /// 床2
    /// </summary>
    [Description("床2")]
    床2,
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

    /// <summary>
    /// 衣柜1
    /// </summary>
    [Description("衣柜1")]
    衣柜1,
    #endregion

    #region 桌子
    /// <summary>
    /// 长桌1
    /// </summary>
    [Description("长桌1")]
    长桌1,

    /// <summary>
    /// 长桌2
    /// </summary>
    [Description("长桌2")]
    长桌2,

    /// <summary>
    /// 餐桌1
    /// </summary>
    [Description("餐桌1")]
    餐桌1,

    /// <summary>
    /// 圆桌1
    /// </summary>
    [Description("圆桌1")]
    圆桌1,

    /// <summary>
    /// 圆桌2
    /// </summary>
    [Description("圆桌2")]
    圆桌2,
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
    /// 餐椅1
    /// </summary>
    [Description("餐椅1")]
    餐椅1,

    /// <summary>
    /// 沙发1
    /// </summary>
    [Description("沙发1")]
    沙发1,

    /// <summary>
    /// 沙发2
    /// </summary>
    [Description("沙发2")]
    沙发2,

    /// <summary>
    /// 沙发3
    /// </summary>
    [Description("沙发3")]
    沙发3,
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

    /// <summary>
    /// 空调1
    /// </summary>
    [Description("空调1")]
    空调1,

    /// <summary>
    /// 电脑1
    /// </summary>
    [Description("电脑1")]
    电脑1,

    /// <summary>
    /// 冰箱1
    /// </summary>
    [Description("冰箱1")]
    冰箱1,

    /// <summary>
    /// 吊扇1
    /// </summary>
    [Description("吊扇1")]
    吊扇1,
    #endregion

    #region 灯饰
    /// <summary>
    /// 壁灯1
    /// </summary>
    [Description("壁灯1")]
    壁灯1,

    /// <summary>
    /// 顶灯1
    /// </summary>
    [Description("顶灯1")]
    顶灯1,

    /// <summary>
    /// 吊灯1
    /// </summary>
    [Description("吊灯1")]
    吊灯1,

    /// <summary>
    /// 台灯1
    /// </summary>
    [Description("台灯1")]
    台灯1,

    #endregion

    #region 饰品
    /// <summary>
    /// 画1
    /// </summary>
    [Description("画1")]
    画1,

    /// <summary>
    /// 杯子1
    /// </summary>
    [Description("杯子1")]
    杯子1,

    /// <summary>
    /// 花瓶1
    /// </summary>
    [Description("花瓶1")]
    花瓶1,

    /// <summary>
    /// 花瓶2
    /// </summary>
    [Description("花瓶2")]
    花瓶2,

    /// <summary>
    /// 花瓶3
    /// </summary>
    [Description("花瓶3")]
    花瓶3,

    /// <summary>
    /// 花瓶4
    /// </summary>
    [Description("花瓶4")]
    花瓶4,

    /// <summary>
    /// 花瓶5
    /// </summary>
    [Description("花瓶5")]
    花瓶5,

    /// <summary>
    /// 花瓶6
    /// </summary>
    [Description("花瓶6")]
    花瓶6,

    #endregion

    #region 卫生间
    /// <summary>
    /// 花洒1
    /// </summary>
    [Description("花洒1")]
    花洒1,

    /// <summary>
    /// 马桶1
    /// </summary>
    [Description("马桶1")]
    马桶1,

    /// <summary>
    /// 洗手盆1
    /// </summary>
    [Description("洗手盆1")]
    洗手盆1,

    #endregion

    #region 厨房
    /// <summary>
    /// 橱柜1
    /// </summary>
    [Description("橱柜1")]
    橱柜1,

    /// <summary>
    /// 灶台1
    /// </summary>
    [Description("灶台1")]
    灶台1,

    /// <summary>
    /// 水壶1
    /// </summary>
    [Description("水壶1")]
    水壶1,

    /// <summary>
    /// 厨具1
    /// </summary>
    [Description("厨具1")]
    厨具1,

    /// <summary>
    /// 厨具2
    /// </summary>
    [Description("厨具2")]
    厨具2,
    #endregion

    #region 其他
    /// <summary>
    /// 门
    /// </summary>
    [Description("门")]
    门,

    /// <summary>
    /// 窗
    /// </summary>
    [Description("窗")]
    窗,

    /// <summary>
    /// 窗台
    /// </summary>
    [Description("窗台")]
    窗台,

    /// <summary>
    /// 窗帘
    /// </summary>
    [Description("窗帘")]
    窗帘,

    /// <summary>
    /// 门框
    /// </summary>
    [Description("门框")]
    门框,

    /// <summary>
    /// 地毯1
    /// </summary>
    [Description("地毯1")]
    地毯1,

    /// <summary>
    /// 垃圾桶1
    /// </summary>
    [Description("垃圾桶1")]
    垃圾桶1,
    #endregion

    #region 建筑
    /// <summary>
    /// 墙
    /// </summary>
    [Description("墙")]
    墙,

    /// <summary>
    /// 地板
    /// </summary>
    [Description("地板")]
    地板,

    /// <summary>
    /// 天花板
    /// </summary>
    [Description("天花板")]
    天花板,

    #endregion

}

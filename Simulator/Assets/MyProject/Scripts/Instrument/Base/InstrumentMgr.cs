//using DG.Tweening.Plugins.Core.PathCore;
//using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;


/// <summary>
/// 物品管理器
/// </summary>
public class InstrumentMgr : MonoBehaviour
{
    static InstrumentMgr instance;
    public static InstrumentMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("InstrumentMgr").AddComponent<InstrumentMgr>();
            }
            return instance;
        }
    }

    Dictionary<InstrumentEnum, List<GameObject>> instrumentGameObjectDic = new Dictionary<InstrumentEnum, List<GameObject>>();

    private void Awake()
    {

    }

    public GameObject CreateInstrument(InstrumentEnum instrumentEnum,bool isPool = true)
    {
        GameObject go = GetInstrument(instrumentEnum);
        if (isPool == false)
            go = null;
        if (go == null)
        {
            string path = GetInstrumentResPath(instrumentEnum);
            if (path != null)
            {
                go = UITool.Instantiate(path);
                instrumentGameObjectDic[instrumentEnum].Add(go);
            }
        }
        go.GetComponent<Instrument>().subInstrument = null;
        return go;
    }

    private GameObject GetInstrument(InstrumentEnum instrumentEnum)
    {
        if (!instrumentGameObjectDic.ContainsKey(instrumentEnum))
        {
            instrumentGameObjectDic.Add(instrumentEnum, new List<GameObject>());
            return null;
        }
        List<GameObject> goList = instrumentGameObjectDic[instrumentEnum];
        for (int i = 0; i < goList.Count; i++)
        {
            if (goList[i].activeSelf == false)
                return goList[i];
        }
        return null;
    }

    private string GetInstrumentResPath(InstrumentEnum instrumentEnum)
    {
        switch (instrumentEnum)
        {
            #region 测试
            case InstrumentEnum.门:
                return "Instruments/测试门";
            case InstrumentEnum.Cube:
                return "Instruments/测试方块";
            case InstrumentEnum.Sphere:
                return "Instruments/测试小球";
            case InstrumentEnum.HangLight:
                return "Instruments/测试吊灯";
            case InstrumentEnum.Double:
                return "Instruments/测试复合体";
            #endregion

            #region 床
            case InstrumentEnum.抱枕1:
                return "Instruments/床/抱枕1";
            case InstrumentEnum.抱枕2:
                return "Instruments/床/抱枕2";
            case InstrumentEnum.枕头1:
                return "Instruments/床/枕头1";
            case InstrumentEnum.床1:
                return "Instruments/床/床1";
            #endregion

            #region 柜子
            case InstrumentEnum.床头柜1:
                return "Instruments/柜子/床头柜1";
            case InstrumentEnum.电视柜1:
                return "Instruments/柜子/电视柜1";
            #endregion

            #region 桌子
            case InstrumentEnum.长桌1:
                return "Instruments/桌子/长桌1";
            case InstrumentEnum.圆桌1:
                return "Instruments/桌子/圆桌1";
            #endregion

            #region 椅子
            case InstrumentEnum.椅子1:
                return "Instruments/椅子/椅子1";
            case InstrumentEnum.椅子2:
                return "Instruments/椅子/椅子2";
            case InstrumentEnum.椅子3:
                return "Instruments/椅子/椅子3";
            case InstrumentEnum.沙发1:
                return "Instruments/椅子/沙发1";
            #endregion

            #region 电器
            case InstrumentEnum.电视1:
                return "Instruments/电器/电视1";
            case InstrumentEnum.音响:
                return "Instruments/电器/音响";
            case InstrumentEnum.空调1:
                return "Instruments/电器/空调1";
            #endregion

            #region 灯饰
            case InstrumentEnum.壁灯1:
                return "Instruments/灯饰/壁灯1";
            #endregion

            #region 饰品
            case InstrumentEnum.画1:
                return "Instruments/饰品/画1";
            case InstrumentEnum.杯子1:
                return "Instruments/饰品/杯子1";
            case InstrumentEnum.花瓶1:
                return "Instruments/饰品/花瓶1";
            #endregion

            #region 卫生间
            case InstrumentEnum.花洒1:
                return "Instruments/卫生间/花洒1";
            case InstrumentEnum.马桶1:
                return "Instruments/卫生间/马桶1";
            case InstrumentEnum.洗手盆1:
                return "Instruments/卫生间/洗手盆1";

            #endregion

            #region 厨房

            #endregion

            #region 其他
            case InstrumentEnum.地毯1:
                return "Instruments/其他/地毯1";
            case InstrumentEnum.垃圾桶1:
                return "Instruments/其他/垃圾桶1";
            #endregion

            default:
                return null;
        }
    }

    /// <summary>
    /// 获取某个家具的全部物体
    /// </summary>
    /// <param name="instrumentEnum"></param>
    /// <param name="isAcive"></param>
    /// <returns></returns>
    public List<GameObject> GetTypeALLInstrument(InstrumentEnum instrumentEnum, bool isAcive = true)
    {
        if (instrumentGameObjectDic.ContainsKey(instrumentEnum))
        {
            if (isAcive)
            {
                List<GameObject> items = null;

                foreach (var item in instrumentGameObjectDic[instrumentEnum])
                {
                    if (item.transform.gameObject.activeInHierarchy)
                    {
                        if (items == null)
                        {
                            items = new List<GameObject>();
                        }
                        items.Add(item);
                    }
                }

                return items;
            }
            else
            {
                return instrumentGameObjectDic[instrumentEnum];
            }

        }

        return null;
    }

    /// <summary>
    /// 删除家具
    /// </summary>
    /// <param name="instrument"></param>
    public void DeleteInstrument(Instrument instrument)
    {
        Instrument[] temp = instrument.transform.GetComponentsInChildren<Instrument>();
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].gameObject.activeSelf) Messenger.Broadcast<Instrument>(GlobalEvent.Player_Delete_Instrument, temp[i]);
            temp[i].gameObject.SetActive(false);
            if (temp[i].curAdsorbInstrument != null)
            {
                temp[i].curAdsorbInstrument.adsorbCollider.enabled = true;
                temp[i].curAdsorbInstrument.subInstrument = null;
            }
            temp[i].curAdsorbInstrument = null;
        }
    }

    /// <summary>
    /// 删除全部家具
    /// </summary>
    public void DeleteSceneAllInstrument()
    {
        Instrument[] temp = FindObjectsOfType<Instrument>();
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].type != InstrumentEnum.墙)
            {
                DeleteInstrument(temp[i]);
            }
        }
    }

}

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
            case InstrumentEnum.Cube:
                return "Instruments/测试方块";
            case InstrumentEnum.Sphere:
                return "Instruments/测试小球";



            case InstrumentEnum.Nail:
                return "Nail/Nail";
            case InstrumentEnum.Lj:
                return "棱镜";
            case InstrumentEnum.Dzg:
                return "对中杆";
            case InstrumentEnum.Sjj:
                return "三脚架";
            case InstrumentEnum.QZY_552r15:
                return "552R15Load/全站仪";
            case InstrumentEnum.RtkOnePlush:
                return "银河1Plus";
            case InstrumentEnum.ConnectGun:
                return "连接杆";
            case InstrumentEnum.RtkCreatEnjoy:
                return "创享Rtk";
            case InstrumentEnum.Stick:
                return "碳纤杆";
            case InstrumentEnum.Bracket:
                return "托架";
            case InstrumentEnum.HeightPiece:
                return "测高片";
            case InstrumentEnum.BottomAntenna:
                return "底部天线";
            case InstrumentEnum.TopAntenna:
                return "顶部天线";
            case InstrumentEnum.DSZ3:
                return "U-DSZ3";
            case InstrumentEnum.DL2007:
                return "U-DL2007";
            case InstrumentEnum.Ruler_4687:
                return "木制水准尺4687";
            case InstrumentEnum.Ruler_4787:
                return "木制水准尺4787";
            case InstrumentEnum.RulerPad:
                return "U-尺垫";
            case InstrumentEnum.SteelRulerPad:
                return "U-铟钢尺底座";
            case InstrumentEnum.BarCodeSteelRuler:
                return "U-铟钢尺";
            default:
                return null;
        }
    }

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

    //删除仪器
    public void DeleteInstrument(Instrument instrument)
    {
        Instrument[] temp = instrument.transform.GetComponentsInChildren<Instrument>();
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i].gameObject.SetActive(false);
            Messenger.Broadcast<Instrument>(GlobalEvent.Player_Delete_Instrument, temp[i]);
        }
    }

}

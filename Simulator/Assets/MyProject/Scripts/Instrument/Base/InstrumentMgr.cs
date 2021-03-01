//using DG.Tweening.Plugins.Core.PathCore;
//using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;


/// <summary>
/// ÎïÆ·¹ÜÀíÆ÷
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
                return "Instruments/²âÊÔ·½¿é";
            case InstrumentEnum.Sphere:
                return "Instruments/²âÊÔÐ¡Çò";
            case InstrumentEnum.HangLight:
                return "Instruments/²âÊÔµõµÆ";



            case InstrumentEnum.Nail:
                return "Nail/Nail";
            case InstrumentEnum.Lj:
                return "Àâ¾µ";
            case InstrumentEnum.Dzg:
                return "¶ÔÖÐ¸Ë";
            case InstrumentEnum.Sjj:
                return "Èý½Å¼Ü";
            case InstrumentEnum.QZY_552r15:
                return "552R15Load/È«Õ¾ÒÇ";
            case InstrumentEnum.RtkOnePlush:
                return "ÒøºÓ1Plus";
            case InstrumentEnum.ConnectGun:
                return "Á¬½Ó¸Ë";
            case InstrumentEnum.RtkCreatEnjoy:
                return "´´ÏíRtk";
            case InstrumentEnum.Stick:
                return "Ì¼ÏË¸Ë";
            case InstrumentEnum.Bracket:
                return "ÍÐ¼Ü";
            case InstrumentEnum.HeightPiece:
                return "²â¸ßÆ¬";
            case InstrumentEnum.BottomAntenna:
                return "µ×²¿ÌìÏß";
            case InstrumentEnum.TopAntenna:
                return "¶¥²¿ÌìÏß";
            case InstrumentEnum.DSZ3:
                return "U-DSZ3";
            case InstrumentEnum.DL2007:
                return "U-DL2007";
            case InstrumentEnum.Ruler_4687:
                return "Ä¾ÖÆË®×¼³ß4687";
            case InstrumentEnum.Ruler_4787:
                return "Ä¾ÖÆË®×¼³ß4787";
            case InstrumentEnum.RulerPad:
                return "U-³ßµæ";
            case InstrumentEnum.SteelRulerPad:
                return "U-î÷¸Ö³ßµ××ù";
            case InstrumentEnum.BarCodeSteelRuler:
                return "U-î÷¸Ö³ß";
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

    //É¾³ýÒÇÆ÷
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

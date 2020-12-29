//using DG.Tweening.Plugins.Core.PathCore;
//using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;


/// <summary>
/// ��Ʒ������
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
        //Messenger.AddListener<Instrument>(GlobalEvent.Instrument_State_Change, SceneInstrumentStorageInfoChange);
        //StorageMgr.Instance.RegisterGetNetworkJsonCallback("scene_instrument_info", GetStorageSceneInstrumentInfoCallback);
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
                return "Instruments/���Է���";
            case InstrumentEnum.Sphere:
                return "Instruments/����С��";



            case InstrumentEnum.Nail:
                return "Nail/Nail";
            case InstrumentEnum.Lj:
                return "�⾵";
            case InstrumentEnum.Dzg:
                return "���и�";
            case InstrumentEnum.Sjj:
                return "���ż�";
            case InstrumentEnum.QZY_552r15:
                return "552R15Load/ȫվ��";
            case InstrumentEnum.RtkOnePlush:
                return "����1Plus";
            case InstrumentEnum.ConnectGun:
                return "���Ӹ�";
            case InstrumentEnum.RtkCreatEnjoy:
                return "����Rtk";
            case InstrumentEnum.Stick:
                return "̼�˸�";
            case InstrumentEnum.Bracket:
                return "�м�";
            case InstrumentEnum.HeightPiece:
                return "���Ƭ";
            case InstrumentEnum.BottomAntenna:
                return "�ײ�����";
            case InstrumentEnum.TopAntenna:
                return "��������";
            case InstrumentEnum.DSZ3:
                return "U-DSZ3";
            case InstrumentEnum.DL2007:
                return "U-DL2007";
            case InstrumentEnum.Ruler_4687:
                return "ľ��ˮ׼��4687";
            case InstrumentEnum.Ruler_4787:
                return "ľ��ˮ׼��4787";
            case InstrumentEnum.RulerPad:
                return "U-�ߵ�";
            case InstrumentEnum.SteelRulerPad:
                return "U-���ֳߵ���";
            case InstrumentEnum.BarCodeSteelRuler:
                return "U-���ֳ�";
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

    //ɾ������
    public void DeleteInstrument(Instrument instrument)
    {
        Instrument[] temp = instrument.transform.GetComponentsInChildren<Instrument>();
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i].gameObject.SetActive(false);
            Messenger.Broadcast<Instrument>(GlobalEvent.Player_Delete_Instrument, temp[i]);
        }
    }

    //��ʼ�������ϴ��˳�ʱ�ı��������
    //public void InitSceneInstrument()
    //{
    //    StorageMgr.Instance.GetNetworkJson("scene_instrument_info");
    //}

    //JsonData mSceneInstrumentInfo = new JsonData();
    //private void GetStorageSceneInstrumentInfoCallback(JsonData json)
    //{
    //    try
    //    {
    //        Messenger.RemoveListener<Instrument>(GlobalEvent.Instrument_State_Change, SceneInstrumentStorageInfoChange);
    //        Dictionary<string, Instrument> tempInstrumentDic = new Dictionary<string, Instrument>();
    //        List<Instrument> tempInstrumentList = new List<Instrument>();
    //        mSceneInstrumentInfo = json;
    //        foreach (string key in ((IDictionary)mSceneInstrumentInfo).Keys)
    //        {
    //            JsonData jsonData = mSceneInstrumentInfo[key];
    //            GameObject go = PacksackMgr.Instance.CreatePlayerHoldInstrument((InstrumentEnum)(int)jsonData["type"]);
    //            if (go == null)
    //                continue;
    //            go.transform.parent = null;
    //            Player.Instance.holdInstrument = null;
    //            Player.Instance.SetState(Player.State.normal);
    //            go.name = jsonData["name"].ToString();
    //            string[] posStr = jsonData["position"].ToString().Split(',');
    //            Vector3 pos = new Vector3(float.Parse(posStr[0]), float.Parse(posStr[1]), float.Parse(posStr[2]));
    //            go.transform.position = pos;
    //            string[] eulerStr = jsonData["eulerAngles"].ToString().Split(',');
    //            Vector3 euler = new Vector3(float.Parse(eulerStr[0]), float.Parse(eulerStr[1]), float.Parse(eulerStr[2]));
    //            go.transform.eulerAngles = euler;
    //            Instrument instrument = go.GetComponent<Instrument>();
    //            instrument.storageKey = key;

    //            if (string.IsNullOrEmpty(jsonData["curAdsorbInstrument"].ToString()) == false)
    //                tempInstrumentDic.Add(jsonData["curAdsorbInstrument"].ToString(), instrument);
    //            else
    //                tempInstrumentList.Add(instrument);
    //        }
    //        for (int i = 0; i < tempInstrumentList.Count; i++)
    //        {
    //            tempInstrumentList[i].SetState(Instrument.State.drop);
    //        }
    //        foreach (var item in tempInstrumentDic)
    //        {
    //            Instrument tempInstrument = null;
    //            for (int i = 0; i < tempInstrumentList.Count; i++)
    //            {
    //                if (tempInstrumentList[i].storageKey == item.Key)
    //                    tempInstrument = tempInstrumentList[i];
    //            }
    //            item.Value.curAdsorbInstrument = tempInstrument;
    //            item.Value.SetState(Instrument.State.drop);
    //        }
    //        Messenger.AddListener<Instrument>(GlobalEvent.Instrument_State_Change, SceneInstrumentStorageInfoChange);
    //    }
    //    catch (Exception e)
    //    {
    //        UIRoot.Instance.ClearCache();
    //        UITool.ExitAPP(true);
    //    }
    //}

    ////�����浵�仯
    //private void SceneInstrumentStorageInfoChange(Instrument instrument)
    //{
    //    InstrumentEnum type = InstrumentEnum.None;
    //    //��������
    //    if (instrument.mLastState == Instrument.State.drop && instrument.mState == Instrument.State.life)
    //    {
    //        if (instrument.IsGroupInstrument())
    //            type = instrument.groupInstrumentType;
    //        else
    //            type = instrument.type;
    //        if (string.IsNullOrEmpty(instrument.storageKey) == false)
    //        {
    //            DeleteSceneInstrumentStorageInfo(instrument);
    //        }
    //        //��Ҫ���������ɸ�Ψһ�浵key
    //        string key = (int)type + "_" + (int)(1000 * instrument.transform.position.x) + "_" + (int)(1000 * instrument.transform.position.z);
    //        JsonData jsonData = new JsonData();
    //        jsonData["name"] = instrument.gameObject.name;
    //        jsonData["type"] = (int)type;
    //        jsonData["curAdsorbInstrument"] = instrument.curAdsorbInstrument != null ? instrument.curAdsorbInstrument.storageKey : "";
    //        jsonData["position"] = string.Format("{0},{1},{2}", instrument.transform.position.x, instrument.transform.position.y, instrument.transform.position.z);
    //        jsonData["eulerAngles"] = string.Format("{0},{1},{2}", instrument.transform.eulerAngles.x, instrument.transform.eulerAngles.y, instrument.transform.eulerAngles.z);
    //        mSceneInstrumentInfo[key] = jsonData;
    //        instrument.storageKey = key;
    //        StorageMgr.Instance.SetNetworkJson("scene_instrument_info", mSceneInstrumentInfo);
    //    }
    //    //��������
    //    if (instrument.mLastState == Instrument.State.life && instrument.mState == Instrument.State.held)
    //    {
    //        DeleteSceneInstrumentStorageInfo(instrument);
    //    }
    //}

    ////ɾ�����������浵
    //public void DeleteSceneInstrumentStorageInfo(Instrument instrument)
    //{
    //    if (string.IsNullOrEmpty(instrument.storageKey) == false)
    //    {
    //        Utility.DeleteJsonKey(mSceneInstrumentInfo, instrument.storageKey);
    //        instrument.storageKey = "";
    //        StorageMgr.Instance.SetNetworkJson("scene_instrument_info", mSceneInstrumentInfo);
    //    }
    //}
}

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

    //初始化场景上次退出时的保存的仪器
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

    ////仪器存档变化
    //private void SceneInstrumentStorageInfoChange(Instrument instrument)
    //{
    //    InstrumentEnum type = InstrumentEnum.None;
    //    //放下仪器
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
    //        //需要给仪器生成个唯一存档key
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
    //    //拿起仪器
    //    if (instrument.mLastState == Instrument.State.life && instrument.mState == Instrument.State.held)
    //    {
    //        DeleteSceneInstrumentStorageInfo(instrument);
    //    }
    //}

    ////删除场景仪器存档
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

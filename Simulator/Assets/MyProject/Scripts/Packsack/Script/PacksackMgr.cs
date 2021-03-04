using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PacksackMgr : MonoBehaviour
{
    static PacksackMgr instance;
    public static PacksackMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("PacksackMgr").AddComponent<PacksackMgr>();
            }
            return instance;
        }
    }

    public List<PacksackItemData> packsackItemDataList;
    JsonData mPacksackCfg;

    private void Awake()
    {
        InitPacksackItemData();
        InitPacksackGroupInstrument();
        UpdateGroupInstrumentPacksackItemData();
        Messenger.AddListener<Instrument>(GlobalEvent.Player_Delete_Instrument, (instrument) =>
        {
            PacksackItemData itemData = GetPacksackItemData(instrument.type);
            if (itemData != null)
            {
                itemData.num++;
                UpdateGroupInstrumentPacksackItemData();
                Messenger.Broadcast(GlobalEvent.Packsack_Item_Change);
            }
        });
    }

    private void InitPacksackItemData()
    {
        //packsackItemDataList = new List<PacksackItemData>();
        //packsackItemDataList.Add(new PacksackItemData((int)InstrumentEnum.Cube, "棱镜", "棱镜", 2, "", CreatePlayerHoldInstrument));
        //packsackItemDataList.Add(new PacksackItemData((int)InstrumentEnum.Sphere, "棱镜", "棱镜", 2, "", CreatePlayerHoldInstrument));
        //packsackItemDataList.Add(new PacksackItemData((int)InstrumentEnum.HangLight, "棱镜", "棱镜", 2, "", CreatePlayerHoldInstrument));

        packsackItemDataList = new List<PacksackItemData>();
        //[
        //    {
        //    "InstrumentEnum":"Nail",
        //    "Name":"测钉",
        //    "Icon":"测钉",
        //    "Amount":99,
        //    "Lnk":"B",
        //    "IsGroup":0,
        //    "GroupList":[],
        //    "IsAtPacksack":0
        //    }
        //]
        mPacksackCfg = JsonMapper.ToObject((ResMgr.Instance.Load("Packsack/PacksackCfg") as TextAsset).text);
        foreach (JsonData item in mPacksackCfg)
        {
            PacksackItemData data = new PacksackItemData();
            data.id = (int)(InstrumentEnum)Enum.Parse(typeof(InstrumentEnum), (string)item["InstrumentEnum"]);
            data.name = (string)item["Name"];
            data.icon = (string)item["Icon"];
            data.key = (string)item["Lnk"];
            data.num = (int)item["Amount"];
            data.isGroupInstrument = (int)item["IsGroup"] > 0 ? true : false;
            data.isAtPacksack = (int)item["IsAtPacksack"] > 0 ? true : false;
            //if (data.isGroupInstrument)
            //{
            //    foreach (JsonData son in item["GroupList"])
            //    {
            //        data.instrumentEnumList.Add((InstrumentEnum)Enum.Parse(typeof(InstrumentEnum), (string)son));
            //    }
            //    data.callback = CreatePlayerHoldGroupInstrument;
            //}
            //else data.callback = CreatePlayerHoldInstrument;
            data.callback = CreatePlayerHoldInstrument;
            packsackItemDataList.Add(data);
        }

    }

    //初始化组合仪器,由多个基础仪器组合而成
    private void InitPacksackGroupInstrument()
    {
        //PacksackItemData wzQzy = GetPacksackItemData(InstrumentEnum.wzQzy);
        //wzQzy.isGroupInstrument = true;
        //wzQzy.instrumentEnumList.Add(InstrumentEnum.QZY_552r15);
        //wzQzy.instrumentEnumList.Add(InstrumentEnum.Sjj);
               
    }

    //通过仪器类型创建出玩家当前持有的仪器
    public GameObject CreatePlayerHoldInstrument(InstrumentEnum type, bool hasTips = false)
    {
        PacksackItemData itemData = GetPacksackItemData(type);
        if (itemData != null && itemData.isGroupInstrument == false)
        {
            return CreatePlayerHoldInstrument(itemData, hasTips);
        }
        else if (itemData != null && itemData.isGroupInstrument == true)
        {
            return CreatePlayerHoldGroupInstrument(itemData, hasTips);
        }
        Debug.LogWarning("itemData:" + itemData);
        return null;
    }

    private GameObject CreatePlayerHoldInstrument(PacksackItemData data, bool hasTips = false)
    {
        if (data.num <= 0)
        {
            Debug.LogWarning("数量不足");
            if (hasTips)
                UIMgr.Instance.ShowTips("数量不足");
            return null;
        }
        UIMgr.Instance.HidePanel("Packsack/Packsack");
        GameObject go = InstrumentMgr.Instance.CreateInstrument((InstrumentEnum)data.id);
        if (go != null)
        {
            Instrument item = go.GetComponent<Instrument>();
            item.isHasR = true;
            item.groupInstrumentList = null;
            data.num--;
            UpdateGroupInstrumentPacksackItemData();
            RightHand.Instance.SetHoldInstrument(item);
        }
        else
        {
            Debug.Log(data.name + "仪器信息未设置");
        }
        return go;
    }

    public PacksackItemData GetPacksackItemData(InstrumentEnum type)
    {
        for (int i = 0; i < packsackItemDataList.Count; i++)
        {
            if (packsackItemDataList[i].id == (int)type)
                return packsackItemDataList[i];
        }
        return null;
    }

    //更新组合仪器的背包数量
    private void UpdateGroupInstrumentPacksackItemData()
    {
        for (int i = 0; i < packsackItemDataList.Count; i++)
        {
            PacksackItemData itemData = packsackItemDataList[i];
            if (itemData.isGroupInstrument)
            {
                int num = 9999;
                List<InstrumentEnum> instrumentEnumList = itemData.instrumentEnumList;
                for (int j = 0; j < instrumentEnumList.Count; j++)
                {
                    try
                    {
                        int tempNum = GetPacksackItemData(instrumentEnumList[j]).num;
                        num = Mathf.Min(num, tempNum);
                    }
                    catch (Exception)
                    {

                        Debug.LogError("仪器初始化失败" + instrumentEnumList[j].ToString());
                    }

                }
                itemData.num = num;
            }
        }
    }

    private GameObject CreatePlayerHoldGroupInstrument(PacksackItemData data, bool hasTips = false)
    {
        if (data.num <= 0)
        {
            if (hasTips)
                UIMgr.Instance.ShowTips("数量不足");
            return null;
        }
        UIMgr.Instance.HidePanel("Packsack/Packsack");

        Instrument instrument = null;
        List<Instrument> instrumentList = new List<Instrument>();
        for (int i = 0; i < data.instrumentEnumList.Count; i++)
        {
            instrument = CreatePlayerHoldInstrument(data.instrumentEnumList[i]).GetComponent<Instrument>();
            instrument.subInstrument = instrument;
            if (i < (data.instrumentEnumList.Count - 1))
            {
                instrument.SetState(Instrument.State.life);
                instrument.isHasR = false;
                RightHand.Instance.holdInstrument = null;
                instrumentList.Add(instrument);
            }
            else
            {
                instrument.groupInstrumentList = instrumentList;
                instrument.groupInstrumentType = (InstrumentEnum)data.id;
            }
        }
        for (int i = 0; i < instrumentList.Count; i++)
        {
            //Transform trs = UITool.TransformFindName(instrument.transform, Utility.GetEnumName(data.instrumentEnumList[i]));
            //instrumentList[i].transform.SetParent(trs);
            instrumentList[i].transform.localPosition = Vector3.zero;
        }
        return instrument != null ? instrument.gameObject : null;
    }


    //获取地图上存在的仪器(剔除掉组合仪器)
    public List<Instrument> GetMapInstrumentList()
    {
        List<Instrument> mapInstrumentList = GameObject.FindObjectsOfType<Instrument>().ToList();
        List<Instrument> group = new List<Instrument>();
        for (int i = 0; i < mapInstrumentList.Count; i++)
        {
            if (mapInstrumentList[i].IsGroupInstrument())
                group.Add(mapInstrumentList[i]);
        }
        //剔除掉组合仪器的内部基础仪器
        for (int i = 0; i < group.Count; i++)
        {
            Instrument[] temp = group[i].GetComponentsInChildren<Instrument>();
            for (int j = 1; j < temp.Length; j++)
            {
                mapInstrumentList.Remove(temp[j]);
            }
        }
        for (int i = mapInstrumentList.Count - 1; i >= 0; i--)
        {
            if (mapInstrumentList[i].mState != Instrument.State.life)
                mapInstrumentList.RemoveAt(i);
        }
        return mapInstrumentList;
    }


    public List<Instrument> GetMapInstrumentList(List<InstrumentEnum> ignoreEnumList, bool isAtPacksack = true)
    {
        List<Instrument> mapInstrumentList = GetMapInstrumentList();
        for (int i = mapInstrumentList.Count - 1; i >= 0; i--)
        {
            //是组合仪器,不剔除
            if (mapInstrumentList[i].IsGroupInstrument())
                continue;
            //忽略类型不包含,再判断是否在背包显示
            if (ignoreEnumList.Contains(mapInstrumentList[i].type) == false)
            {
                if (PacksackMgr.Instance.GetPacksackItemData(mapInstrumentList[i].type).isAtPacksack == isAtPacksack)
                    continue;
            }
            //剩下不符合的都剔除掉
            mapInstrumentList.Remove(mapInstrumentList[i]);
        }
        mapInstrumentList.Sort((Instrument a, Instrument b) => { return a.selfSortIndex - b.selfSortIndex; });
        return mapInstrumentList;
    }
}

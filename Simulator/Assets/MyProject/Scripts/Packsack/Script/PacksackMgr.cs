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
        Messenger.AddListener<Instrument>(GlobalEvent.Player_Delete_Instrument, (instrument) =>
        {
            PacksackItemData itemData = GetPacksackItemData(instrument.type);
            if (itemData != null)
            {
                itemData.num++;
                Messenger.Broadcast(GlobalEvent.Packsack_Item_Change);
            }
        });
    }

    private void InitPacksackItemData()
    {
        packsackItemDataList = new List<PacksackItemData>();
        mPacksackCfg = JsonMapper.ToObject((ResMgr.Instance.Load("Packsack/PacksackCfg") as TextAsset).text);
        foreach (JsonData item in mPacksackCfg)
        {
            PacksackItemData data = new PacksackItemData();
            data.id = (int)(InstrumentEnum)Enum.Parse(typeof(InstrumentEnum), (string)item["InstrumentEnum"]);
            data.Type = (int)(InstrumentTypeEnum)Enum.Parse(typeof(InstrumentTypeEnum),(string)item["InstrumentTypeEnum"]);
            data.name = (string)item["Name"];
            data.icon = (string)item["Icon"];
            data.num = (int)item["Amount"];
            data.isAtPacksack = (int)item["IsAtPacksack"] > 0 ? true : false;
            data.callback = CreatePlayerHoldInstrument;
            packsackItemDataList.Add(data);
        }

    }

    //通过仪器类型创建出玩家当前持有的仪器
    public GameObject CreatePlayerHoldInstrument(InstrumentEnum type, bool hasTips = false)
    {
        PacksackItemData itemData = GetPacksackItemData(type);
        if (itemData != null)
        {
            return CreatePlayerHoldInstrument(itemData, hasTips);
        }
        Debug.LogWarning("itemData:" + itemData);
        return null;
    }

    private GameObject CreatePlayerHoldInstrument(PacksackItemData data, bool hasTips = false)
    {
        if (data.num <= 0)
        {
            Debug.LogWarning("数量不足");
            return null;
        }
        GameObject go = InstrumentMgr.Instance.CreateInstrument((InstrumentEnum)data.id);
        if (go != null)
        {
            Instrument item = go.GetComponent<Instrument>();
            item.isHasR = true;
            item.groupInstrumentList = null;
            data.num--;
            RightHand.Instance.SetHoldInstrument(item);
            Messenger.Broadcast(GlobalEvent.Packsack_Item_Change);
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

}

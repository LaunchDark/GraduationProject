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
            #region ����
            case InstrumentEnum.��:
                return "Instruments/������";
            case InstrumentEnum.Cube:
                return "Instruments/���Է���";
            case InstrumentEnum.Sphere:
                return "Instruments/����С��";
            case InstrumentEnum.HangLight:
                return "Instruments/���Ե���";
            case InstrumentEnum.Double:
                return "Instruments/���Ը�����";
            #endregion

            #region ��
            case InstrumentEnum.����1:
                return "Instruments/��/����1";
            case InstrumentEnum.����2:
                return "Instruments/��/����2";
            case InstrumentEnum.��ͷ1:
                return "Instruments/��/��ͷ1";
            case InstrumentEnum.��1:
                return "Instruments/��/��1";
            #endregion

            #region ����
            case InstrumentEnum.��ͷ��1:
                return "Instruments/����/��ͷ��1";
            case InstrumentEnum.���ӹ�1:
                return "Instruments/����/���ӹ�1";
            #endregion

            #region ����
            case InstrumentEnum.����1:
                return "Instruments/����/����1";
            case InstrumentEnum.Բ��1:
                return "Instruments/����/Բ��1";
            #endregion

            #region ����
            case InstrumentEnum.����1:
                return "Instruments/����/����1";
            case InstrumentEnum.����2:
                return "Instruments/����/����2";
            case InstrumentEnum.����3:
                return "Instruments/����/����3";
            case InstrumentEnum.ɳ��1:
                return "Instruments/����/ɳ��1";
            #endregion

            #region ����
            case InstrumentEnum.����1:
                return "Instruments/����/����1";
            case InstrumentEnum.����:
                return "Instruments/����/����";
            case InstrumentEnum.�յ�1:
                return "Instruments/����/�յ�1";
            #endregion

            #region ����
            case InstrumentEnum.�ڵ�1:
                return "Instruments/����/�ڵ�1";
            #endregion

            #region ��Ʒ
            case InstrumentEnum.��1:
                return "Instruments/��Ʒ/��1";
            case InstrumentEnum.����1:
                return "Instruments/��Ʒ/����1";
            case InstrumentEnum.��ƿ1:
                return "Instruments/��Ʒ/��ƿ1";
            #endregion

            #region ������
            case InstrumentEnum.����1:
                return "Instruments/������/����1";
            case InstrumentEnum.��Ͱ1:
                return "Instruments/������/��Ͱ1";
            case InstrumentEnum.ϴ����1:
                return "Instruments/������/ϴ����1";

            #endregion

            #region ����

            #endregion

            #region ����
            case InstrumentEnum.��̺1:
                return "Instruments/����/��̺1";
            case InstrumentEnum.����Ͱ1:
                return "Instruments/����/����Ͱ1";
            #endregion

            default:
                return null;
        }
    }

    /// <summary>
    /// ��ȡĳ���Ҿߵ�ȫ������
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
    /// ɾ���Ҿ�
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
    /// ɾ��ȫ���Ҿ�
    /// </summary>
    public void DeleteSceneAllInstrument()
    {
        Instrument[] temp = FindObjectsOfType<Instrument>();
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].type != InstrumentEnum.ǽ)
            {
                DeleteInstrument(temp[i]);
            }
        }
    }

}

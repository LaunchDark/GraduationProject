using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 门
/// </summary>
public class Door : Instrument
{
    protected Transform RotaCenter;
    void Start()
    {
        type = InstrumentEnum.门;
        CanScaleInstrument = false;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.5f;
        canDropDis = 0f;

        width = 0.1f;
        height = 2f;

        adsorbTypeList = new List<InstrumentEnum>();
        adsorbTypeList.Add(InstrumentEnum.门框);

        RotaCenter = transform.Find("旋转轴");
        GetComponentInChildren<mButton>().clickCallBack = OpenOrCloseDoor;

        foreach (var item in transform.GetComponentsInChildren<Transform>())
        {
            if (item.GetComponentInParent<mButton>())
                item.gameObject.layer = LayerMask.NameToLayer("UI");
        }

    }

    override public void AdsorbCallBack()
    {
        Transform tf = curAdsorbInstrument.transform.Find(type.ToString());
        transform.SetParent(tf);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }

    public void OpenOrCloseDoor()
    {
        //Debug.Log(RotaCenter.localEulerAngles.y);
        if(RotaCenter.localEulerAngles.y != 0)
        {
            RotaCenter.DOLocalRotate(Vector3.zero, 0.5f);
        }
        else
        {
            RotaCenter.DOLocalRotate(new Vector3(0,90,0), 0.5f);
        }
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Keypad0))
    //    {
    //        OpenOrCloseDoor();
    //    }
    //}
}

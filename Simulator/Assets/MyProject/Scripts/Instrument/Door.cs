using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Instrument
{

    void Start()
    {
        type = InstrumentEnum.门;
        MaxOffsetZ = 3f;
        MinOffsetZ = 0.5f;
        canDropDis = 0f;
        adsorbTypeList = new List<InstrumentEnum>();
        adsorbTypeList.Add(InstrumentEnum.墙);
    }

    override public void AdsorbCallBack()
    {
        Transform tf = curAdsorbInstrument.transform.Find(type.ToString());
        transform.SetParent(tf);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 窗1
/// </summary>
public class Window1Build : Instrument
{
    public override void AwakeLater()
    {
        Start();
    }

    void Start()
    {
        type = InstrumentEnum.窗;
        CanScaleInstrument = false;
        isHangInsturment = true;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.5f;
        canDropDis = 0f;

        width = 0.1f;
        height = 0.8f;

        adsorbTypeList = new List<InstrumentEnum>();
        adsorbTypeList.Add(InstrumentEnum.窗台);
    }

    override public void AdsorbCallBack()
    {
        Transform tf = curAdsorbInstrument.transform.Find(type.ToString());
        transform.SetParent(tf);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }
}

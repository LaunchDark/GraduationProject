using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 门框
/// </summary>
public class Doorframe : Instrument
{
    void Start()
    {
        isHasR = false;
        type = InstrumentEnum.门框;

        adsorbTypeList = new List<InstrumentEnum>();
        adsorbTypeList.Add(InstrumentEnum.门);

        SetState(State.life);
    }
}

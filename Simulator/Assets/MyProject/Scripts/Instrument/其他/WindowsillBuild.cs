using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsillBuild : Instrument
{
    void Start()
    {
        isHasR = false;
        type = InstrumentEnum.窗台;

        adsorbTypeList = new List<InstrumentEnum>();
        adsorbTypeList.Add(InstrumentEnum.窗);

        SetState(State.life);
    }
}

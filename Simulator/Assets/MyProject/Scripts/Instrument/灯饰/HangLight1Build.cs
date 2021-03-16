using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangLight1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.壁灯1;
        isHangInsturment = true;
        CanScaleInstrument = true;

        MaxOffsetZ = 2f;
        MinOffsetZ = 0.3f;
        canDropDis = 10f;

        width = 0.18f;
        height = 0.22f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.杯子1;
        isFreeInstrument = true;
        CanScaleInstrument = true;

        MaxOffsetZ = 1.0f;
        MinOffsetZ = 0.1f;
        canDropDis = 2f;

        width = 0.1f;
        height = 0.1f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 椅子3
/// </summary>
public class Chair3Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.椅子3;
        isFreeInstrument = true;
        CanScaleInstrument = true;

        MaxOffsetZ = 2f;
        MinOffsetZ = 0.3f;
        canDropDis = 2f;

        width = 0.36f;
        height = 0.6f;
    }

}

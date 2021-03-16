using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 椅子1
/// </summary>
public class Chair1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.椅子1;
        isFreeInstrument = true;
        CanScaleInstrument = true;

        MaxOffsetZ = 2f;
        MinOffsetZ = 0.3f;
        canDropDis = 2f;

        width = 0.3f;
        height = 0.3f;
    }

}

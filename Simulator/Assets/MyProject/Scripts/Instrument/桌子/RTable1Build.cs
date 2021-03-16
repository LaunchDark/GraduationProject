using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 圆桌1
/// </summary>
public class RTable1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.圆桌1;
        CanScaleInstrument = true;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.5f;
        canDropDis = 2f;

        width = 0.5f;
        height = 0.2f;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 圆桌2
/// </summary>
public class RTable2Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.圆桌2;
        CanScaleInstrument = true;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.5f;
        canDropDis = 2f;

        width = 0.6f;
        height = 0.6f;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 空调
/// </summary>
public class Air1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.空调1;
        CanScaleInstrument = true;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.5f;
        canDropDis = 2f;

        width = 0.45f;
        height = 1.3f;
    }

}

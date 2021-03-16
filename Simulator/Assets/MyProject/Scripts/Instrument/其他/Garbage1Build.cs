using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 垃圾桶
/// </summary>
public class Garbage1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.垃圾桶1;
        isFreeInstrument = true;
        CanScaleInstrument = true;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.1f;
        canDropDis = 2f;

        width = 0.15f;
        height = 0.15f;
    }

}

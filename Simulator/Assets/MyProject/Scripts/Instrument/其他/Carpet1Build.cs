using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地毯
/// </summary>
public class Carpet1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.地毯1;
        isFloot = true;
        CanScaleInstrument = true;

        MaxOffsetZ = 3f;
        MinOffsetZ = 1f;
        canDropDis = 1f;

        width = 1.8f;
        height = 0.05f;
    }
}

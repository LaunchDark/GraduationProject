using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 床2
/// </summary>
public class Bed2Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.床2;
        isFloot = true;
        CanScaleInstrument = true;

        MaxOffsetZ = 5f;
        MinOffsetZ = 1f;
        canDropDis = 2f;

        width = 2.2f;
        height = 1f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 花瓶1
/// </summary>
public class Vase1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.花瓶1;
        isFreeInstrument = true;
        CanScaleInstrument = true;

        MaxOffsetZ = 1.5f;
        MinOffsetZ = 0.1f;
        canDropDis = 2f;

        width = 0.18f;
        height = 0.18f;
    }

}

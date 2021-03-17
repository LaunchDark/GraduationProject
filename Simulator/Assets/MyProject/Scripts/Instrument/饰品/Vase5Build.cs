using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 花瓶5
/// </summary>
public class Vase5Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.花瓶5;
        isFreeInstrument = true;
        CanScaleInstrument = true;

        MaxOffsetZ = 1.5f;
        MinOffsetZ = 0.1f;
        canDropDis = 2f;

        width = 0.1f;
        height = 0.15f;
    }

}

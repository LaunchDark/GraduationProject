using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 电脑1
/// </summary>
public class Computer1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.电脑1;
        isFreeInstrument = true;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.5f;
        canDropDis = 2f;

        width = 0.2f;
        height = 0.2f;

    }
}

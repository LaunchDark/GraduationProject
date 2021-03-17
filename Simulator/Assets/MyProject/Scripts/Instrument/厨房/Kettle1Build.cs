using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 水壶1
/// </summary>
public class Kettle1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.水壶1;
        isFreeInstrument = true;

        MaxOffsetZ = 1.5f;
        MinOffsetZ = 0.5f;
        canDropDis = 2f;

        width = 0.08f;
        height = 0.07f;
    }
}

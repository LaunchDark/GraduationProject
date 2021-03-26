using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 餐桌1
/// </summary>
public class DiningTable1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.餐桌1;
        CanScaleInstrument = true;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.5f;
        canDropDis = 2f;

        width = 0.84f;
        height = 0.65f;
    }
}

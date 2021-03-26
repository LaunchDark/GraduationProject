using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 餐椅1
/// </summary>
public class DiningChair1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.餐椅1;
        isFreeInstrument = true;
        CanScaleInstrument = true;

        MaxOffsetZ = 2f;
        MinOffsetZ = 0.3f;
        canDropDis = 2f;

        width = 0.5f;
        height = 0.7f;
    }
}

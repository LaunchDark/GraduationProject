using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 电视1
/// </summary>
public class TV1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.电视1;
        isHangInsturment = true;
        CanScaleInstrument = true;

        MaxOffsetZ = 2f;
        MinOffsetZ = 0.5f;
        canDropDis = 10f;

        width = 0.1f;
        height = 0.4f;
    }

}

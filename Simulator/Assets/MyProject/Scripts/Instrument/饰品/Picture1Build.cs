using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 画1
/// </summary>
public class Picture1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.画1;
        isHangInsturment = true;
        CanScaleInstrument = true;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.5f;
        canDropDis = 5f;

        width = 0.03f;
        height = 0.6f;
    }

}

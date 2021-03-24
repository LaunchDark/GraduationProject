using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 吊扇1
/// </summary>
public class HangingFan1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.吊扇1;
        CanScaleInstrument = true;
        isHangInsturment = true;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.5f;
        canDropDis = 5f;

        width = 0.5f;
        height = 0.15f;
    }
}

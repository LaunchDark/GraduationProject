using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 长桌
/// </summary>
public class Table1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.长桌1;
        CanScaleInstrument = true;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.5f;
        canDropDis = 2f;

        width = 0.36f;
        height = 0.4f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 台灯1
/// </summary>
public class TableLight1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.台灯1;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.5f;
        canDropDis = 3f;

        width = 0.18f;
        height = 0.4f;
    }
}

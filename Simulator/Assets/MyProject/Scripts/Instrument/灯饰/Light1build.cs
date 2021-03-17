using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 顶灯1
/// </summary>
public class Light1build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.顶灯1;
        isHangInsturment = true;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.5f;
        canDropDis = 5f;

        width = 0.2f;
        height = 0.08f;
    }

}

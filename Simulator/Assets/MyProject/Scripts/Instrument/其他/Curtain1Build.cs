using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 窗帘
/// </summary>
public class Curtain1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.窗帘;
        isHangInsturment = true;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.5f;
        canDropDis = 5f;

        width = 0.2f;
        height = 1.9f;
    }
}

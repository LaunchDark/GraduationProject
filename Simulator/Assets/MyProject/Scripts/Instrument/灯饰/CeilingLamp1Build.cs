using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 吊灯1
/// </summary>
public class CeilingLamp1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.吊灯1;
        isHangInsturment = true;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.5f;
        canDropDis = 5f;

        width = 0.2f;
        height = 1.1f;
    }
}

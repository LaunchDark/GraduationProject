using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 冰箱
/// </summary>
public class Refrigerator1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.冰箱1;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.5f;
        canDropDis = 2f;

        width = 0.55f;
        height = 1.62f;
    }
}

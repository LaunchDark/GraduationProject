using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 马桶1
/// </summary>
public class Closestool1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.马桶1;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.5f;
        canDropDis = 2f;

        width = 0.7f;
        height = 1.0f;
    }

}

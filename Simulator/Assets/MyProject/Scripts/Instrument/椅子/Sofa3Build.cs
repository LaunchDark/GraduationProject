using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 沙发3
/// </summary>
public class Sofa3Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.沙发3;

        MaxOffsetZ = 3f;
        MinOffsetZ = 1f;
        canDropDis = 2f;

        width = 0.64f;
        height = 0.53f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 灶台1
/// </summary>
public class Hearth1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.灶台1;

        MaxOffsetZ = 2f;
        MinOffsetZ = 0.3f;
        canDropDis = 2f;

        width = 0.2f;
        height = 0.04f;
    }
}

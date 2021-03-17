using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 橱柜1
/// </summary>
public class Cupboard1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.橱柜1;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.5f;
        canDropDis = 2f;

        width = 0.98f;
        height = 0.45f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 锅
/// </summary>
public class KitchenWare2Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.厨具2;
        isFreeInstrument = true;

        MaxOffsetZ = 1.5f;
        MinOffsetZ = 0.1f;
        canDropDis = 2f;

        width = 0.08f;
        height = 0.04f;
    }
}

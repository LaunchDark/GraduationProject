using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 厨具1
/// </summary>
public class KitchenWare1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.厨具1;

        MaxOffsetZ = 2f;
        MinOffsetZ = 0.1f;
        canDropDis = 2f;

        width = 0.2f;
        height = 0.2f;
    }
}

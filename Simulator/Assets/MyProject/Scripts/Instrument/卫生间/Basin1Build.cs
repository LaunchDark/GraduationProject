using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 洗手盆1
/// </summary>
public class Basin1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.洗手盆1;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.5f;
        canDropDis = 2f;

        width = 0.66f;
        height = 0.1f;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玻璃桌
/// </summary>
public class Table2Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.长桌2;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.5f;
        canDropDis = 2f;

        width = 0.55f;
        height = 0.3f;
    }
}

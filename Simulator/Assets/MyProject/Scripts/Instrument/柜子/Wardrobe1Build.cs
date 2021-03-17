using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 衣柜1
/// </summary>
public class Wardrobe1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.衣柜1;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.5f;
        canDropDis = 2f;

        width = 0.46f;
        height = 1.25f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 沙发2
/// </summary>
public class Sofa2Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.沙发2;

        MaxOffsetZ = 3f;
        MinOffsetZ = 1f;
        canDropDis = 2f;

        width = 0.52f;
        height = 0.35f;
    }

}

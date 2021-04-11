using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 沙发1
/// </summary>
public class Sofa1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.沙发1;
        CanScaleInstrument = true;

        MaxOffsetZ = 3f;
        MinOffsetZ = 1f;
        canDropDis = 2f;

        width = 0.7f;
        height = 0.3f;
    }

}

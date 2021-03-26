using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枕头2
/// </summary>
public class BedPillow2Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.枕头2;
        isFreeInstrument = true;
        CanScaleInstrument = true;

        MaxOffsetZ = 2f;
        MinOffsetZ = 0.2f;
        canDropDis = 2f;

        width = 0.25f;
        height = 0.25f;
    }
}

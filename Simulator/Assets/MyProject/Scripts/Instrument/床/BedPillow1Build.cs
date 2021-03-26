using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枕头1
/// </summary>
public class BedPillow1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.枕头1;
        isFreeInstrument = true;
        CanScaleInstrument = true;

        MaxOffsetZ = 2f;
        MinOffsetZ = 0.2f;
        canDropDis = 2f;

        width = 0.25f;
        height = 0.25f;
    }

}

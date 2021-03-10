using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedPillow1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.枕头1;
        isFreeInstrument = true;
        CanScaleInstrument = true;
    }

}

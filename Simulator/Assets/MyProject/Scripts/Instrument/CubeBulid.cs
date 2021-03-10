using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBulid : Instrument
{
    void Start()
    {
        type = InstrumentEnum.Cube;
        isFreeInstrument = true;
        CanScaleInstrument = true;
    }
}

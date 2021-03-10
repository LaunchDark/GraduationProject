using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillow2Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.抱枕2;
        isFreeInstrument = true;
        CanScaleInstrument = true;
    }
}

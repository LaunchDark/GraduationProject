using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillow1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.抱枕1;
        isFreeInstrument = true;
        CanScaleInstrument = true;
    }

}

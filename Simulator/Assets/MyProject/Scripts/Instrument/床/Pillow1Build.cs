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

        MaxOffsetZ = 2f;
        MinOffsetZ = 0.2f;
        canDropDis = 2f;

        width = 0.25f;
        height = 0.25f;
    }

}

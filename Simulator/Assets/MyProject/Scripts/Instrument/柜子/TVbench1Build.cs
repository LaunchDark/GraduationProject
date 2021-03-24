using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVbench1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.电视柜1;
        CanScaleInstrument = true;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.5f;
        canDropDis = 2f;

        width = 0.38f;
        height = 0.45f;
    }

}

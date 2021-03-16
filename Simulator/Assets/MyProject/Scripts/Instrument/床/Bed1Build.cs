using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.床1;
        isFloot = true;

        MaxOffsetZ = 5f;
        MinOffsetZ = 1f;
        canDropDis = 2f;

        width = 2.6f;
        height = 1.3f;
    }

}

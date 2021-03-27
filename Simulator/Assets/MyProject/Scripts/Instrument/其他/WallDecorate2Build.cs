using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDecorate2Build: Instrument
{
    private void Start()
    {
        type = InstrumentEnum.WallDecorate2;

        isHangInsturment = true;
        isUprightWall = true;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.1f;
        canDropDis = 5f;

        width = 0.1f;
        height = 1.95f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDecorate1Build: Instrument
{
    private void Start()
    {
        type = InstrumentEnum.WallDecorate1;

        isHangInsturment = true;
        isCrossWall = true;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.1f;
        canDropDis = 5f;

        width = 0.28f;
        height = 0.09f;
    }
}

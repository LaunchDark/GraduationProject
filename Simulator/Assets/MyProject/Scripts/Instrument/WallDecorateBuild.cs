using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDecorateBuild: Instrument
{
    private void Start()
    {
        type = InstrumentEnum.WallDecorate;

        isHangInsturment = true;
        isCrossWall = true;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.1f;
        canDropDis = 5f;

        width = 0.1f;
        height = 0.1f;
    }
}

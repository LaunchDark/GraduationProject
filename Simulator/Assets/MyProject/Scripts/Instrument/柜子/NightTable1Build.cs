using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 床头柜1
/// </summary>
public class NightTable1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.床头柜1;
        CanScaleInstrument = true;

        MaxOffsetZ = 2f;
        MinOffsetZ = 0.5f;
        canDropDis = 2f;

        width = 0.4f;
        height = 0.35f;
    }

}

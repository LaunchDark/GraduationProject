using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 花洒
/// </summary>
public class Shower1Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.花洒1;
        isHangInsturment = true;

        MaxOffsetZ = 3f;
        MinOffsetZ = 0.3f;
        canDropDis = 3f;

        width = 0.2f;
        height = 0.65f;
    }

}

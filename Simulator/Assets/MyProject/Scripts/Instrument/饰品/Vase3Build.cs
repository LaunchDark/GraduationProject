﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 花瓶3
/// </summary>
public class Vase3Build : Instrument
{
    void Start()
    {
        type = InstrumentEnum.花瓶3;
        isFreeInstrument = true;
        CanScaleInstrument = true;

        MaxOffsetZ = 1.5f;
        MinOffsetZ = 0.1f;
        canDropDis = 2f;

        width = 0.13f;
        height = 0.15f;
    }

}

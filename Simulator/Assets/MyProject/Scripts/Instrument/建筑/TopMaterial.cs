using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 天花板代码
/// </summary>
public class TopMaterial : Instrument
{
    void Start()
    {
        isHasR = false;
        type = InstrumentEnum.天花板;

        SetState(State.life);
    }
}

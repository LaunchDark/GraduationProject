using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 墙体代码
/// </summary>
public class WallMaterial : Instrument
{
    void Start()
    {
        isHasR = false;
        type = InstrumentEnum.墙;

        SetState(State.life);

        Destroy(gameObject.GetComponent<OutLineTargetComponent>());
    }
}

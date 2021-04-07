using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地板代码
/// </summary>
public class FloorMaterial : Instrument
{
    void Start()
    {
        isHasR = false;
        isFloot = true;
        type = InstrumentEnum.地板;

        SetState(State.life);
        Destroy(gameObject.GetComponent<OutLineTargetComponent>());
    }
}

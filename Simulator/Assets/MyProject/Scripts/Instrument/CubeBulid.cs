using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBulid : Instrument
{
    void Start()
    {
        type = InstrumentEnum.Cube;
        isFreeInstrument = true;
        CanScaleInstrument = true;
    }

    public override void FreeCallBack()
    {
        AddFreeComponent();
    }

    public override void HeldCallBack(GameObject hand)
    {
        base.HeldCallBack(hand);
        RemoveFreeComponent();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBulid : Instrument
{

    List<GameObject> Objects;

    void Start()
    {
        type = InstrumentEnum.Cube;
        isFreeInstrument = true;
        CanScaleInstrument = true;
        Objects = new List<GameObject>();
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

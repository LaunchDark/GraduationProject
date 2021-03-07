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


    protected override void ControlScale()
    {
        if(LastPos == Vector3.zero)
        {
            LastPos = ScaleHand.transform.position;
            return;
        }
        CurPos = ScaleHand.transform.position;
        //缩放比例 = 当前长度/上一个长度
        float ratio = Mathf.Abs(Vector3.Distance(CurPos,transform.position))/Mathf.Abs(Vector3.Distance(LastPos, transform.position));
        transform.localScale *= ratio;
        LastPos = CurPos;
    }
}

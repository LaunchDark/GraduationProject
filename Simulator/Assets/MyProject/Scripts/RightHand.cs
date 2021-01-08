using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class RightHand : HandBase
{
    private static RightHand _instance;
    public static RightHand Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<RightHand>();
            }
            return _instance;
        }
    }

    protected override void Start()
    {
        hand = transform.GetComponent<Hand>();
    }

    protected override void Update()
    {
        #region 按键输入
        if (TouchPad[SteamVR_Input_Sources.RightHand].changed)
        {
            InputTouchPad();
        }

        if (hand.GetGrabStarting() != GrabTypes.None)
        {
            if (hand.GetGrabStarting() == GrabTypes.Grip)
            {
                InputGrip();
            }
            if (hand.GetGrabStarting() == GrabTypes.Pinch)
            {
                InputTrigger();
            }
        }

        if (telepory.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            InputTeleport();
        }
        #endregion

    }

    protected override void InputTouchPad()
    {
        Debug.Log("右手摇杆输入：" + TouchPad[SteamVR_Input_Sources.RightHand].axis);

    }

    protected override void InputGrip()
    {
        print("右手输入类型：" + hand.GetGrabStarting());

        Debug.Log("生成方块");
        //InstrumentMgr.Instance.CreateInstrument(InstrumentEnum.Cube, false);
        PacksackMgr.Instance.CreatePlayerHoldInstrument(InstrumentEnum.Cube, true);

    }

    protected override void InputTrigger()
    {
        print("右手输入类型：" + hand.GetGrabStarting());

        Debug.Log(holdInstrument);
        if (holdInstrument)
        {
            if (holdInstrument.GetHeldState() == Instrument.HeldState.green)
            {
                holdInstrument.transform.parent = null;
                holdInstrument.SetState(Instrument.State.drop);
                holdInstrument = null;
            }
        }
        else
        {
            Debug.Log("生成小球");
            //InstrumentMgr.Instance.CreateInstrument(InstrumentEnum.Cube, false);
            PacksackMgr.Instance.CreatePlayerHoldInstrument(InstrumentEnum.Sphere, true);
        }
    }

    protected override void InputTeleport()
    {
        print("右手点击触摸板:");
    }

}

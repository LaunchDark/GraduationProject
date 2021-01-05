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
        if (TouchPad[SteamVR_Input_Sources.RightHand].changed)
        {
            Debug.Log("右手摇杆输入：" + TouchPad[SteamVR_Input_Sources.RightHand].axis);
        }

        if (hand.GetGrabStarting() != GrabTypes.None)
        {
            print("右手输入类型：" + hand.GetGrabStarting());

            if (hand.GetGrabStarting() == GrabTypes.Grip)
            {
                Debug.Log("生成方块");
                //InstrumentMgr.Instance.CreateInstrument(InstrumentEnum.Cube, false);
                PacksackMgr.Instance.CreatePlayerHoldInstrument(InstrumentEnum.Cube, true);
            }
            if (hand.GetGrabStarting() == GrabTypes.Pinch)
            {
                Debug.Log("生成小球");
                //InstrumentMgr.Instance.CreateInstrument(InstrumentEnum.Cube, false);
                PacksackMgr.Instance.CreatePlayerHoldInstrument(InstrumentEnum.Sphere, true);
            }
        }

        if (telepory.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            print("右手点击触摸板:");
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class LeftHand : HandBase
{
    private static LeftHand _instance;
    public static LeftHand Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LeftHand>();
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
        if (TouchPad[SteamVR_Input_Sources.LeftHand].changed)
        {
            Debug.Log("左手摇杆输入：" + TouchPad[SteamVR_Input_Sources.LeftHand].axis);
        }

        if (hand.GetGrabStarting() != GrabTypes.None)
        {
            print("左手输入类型：" + hand.GetGrabStarting());

            //if (hand.GetGrabStarting() == GrabTypes.Grip)
            //{
            //    Debug.Log("生成方块");
            //    //InstrumentMgr.Instance.CreateInstrument(InstrumentEnum.Cube, false);
            //    PacksackMgr.Instance.CreatePlayerHoldInstrument(InstrumentEnum.Cube, true);
            //}
            //if (hand.GetGrabStarting() == GrabTypes.Pinch)
            //{
            //    Debug.Log("生成小球");
            //    //InstrumentMgr.Instance.CreateInstrument(InstrumentEnum.Cube, false);
            //    PacksackMgr.Instance.CreatePlayerHoldInstrument(InstrumentEnum.Sphere, true);
            //}
        }

        if (telepory.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            print("左手点击触摸板:");
        }

    }


}

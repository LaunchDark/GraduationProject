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
        #region 按键输入
        if (TouchPad[SteamVR_Input_Sources.LeftHand].changed)
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

        if (telepory.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            InputTeleport();
        }
        #endregion

    }

    protected override void InputTouchPad()
    {
        Debug.Log("左手摇杆输入：" + TouchPad[SteamVR_Input_Sources.LeftHand].axis);

    }

    protected override void InputGrip()
    {
        print("左手输入类型：" + hand.GetGrabStarting());
    }

    protected override void InputTrigger()
    {
        print("左手输入类型：" + hand.GetGrabStarting());
    }

    protected override void InputTeleport()
    {
        print("左手点击触摸板:");
    }

}

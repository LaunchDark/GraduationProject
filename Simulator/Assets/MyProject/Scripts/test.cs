using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class test : MonoBehaviour
{
    public Hand left;
    public Hand right;

    public SteamVR_Action_Boolean snapLeftAction = SteamVR_Input.GetBooleanAction("SnapTurnLeft");
    public SteamVR_Action_Boolean snapRightAction = SteamVR_Input.GetBooleanAction("SnapTurnRight");

    public SteamVR_Action_Boolean telepory = SteamVR_Input.GetBooleanAction("Teleport");

    void Start()
    {
        left = transform.Find("SteamVRObjects/LeftHand").GetComponent<Hand>();
        right = transform.Find("SteamVRObjects/RightHand").GetComponent<Hand>();

        print("左手类型：" + left.handType);
        print("右手类型：" + right.handType);
    }


    void Update()
    {
        if (left.GetGrabStarting() != GrabTypes.None)
        {
            print("左手输入类型：" + left.GetGrabStarting());

            if(left.GetGrabStarting() == GrabTypes.Grip)
            {
                Debug.Log("生成方块");
                //InstrumentMgr.Instance.CreateInstrument(InstrumentEnum.Cube, false);
                PacksackMgr.Instance.CreatePlayerHoldInstrument(InstrumentEnum.Cube, true);
            }
            if (left.GetGrabStarting() == GrabTypes.Pinch)
            {
                Debug.Log("生成小球");
                //InstrumentMgr.Instance.CreateInstrument(InstrumentEnum.Cube, false);
                PacksackMgr.Instance.CreatePlayerHoldInstrument(InstrumentEnum.Sphere, true);
            }



        }
        if (right.GetGrabStarting() != GrabTypes.None)
        {
            print("右手输入类型：" + right.GetGrabStarting());
        }

        if (snapLeftAction.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            print("左手输入向左:");
        }
        if (snapRightAction.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            print("左手输入向右:");
        }
        if (telepory.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            print("左手点击触摸板:");
        }
        if (snapLeftAction.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            print("右手输入向左:");
        }
        if (snapRightAction.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            print("右手输入向右:");
        }
        if (telepory.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            print("右手点击触摸板:");
        }

    }



}

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

    //public SteamVR_Action_Single T = SteamVR_Input.GetSingleAction("TouchPadTop");
    //public SteamVR_Action_Single D = SteamVR_Input.GetSingleAction("TouchPadDown");
    //public SteamVR_Action_Single L = SteamVR_Input.GetSingleAction("TouchPadLeft");
    //public SteamVR_Action_Single R = SteamVR_Input.GetSingleAction("TouchPadRight");

    public SteamVR_Action_Vector2 TouchPad = SteamVR_Input.GetVector2Action("TouchPad");

    void Start()
    {
        left = transform.Find("SteamVRObjects/LeftHand").GetComponent<Hand>();
        right = transform.Find("SteamVRObjects/RightHand").GetComponent<Hand>();

        print("左手类型：" + left.handType);
        print("右手类型：" + right.handType);
    }


    void Update()
    {

        //if (T[SteamVR_Input_Sources.LeftHand].changed)
        //{
        //    Debug.Log("T输入：" + T[SteamVR_Input_Sources.LeftHand].axis);
        //}
        //if (D[SteamVR_Input_Sources.LeftHand].changed)
        //{
        //    Debug.Log("D输入：" + D[SteamVR_Input_Sources.LeftHand].axis);
        //}
        //if (L[SteamVR_Input_Sources.LeftHand].changed)
        //{
        //    Debug.Log("L输入：" + L[SteamVR_Input_Sources.LeftHand].axis);
        //}
        //if (R[SteamVR_Input_Sources.LeftHand].changed)
        //{
        //    Debug.Log("R输入：" + R[SteamVR_Input_Sources.LeftHand].axis);
        //}

        if (TouchPad[SteamVR_Input_Sources.LeftHand].changed)
        {
            Debug.Log("左手摇杆输入：" + TouchPad[SteamVR_Input_Sources.LeftHand].axis);
        }
        if (TouchPad[SteamVR_Input_Sources.RightHand].changed)
        {
            Debug.Log("右手摇杆输入：" + TouchPad[SteamVR_Input_Sources.RightHand].axis);
        }





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
        //if (right.GetGrabStarting() != GrabTypes.None)
        //{
        //    print("右手输入类型：" + right.GetGrabStarting());
        //}

        //if (snapLeftAction.GetStateDown(SteamVR_Input_Sources.LeftHand))
        //{
        //    print("左手输入向左:");
        //}
        //if (snapRightAction.GetStateDown(SteamVR_Input_Sources.LeftHand))
        //{
        //    print("左手输入向右:");
        //}
        //if (telepory.GetStateDown(SteamVR_Input_Sources.LeftHand))
        //{
        //    print("左手点击触摸板:");
        //}
        //if (snapLeftAction.GetStateDown(SteamVR_Input_Sources.RightHand))
        //{
        //    print("右手输入向左:");
        //}
        //if (snapRightAction.GetStateDown(SteamVR_Input_Sources.RightHand))
        //{
        //    print("右手输入向右:");
        //}
        //if (telepory.GetStateDown(SteamVR_Input_Sources.LeftHand))
        //{
        //    print("右手点击触摸板:");
        //}

    }



}

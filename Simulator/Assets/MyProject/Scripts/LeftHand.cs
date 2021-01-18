using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class LeftHand : HandBase
{
    #region Variable


    #endregion


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
        base.Start();
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

        if (teleport.GetState(SteamVR_Input_Sources.LeftHand))
        {
            InputTeleport();
        }
        if (teleport.GetStateUp(SteamVR_Input_Sources.LeftHand))
        {
            Teleport();
        }
        #endregion

        #region 正常行走并且是在没持有仪器的情况下,发送射线检测是否选中仪器
        Collider collider = null;
        if (mState == State.normal && holdInstrument == null)
        {
            //Ray ray = new Ray(transform.GetComponent<HandPhysics>().handCollider.transform.position, transform.GetComponent<HandPhysics>().handCollider.transform.forward);
            RaycastHit hitInfo;
            int layerMask = 1 << 12;
            layerMask = ~layerMask; //忽略player层
            if (Physics.SphereCast(HandDirection.position, 0.1f, HandDirection.forward, out hitInfo, 3f, layerMask))
            {
                //Debug.Log(hitInfo.collider.name);
                if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Instrument"))
                {
                    Debug.DrawLine(HandDirection.position, hitInfo.point, Color.red);
                    collider = hitInfo.collider;
                }
                //ShowLaser(hitInfo);
            }
            else
            {
                laser.SetActive(false);
                //Debug.DrawLine(ray.origin, transform.GetComponent<HandPhysics>().handCollider.transform.position + transform.GetComponent<HandPhysics>().handCollider.transform.forward * 3f, Color.red);
                Debug.DrawLine(HandDirection.position, HandDirection.forward * 3f, Color.red);
            }
        }
        else
        {
            laser.SetActive(false);
        }
        selectedInstrument = collider ? collider.gameObject : null;
        //如果右手选中了物体，左手不发射
        if (RightHand.Instance.selectedInstrument == null)
        {
            Messenger.Broadcast<Collider>(GlobalEvent.Player_Selected_Instrument, collider);
        }
        #endregion
    }
    private void LateUpdate()
    {
        if (holdInstrument != null)
        {
            holdInstrument.transform.rotation = Quaternion.Euler(0, holdInstrument.transform.eulerAngles.y, 0);
        }
    }

    protected override void InputTouchPad()
    {
        //Debug.Log("左手摇杆输入：" + TouchPad[SteamVR_Input_Sources.LeftHand].axis);

    }

    protected override void InputGrip()
    {
        //print("左手输入类型：" + hand.GetGrabStarting());
        if (holdInstrument)
        {
            DeleteHoldInstrument();
        }
    }

    protected override void InputTrigger()
    {
        //print("左手输入类型：" + hand.GetGrabStarting());

        if (selectedInstrument)
        {
            InputHeld();
        }
        else if (holdInstrument)
        {
            if (holdInstrument.GetHeldState() == Instrument.HeldState.green)
            {
                holdInstrument.transform.parent = null;
                holdInstrument.SetState(Instrument.State.drop);
                holdInstrument = null;
                SetState(State.normal);
            }
        }

    }

    protected override void InputTeleport()
    {
        base.InputTeleport();
    }

}

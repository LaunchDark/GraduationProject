using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class RightHand : HandBase
{
    #region Variable

    #endregion



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
        base.Start();
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

        #region 正常行走并且是在没持有仪器的情况下,发送射线检测是否选中仪器
        Collider collider = null;
        if (mState == State.normal && holdInstrument == null)
        {
            //Ray ray = new Ray(transform.GetComponent<HandPhysics>().handCollider.transform.position, transform.GetComponent<HandPhysics>().handCollider.transform.forward);
            RaycastHit hitInfo;
            int layerMask = 1 << 12;
            layerMask = ~layerMask; //忽略player层
            if (Physics.SphereCast(HandDirection.position,0.1f, HandDirection.forward, out hitInfo, 3f, layerMask))
            {
                //Debug.Log(hitInfo.collider.name);
                if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Instrument"))
                {
                    Debug.DrawLine(HandDirection.position, hitInfo.point, Color.red);
                    collider = hitInfo.collider;
                }
                ShowLaser(hitInfo);
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
        //如果左手选中了物体，右手不发射
        if (LeftHand.Instance.selectedInstrument == null)
        {
            Messenger.Broadcast<Collider>(GlobalEvent.Player_Selected_Instrument, collider);
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

        if (holdInstrument)
        {
            DeleteHoldInstrument();
        }
        else
        {
            Debug.Log("生成方块");
            PacksackMgr.Instance.CreatePlayerHoldInstrument(InstrumentEnum.Cube, true);
        }
    }

    protected override void InputTrigger()
    {
        print("右手输入类型：" + hand.GetGrabStarting());


        if (selectedInstrument)
        {
            InputHeld();
        }
        else if (holdInstrument)
        {
            if(holdInstrument.GetHeldState() == Instrument.HeldState.green)
            {
                holdInstrument.transform.parent = null;
                holdInstrument.SetState(Instrument.State.drop);
                holdInstrument = null;
                SetState(State.normal);
            }
        }
        else
        {
            Debug.Log("生成小球");
            PacksackMgr.Instance.CreatePlayerHoldInstrument(InstrumentEnum.Sphere, true);
        }


    }

    protected override void InputTeleport()
    {
        print("右手点击触摸板:");
    }

    /// <summary>
    /// 显示射线
    /// </summary>
    /// <param name="hit"></param>
    protected void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);
        laser.transform.position = Vector3.Lerp(HandDirection.position, hit.point,0.5f);
        laser.transform.LookAt(hit.point);
        laser.transform.localScale = new Vector3(laser.transform.localScale.x, laser.transform.localScale.y, hit.distance);
    }


}

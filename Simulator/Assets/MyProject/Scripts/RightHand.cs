using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class RightHand : HandBase
{
    #region Variable

    protected bool teleportDown = false;
    protected bool isOnUI = false;

    [SerializeField] protected float RotateSpeed = 1.0f;
    [SerializeField] protected float MoveSpeed = 1.0f;

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

        if (hand.GetGrabEnding() == GrabTypes.Grip)
        {
            //如果没有调用过其他UI
            if (GripDown && ScaleInstrument == null)
            {
                if (!ChangeCanvas.Instance.gameObject.activeSelf)
                {
                    //如果选中仪器，弹出仪器的材质选择UI
                    if (selectedInstrument != null)
                    {
                        if (selectedInstrument.GetComponentInParent<Instrument>())
                        {
                            ChangeCanvas.Instance.ShowUI(transform.position, selectedInstrument.GetComponentInParent<Instrument>());
                        }
                    }
                }
                else
                {
                    ChangeCanvas.Instance.HideUI();
                }
            }
            GripDown = false;
        }

        if (hand.GetGrabEnding() == GrabTypes.Pinch)
        {
            TriggerDown = false;
        }

        if (!GripDown && ScaleInstrument)
        {
            ScaleInstrument.ScaleHand = null;
            ScaleInstrument = null;
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

        if (teleport.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            teleportDown = true;
        }
        if (teleport.GetState(SteamVR_Input_Sources.RightHand))
        {
            InputTeleport();
        }
        if (teleport.GetStateUp(SteamVR_Input_Sources.RightHand))
        {
            Teleport();
            teleportDown = false;
        }
        #endregion

        #region 检测是否在UI上
        isOnUI = false;
        if (mState == State.normal && holdInstrument == null)
        {
            if (Physics.Raycast(HandDirection.position, HandDirection.forward, 1, 1 << 5))
            {
                isOnUI = true;
            }
        }
        #endregion


        #region 正常行走并且是在没持有仪器的情况下,发送射线检测是否选中仪器
        Collider collider = null;
        if (mState == State.normal && holdInstrument == null && !isOnUI && !teleportDown)
        {
            RaycastHit hitInfo;
            int layerMask = ~LayerMask.GetMask("UI", "Player");
            if (Physics.SphereCast(HandDirection.position, 0.1f, HandDirection.forward, out hitInfo, 3f, layerMask))
            {
                //Debug.Log(hitInfo.collider.name);
                if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Instrument")
                    || hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Wall")
                    || hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("TopWall")
                    || hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    {
                        Debug.DrawLine(HandDirection.position, hitInfo.point, Color.red);
                        collider = hitInfo.collider;
                    }
                }
                ShowLaser(hitInfo);
            }
            else
            {
                if (!teleportDown)
                {
                    laser.SetActive(false);
                }
                //Debug.DrawLine(ray.origin, transform.GetComponent<HandPhysics>().handCollider.transform.position + transform.GetComponent<HandPhysics>().handCollider.transform.forward * 3f, Color.red);
                //Debug.DrawLine(HandDirection.position, HandDirection.forward * 3f, Color.red);
            }
        }
        else
        {
            if (!teleportDown)
            {
                laser.SetActive(false);
            }
        }
        selectedInstrument = collider ? collider.gameObject : null;
        //如果左手选中了物体，右手不发射
        //if (LeftHand.Instance.selectedInstrument == null)
        {
            Messenger.Broadcast<Collider,string>(GlobalEvent.Player_Selected_Instrument, collider,"Right");
        }

        #endregion
    }

    protected override void InputTouchPad()
    {
        //Debug.Log("右手摇杆输入：" + TouchPad[SteamVR_Input_Sources.RightHand].axis);
        if (!teleportDown)
        {
            if (holdInstrument != null)
            {
                //手持仪器旋转
                if (TouchPad[SteamVR_Input_Sources.RightHand].axis.x > 0.3f)
                {
                    holdInstrument.transform.eulerAngles += new Vector3(0, -RotateSpeed, 0);
                }
                else if (TouchPad[SteamVR_Input_Sources.RightHand].axis.x < -0.3f)
                {
                    holdInstrument.transform.eulerAngles += new Vector3(0, RotateSpeed, 0);
                }

                //手持仪器移动
                if (TouchPad[SteamVR_Input_Sources.RightHand].axis.y > 0.3)
                {
                    if (holdInstrument.GetOffsetZ() < holdInstrument.MaxOffsetZ)
                    {
                        holdInstrument.SetOffsetZChange(0.01f);
                    }
                }
                else if (TouchPad[SteamVR_Input_Sources.RightHand].axis.y < -0.3f)
                {
                    if (holdInstrument.GetOffsetZ() > holdInstrument.MinOffsetZ)
                    {
                        holdInstrument.SetOffsetZChange(-0.01f);
                    }
                }                
            }
            else
            {
                if (TouchPad[SteamVR_Input_Sources.RightHand].axis.x > 0.6f)
                {
                    FindObjectOfType<SnapTurn>().RotatePlayer(60);
                }
                else if (TouchPad[SteamVR_Input_Sources.RightHand].axis.x < -0.6f)
                {
                    FindObjectOfType<SnapTurn>().RotatePlayer(-60);
                }
            }
        }
    }

    protected override void InputGrip()
    {
        //print("右手输入类型：" + hand.GetGrabStarting());

        if (holdInstrument)
        {
            DeleteHoldInstrument();
        }
        else
        {
            GripDown = true;
        }
    }

    protected override void InputTrigger()
    {
        //print("右手输入类型：" + hand.GetGrabStarting());
        if (GripDown)
        {
            GripDown = false;
            if (UIRoot.Instance.gameObject.activeSelf)
            {
                UIRoot.Instance.HideUIRoot();
            }
            else
            {
                UIRoot.Instance.ShowUIRoot(transform.position);
            }
            return;
        }

        if (selectedInstrument)
        {
            InputHeld();
        }
        else if (holdInstrument && !TriggerDown)
        {
            if(holdInstrument.GetHeldState() == Instrument.HeldState.green)
            {
                holdInstrument.transform.parent = null;
                if (holdInstrument.isHangInsturment)
                {
                    if (isWall)
                    {
                        holdInstrument.SetState(Instrument.State.life);
                    }
                    else
                    {
                        holdInstrument.SetState(Instrument.State.drop);
                    }
                }
                else if (holdInstrument.isFreeInstrument)
                {
                    holdInstrument.SetState(Instrument.State.free);
                }
                else
                {
                    holdInstrument.SetState(Instrument.State.drop);
                }
                holdInstrument = null;
                SetState(State.normal);
            }
        }
        TriggerDown = true;
    }

    protected override void InputTeleport()
    {
        base.InputTeleport();
    }



}

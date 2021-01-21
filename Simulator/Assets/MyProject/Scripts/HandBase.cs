using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class HandBase : MonoBehaviour
{
	//状态枚举
	public enum State
	{
		normal,//正常
		Instrument,//手持仪器
	}

	#region Variables

	/// <summary>
	/// 持有的仪器对象
	/// </summary>
	[SerializeField] public Instrument holdInstrument;

	/// <summary>
	/// 选中的仪器对象
	/// </summary>
	[SerializeField] public GameObject selectedInstrument;

	//当前状态
	[SerializeField] protected State mState = State.normal;

	/// <summary>
	/// 手部代码
	/// </summary>
	[SerializeField] protected Hand hand;
	/// <summary>
	/// 触摸板
	/// </summary>
	[SerializeField] protected SteamVR_Action_Vector2 TouchPad = SteamVR_Input.GetVector2Action("TouchPad");
	/// <summary>
	/// Teleprot
	/// </summary>
	[SerializeField] protected SteamVR_Action_Boolean teleport = SteamVR_Input.GetBooleanAction("Teleport");
	/// <summary>
	/// 控制器位置
	/// </summary>
	[SerializeField] protected SteamVR_Behaviour_Pose controllerPos;
	/// <summary>
	/// VR摄像头
	/// </summary>
	[SerializeField] protected Transform HeadTransform;
	/// <summary>
	/// 仪器吸附位置
	/// </summary>
	[SerializeField] protected Transform ObjectAttachmentPoint;
	protected Vector3 startAttachmentPoint;
	protected Vector3 startAttachmentRote;


	public GameObject laserprefab;
	public GameObject laser;
	public Transform HandDirection;

	/// <summary>
	/// 传送射线检测层级
	/// </summary>
	public LayerMask TeleportLayerMask;
	public GameObject TeleportPanelPrefab;
	public GameObject TeleportPanel;
	protected Vector3 teleportPoint;
	protected bool shouldTeleport = false;


	#endregion

	protected virtual void Awake()
    {

    }

	protected virtual void Start()
	{
		controllerPos = transform.GetComponent<SteamVR_Behaviour_Pose>();

		laser = Instantiate(laserprefab);
		TeleportPanel = Instantiate(TeleportPanelPrefab);
		laser.SetActive(false);
		TeleportPanel.SetActive(false);

		hand = transform.GetComponent<Hand>();

		startAttachmentPoint = ObjectAttachmentPoint.position;
		startAttachmentRote = ObjectAttachmentPoint.eulerAngles;

	}

	protected virtual void Update()
    {

    }

    protected virtual void LateUpdate()
	{
		if (holdInstrument != null)
		{
			if (holdInstrument.isInWall)
			{
				//重复移位
				holdInstrument.transform.eulerAngles = holdInstrument.isInWall.eulerAngles;
				holdInstrument.transform.position = holdInstrument.transform.position + (holdInstrument.isInWall.forward * (holdInstrument.radius - holdInstrument.test));
			}
			else
			{
				holdInstrument.transform.rotation = Quaternion.Euler(0, holdInstrument.transform.eulerAngles.y, 0);
				holdInstrument.transform.localPosition = new Vector3(0, 0, holdInstrument.GetOffsetZ());
			}
		}
	}

    /// <summary>
    /// 触摸板事件
    /// </summary>
    protected virtual void InputTouchPad()
    {

    }

	/// <summary>
	/// 点击Trigger事件
	/// </summary>
	protected virtual void InputTrigger()
    {

    }

	/// <summary>
	/// 点击握持键数据
	/// </summary>
	protected virtual void InputGrip()
    {

    }

	/// <summary>
	/// 点击触摸板事件
	/// </summary>
	protected virtual void InputTeleport()
    {
		if (mState == State.normal && holdInstrument == null)
		{			
			RaycastHit hitInfo;
			if (Physics.Raycast(HandDirection.position, HandDirection.forward, out hitInfo, 10f, TeleportLayerMask))
			{
				teleportPoint = hitInfo.point;
				ShowLaser(hitInfo);
				TeleportPanel.SetActive(true);
				TeleportPanel.transform.position = hitInfo.point + new Vector3(0, 0.01f, 0);//深度缓冲问题
				shouldTeleport = true;
			}
			else
			{
				laser.SetActive(false);
				TeleportPanel.SetActive(false);
				Debug.DrawLine(HandDirection.position, HandDirection.forward * 3f, Color.red);
			}
		}
	}

	protected virtual void Teleport()
    {
		shouldTeleport = false;
		TeleportPanel.SetActive(false);
		Vector3 diff = Player.instance.transform.position - HeadTransform.position;
		diff.y = 0;
		Player.instance.transform.position = teleportPoint + diff;
    }

	/// <summary>
	/// 获取手部状态
	/// </summary>
	public virtual State GetState()
	{
		return mState;
	}

	/// <summary>
	/// 设置手部状态
	/// </summary>
	/// <param name="state"></param>
	public virtual void SetState(State state)
	{
		mState = state;
		Messenger.Broadcast<State>(GlobalEvent.Player_State_Change, state);
		//正常状态
		if (mState == State.normal)
		{

		}
		if(mState == State.Instrument)
        {

        }
	}

	/// <summary>
	/// 放下仪器事件
	/// </summary>
	public virtual void InputTakeDown()
	{
		if (holdInstrument && mState == State.normal)
		{
			if (holdInstrument.GetHeldState() == Instrument.HeldState.green)
			{
				holdInstrument.transform.parent = null;
				holdInstrument.SetState(Instrument.State.drop);
				holdInstrument = null;
			}
			else if (holdInstrument.GetHeldState() == Instrument.HeldState.red)
			{
				UIMgr.Instance.ShowTips("仪器不可放下", "#FF0000");
			}
		}
	}

	/// <summary>
	/// 调整仪器距离事件
	/// </summary>
	/// <param name="f">仪器距离</param>
	public virtual void InputDisitance(float f)
	{
		if (holdInstrument && mState == State.normal)
		{
			holdInstrument.SetOffsetZChange(f);
		}
	}


	/// <summary>
	/// 回收仪器事件
	/// </summary>
	public virtual void InputRecovery()
	{
		if (holdInstrument)
		{
			DeleteHoldInstrument();
		}
	}

	/// <summary>
	/// 拿起仪器事件
	/// </summary>
	public virtual void InputHeld()
	{
		if (mState == State.normal && selectedInstrument)
		{
			Instrument instrument = selectedInstrument.GetComponentInParent<Instrument>();
			if (instrument.isHasR)
			{ SetHoldInstrument(instrument); }
		}
	}

	/// <summary>
	///设置持有的仪器
	/// </summary>
	/// <param name="instrument"></param>
	public virtual void SetHoldInstrument(Instrument instrument)
	{
		if (holdInstrument)
		{
			DeleteHoldInstrument();
		}
		//ResetAttachmentPoint();
		holdInstrument = instrument;
		instrument.gameObject.SetActive(true);
		instrument.SetState(Instrument.State.held, gameObject);
		holdInstrument.transform.SetParent(transform.GetChild(1).transform);
		holdInstrument.transform.localPosition = Vector3.zero;
		holdInstrument.transform.localRotation = Quaternion.Euler(Vector3.zero);
		SetState(State.Instrument);
	}

	/// <summary>
	///删除玩家持有的仪器
	/// </summary>
	public virtual void DeleteHoldInstrument()
	{
		InstrumentMgr.Instance.DeleteInstrument(holdInstrument);
        holdInstrument.transform.SetParent(null);

		holdInstrument = null;
		SetState(State.normal);
	}


	/// <summary>
	/// 显示射线
	/// </summary>
	/// <param name="hit"></param>
	protected virtual void ShowLaser(RaycastHit hit)
	{
		laser.SetActive(true);
		laser.transform.position = Vector3.Lerp(HandDirection.position, hit.point, 0.5f);
		laser.transform.LookAt(hit.point);
		laser.transform.localScale = new Vector3(laser.transform.localScale.x, laser.transform.localScale.y, hit.distance);
	}

	/// <summary>
	/// 重置吸附点
	/// </summary>
	protected virtual void ResetAttachmentPoint()
    {
		ObjectAttachmentPoint.position = startAttachmentPoint;
		ObjectAttachmentPoint.eulerAngles = startAttachmentRote;
    }
}

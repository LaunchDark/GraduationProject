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
	[SerializeField] protected GameObject selectedInstrument;

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
	[SerializeField] protected SteamVR_Action_Boolean telepory = SteamVR_Input.GetBooleanAction("Teleport");
	#endregion
	protected virtual void Awake()
    {

    }

	protected virtual void Start()
    {

    }

	protected virtual void Update()
    {

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
			if (!gameObject.activeSelf)
				gameObject.SetActive(true);

			this.enabled = true;
		}
		//仪器内部状态
		if (mState == State.Instrument)
		{
			//gameObject.SetActive(false);

			Messenger.Broadcast<Collider>(GlobalEvent.Player_Selected_Instrument, null);
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
		holdInstrument = instrument;
		instrument.gameObject.SetActive(true);
		instrument.SetState(Instrument.State.held,gameObject);
		holdInstrument.transform.SetParent(transform);
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


}

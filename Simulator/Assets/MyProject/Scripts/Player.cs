using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private static Player _instance;
	public static Player Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<Player>();
			}
			return _instance;
		}
	}
	//玩家状态枚举
	public enum State
	{
		normal,//正常
		Instrument,//仪器
	}

# region Variables
	//玩家视角摄像机
	[HideInInspector]
	public Camera camera;

	[HideInInspector]
	public GameObject rightHand;

	//玩家持有的仪器对象
	[SerializeField]
	public Instrument holdInstrument;

	//玩家选中的仪器对象
	[SerializeField]
	private GameObject selectedInstrument;

	//玩家当前状态
	[SerializeField]
	private State mState = State.normal;

    #endregion

    private void Start()
    {
		rightHand = GameObject.Find("RightHand");
    }


    //获取玩家状态
    public State GetState()
	{
		return mState;
	}

	//设置玩家状态
	public void SetState(State state)
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

	//按下鼠标左键的处理
	public void InputGetMouseButtonDownByLeft()
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

	//鼠标滚轮的处理
	public void InputMouseScrollWheel(float f)
	{
		if (holdInstrument && mState == State.normal)
		{
			holdInstrument.SetOffsetZChange(f);
		}
	}


	//按键X处理
	public void InputKeyDownX()
	{
		if (holdInstrument)
		{
			DeleteHoldInstrument();
		}
	}

	//按键R处理
	public void InputKeyDownR()
	{
		if (mState == State.normal && selectedInstrument)
		{
			Instrument instrument = selectedInstrument.GetComponentInParent<Instrument>();
			if (instrument.isHasR)
			{ SetHoldInstrument(instrument); }
		}
	}

	/// <summary>
	/// 放置仪器在场景上
	/// </summary>
	public Instrument CreatItemByLife(InstrumentEnum type, Vector3 localPosition, Vector3 localEulerAngles)
	{
		PacksackMgr.Instance.CreatePlayerHoldInstrument(type, true);
		Player.Instance.holdInstrument.transform.parent = null;
		Player.Instance.holdInstrument.transform.localPosition = localPosition;
		Player.Instance.holdInstrument.transform.localEulerAngles = localEulerAngles;
		Player.Instance.holdInstrument.SetState(Instrument.State.drop);
		Instrument tempInstrument = Player.Instance.holdInstrument;
		Player.Instance.holdInstrument = null;
		return tempInstrument;
	}

	//设置玩家持有的仪器
	public void SetHoldInstrument(Instrument instrument)
	{
		if (holdInstrument)
		{
			DeleteHoldInstrument();
		}
		holdInstrument = instrument;
		instrument.gameObject.SetActive(true);
		instrument.SetState(Instrument.State.held);
		holdInstrument.transform.SetParent(rightHand.transform);
	}

	//删除玩家持有的仪器
	public void DeleteHoldInstrument()
	{
		InstrumentMgr.Instance.DeleteInstrument(holdInstrument);
		holdInstrument.transform.SetParent(null);

		holdInstrument = null;
		SetState(State.normal);
	}


}

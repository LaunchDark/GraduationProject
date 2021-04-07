using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

/// <summary>
/// 手部基类
/// </summary>
public class HandBase : MonoBehaviour
{
	//状态枚举
	public enum State
	{
		normal,//正常
		Instrument,//手持家具
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
	
	/// <summary>
	/// 是否是天花板/墙
	/// </summary>
	protected bool isWall = false;
	/// <summary>
	/// 按下grip键
	/// </summary>
	protected bool GripDown = false;
	/// <summary>
	/// 按下Trigger键
	/// </summary>
	protected bool TriggerDown = false;
	/// <summary>
	/// 控制缩放的物体
	/// </summary>
	public Instrument ScaleInstrument;

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
			//手持物体时，发射射线检测，选中的墙，碰撞点向下面发射射线，然后根据物体厚度，高度，和墙的方向，调整手上物体
			isWall = false;
			Ray ray = new Ray(HandDirection.position, HandDirection.forward);
			RaycastHit hitInfo;
			//只检测墙体层 layer = 9
			if (Physics.Raycast(ray, out hitInfo, holdInstrument.GetOffsetZ() + (holdInstrument.width / 2 * holdInstrument.transform.localScale.z), LayerMask.GetMask("Wall")))
			{
				isWall = true;
				holdInstrument.transform.eulerAngles = hitInfo.transform.eulerAngles;
				holdInstrument.transform.position = hitInfo.point + hitInfo.transform.forward * (holdInstrument.width / 2 * holdInstrument.transform.localScale.z);
			}
			//只检测天花板 layer = 10
			else if (Physics.Raycast(ray, out hitInfo, holdInstrument.GetOffsetZ() + (holdInstrument.height / 2 * holdInstrument.transform.localScale.y), LayerMask.GetMask("TopWall")))
			{
				isWall = true;
				holdInstrument.transform.eulerAngles = Vector3.up;
				holdInstrument.transform.position = hitInfo.point + hitInfo.transform.forward * (holdInstrument.height / 2 * holdInstrument.transform.localScale.y);
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
		bool can = true;
		if (mState == State.normal && holdInstrument == null)
		{
			RaycastHit hitInfo;
			int layer = ~LayerMask.GetMask("Door");
			//忽略门
			if (Physics.Raycast(HandDirection.position, HandDirection.forward, out hitInfo, 5f,layer))
			{
				bool isFloot = false;
				//如果是地毯
				if (hitInfo.collider.GetComponentInParent<Instrument>())
				{
					isFloot = hitInfo.collider.GetComponentInParent<Instrument>().isFloot;
				}
				if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground") || isFloot)
				{
					teleportPoint = hitInfo.point;
					ShowLaser(hitInfo);
					TeleportPanel.SetActive(true);
					TeleportPanel.transform.position = hitInfo.point + new Vector3(0, 0.01f, 0);//深度缓冲问题
					TeleportPanel.transform.LookAt(Player.instance.transform);
					//TeleportPanel.transform.rotation = Quaternion.Euler(new Vector3(0, TeleportPanel.transform.rotation.y + 180, 0));
					shouldTeleport = true;
				}
				else
				{
					shouldTeleport = false;
					can = false;
				}

			}
			else
			{
				shouldTeleport = false;
				can = false;
			}
			if (!can)
			{
				shouldTeleport = false;
				laser.SetActive(false);
				TeleportPanel.SetActive(false);
				Debug.DrawLine(HandDirection.position, HandDirection.forward * 3f, Color.red);
			}
		}
	}

	protected virtual void Teleport()
	{
		TeleportPanel.SetActive(false);
		if (shouldTeleport)
		{
			shouldTeleport = false;
			Vector3 diff = Player.instance.transform.position - HeadTransform.position;
			diff.y = 0;
			Player.instance.transform.position = teleportPoint + diff;
		}
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
	/// 拿起仪器事件
	/// </summary>
	public virtual void InputHeld()
	{
		if (mState == State.normal && selectedInstrument)
		{
			Instrument instrument = selectedInstrument.GetComponentInParent<Instrument>();
			if (instrument)
			{
				if (instrument.isHasR && instrument.mState != Instrument.State.held)
				{ SetHoldInstrument(instrument); }
			}
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
		holdInstrument.transform.localRotation = Quaternion.Euler(new Vector3(0,180,0));
		//holdInstrument.transform.localRotation = Quaternion.Euler(Vector3.zero);
		//holdInstrument.transform.LookAt(Player.instance.transform);
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

	public bool GetGripDown()
    {
		return GripDown;
    }

	public bool GetTriggerDown()
    {
		return TriggerDown;
    }

	public void SetTriggerDown(bool b)
    {
		TriggerDown = b;
    }
}

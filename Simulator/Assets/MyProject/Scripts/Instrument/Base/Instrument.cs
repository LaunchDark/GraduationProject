//using OfficeOpenXml.FormulaParsing.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using UnityScript.Steps;

public class Instrument : MonoBehaviour
{
    public enum State
    {
        normal,//正常
        held,//被玩家持有
        drop,//仪器下落的途中
        life,//放在场景上
        enter,//进入仪器操作
    }

    public enum HeldState
    {
        normal,//正常
        green,//绿色
        red,//红色
    }

    [HideInInspector] public InstrumentEnum type = InstrumentEnum.None;
    protected bool isHasF = false;
    [HideInInspector] public bool isHasR = true;


    public State mState = State.normal;
    [HideInInspector] public State mLastState = State.normal;
    private HeldState mHeldState = HeldState.normal;

    private Rigidbody rig;

    public float MaxOffsetZ = 3;
    public float MinOffsetZ = 0;
    private float offsetZ = 1;
    public float canDropDis = 1;//设置值为0时,仪器不能放在地面上

    //原来网格的材质
    private Renderer[] renderers;
    private List<Material[]> materials = new List<Material[]>();
    private Material greenMaterial;
    private Material redMaterial;
    private OutLineTargetComponent outLineTargetComponent;
    private bool curOutLine = false;

    private List<Collider> colliders;

    //private Player player;

    private bool isHeldCollision = false;

    [SerializeField] public Collider adsorbCollider;//吸附范围碰撞体
    [SerializeField] protected List<InstrumentEnum> adsorbTypeList;//可吸附仪器类型列表
    [SerializeField] public Instrument curAdsorbInstrument;//按下左键时,将要吸附的仪器
    [SerializeField] public Instrument subInstrument;//吸附到的子仪器

    [HideInInspector] public List<Instrument> groupInstrumentList;//组合仪器列表
    [HideInInspector] public InstrumentEnum groupInstrumentType;//组合仪器类型

    protected InstrumentLineRender lineRender;

    [HideInInspector] public int selfSortIndex;//背包定位排序值
    [HideInInspector] static int sortMaxIndex;//背包定位排序值

    protected GameObject HeldingHand;
    /// <summary>
    /// 被选中
    /// </summary>
    protected bool isSelecting;
    /// <summary>
    /// 在墙里
    /// </summary>
    [HideInInspector] public Transform isInWall;
    /// <summary>
    /// 宽度
    /// </summary>
    public float width;
    /// <summary>
    /// 高度
    /// </summary>
    public float height;
    /// <summary>
    /// 悬空仪器（挂灯，壁灯等）
    /// </summary>
    public bool isHangInsturment = false;


    private void Awake()
    {
        mState = State.normal;
        rig = gameObject.GetComponent<Rigidbody>();
        greenMaterial = (Material)ResMgr.Instance.LoadByCore("Material/GreenMaterial");
        redMaterial = (Material)ResMgr.Instance.LoadByCore("Material/RedMaterial");
        outLineTargetComponent = gameObject.GetComponent<OutLineTargetComponent>();
        if (outLineTargetComponent == null)
            outLineTargetComponent = gameObject.AddComponent<OutLineTargetComponent>();
        //player = Player.Instance;
        offsetZ = MinOffsetZ;

        renderers = transform.GetComponentsInChildren<Renderer>(true);
        for (int i = 0; i < renderers.Length; i++)
        {
            materials.Add(renderers[i].materials);
        }
        colliders = transform.GetComponentsInChildren<Collider>(true).ToList();
        for (int i = colliders.Count - 1; i >= 0; i--)
        {
            if (colliders[i].GetComponent<MeshCollider>() != null)
                colliders.Remove(colliders[i]);
        }
        if (adsorbCollider)
        {
            adsorbCollider.enabled = false;
            adsorbCollider.isTrigger = true;
            adsorbTypeList = null;
            colliders.Remove(adsorbCollider);
        }
        SetState(State.normal);
        Messenger.AddListener<Collider>(GlobalEvent.Player_Selected_Instrument, SelectedInsturment);
        lineRender = UITool.Instantiate("Instruments/LineRender", gameObject).GetComponent<InstrumentLineRender>();
        Messenger.AddListener(GlobalEvent.Enter_UI, EnterUI);
        Messenger.AddListener(GlobalEvent.Exit_UI, ExitUI);
        AwakeLater();
    }
    private void EnterUI()
    {
        //if (mState == State.enter && mInputKeyInterface != null) 
        //InputKeyMgr.Instance.Remove(mInputKeyInterface);
    }
    private void ExitUI()
    {
        //if (mState == State.enter && mInputKeyInterface != null)
        //InputKeyMgr.Instance.Register(mInputKeyInterface);
    }

    virtual public void AwakeLater() { }

    private void LateUpdate()
    {
        if (mState == State.held && HeldingHand.GetComponent<HandBase>().GetState() == HandBase.State.Instrument)
        {
            //持有仪器时往地面画线
            lineRender.gameObject.SetActive(true);
            //吸附模式
            if (curAdsorbInstrument)
            {
                lineRender.gameObject.SetActive(false);
                return;
            }
            //放下模式
            //持有仪器并且没有碰到别的物体时,往地上打射线,检测距离,判断是否可以放下
            if (!isHeldCollision)
            {
                Ray ray = new Ray(transform.position, transform.up * -1f);
                RaycastHit hitInfo;
                Debug.DrawRay(transform.position, transform.up * -1f * canDropDis, Color.red);
                if (Physics.Raycast(ray, out hitInfo, canDropDis, LayerMask.GetMask("Ground")))
                {
                    SetRenderer(HeldState.green);
                }
                else
                {
                    SetRenderer(HeldState.red);
                }
            }
            //持有仪器并且没有碰到别的物体时,屏幕中间发送射线,检测碰撞,判断是否有障碍物档在中间
            //if (mHeldState == HeldState.green && !isHeldCollision)
            //{
            //    Ray ray = new Ray(HeldingHand.transform.position, HeldingHand.transform.forward);
            //    RaycastHit hitInfo;
            //    //float distance = Vector3.Distance(HeldingHand.transform.position, transform.position);
            //    if (Physics.Raycast(ray, out hitInfo, 2))
            //    {
            //        Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
            //        if (hitInfo.collider.GetComponentInParent<Instrument>() != this)
            //            SetRenderer(HeldState.red);
            //    }
            //}
        }
        else
        {
            lineRender.gameObject.SetActive(false);
        }
    }

    /// <summary>
    ///设置仪器状态
    /// </summary>
    /// <param name="state">状态</param>
    /// <param name="hand">设置握持手</param>
    public void SetState(State state, GameObject hand = null)
    {
        HeldingHand = hand;
        if (rig == null)
            rig = gameObject.AddComponent<Rigidbody>();
        if (mState != state)
        {
            mLastState = mState;
            mState = state;
        }
        if (adsorbCollider) adsorbCollider.enabled = false;
        Messenger.Broadcast<Instrument>(GlobalEvent.Instrument_State_Change, this);
        if (mState == State.normal)
        {
            rig.isKinematic = true;
            for (int i = 0; i < colliders.Count; i++)
                colliders[i].isTrigger = false;
            SetRenderer(HeldState.normal);
        }
        if (mState == State.held)
        {
            if (curAdsorbInstrument)
            {
                curAdsorbInstrument.adsorbCollider.enabled = true;
                curAdsorbInstrument.subInstrument = null;
                curAdsorbInstrument = null;
            }
            rig.isKinematic = true;
            isHeldCollision = false;
            for (int i = 0; i < colliders.Count; i++)
                colliders[i].isTrigger = true;
            SetRenderer(HeldState.green);
            HeldCallBack(hand);
        }
        if (mState == State.drop)
        {
            if (curAdsorbInstrument)
            {
                //吸附模式
                SetState(State.life);
                AdsorbCallBack();
                curAdsorbInstrument.adsorbCollider.enabled = false;
                curAdsorbInstrument.subInstrument = this;
            }
            else
            {
                //放下模式
                rig.isKinematic = false;
                for (int i = 0; i < colliders.Count; i++)
                    colliders[i].isTrigger = false;
                SetRenderer(HeldState.normal);
                FallCourse();
            }
        }
        if (mState == State.life)
        {
            if (adsorbCollider && subInstrument == null) adsorbCollider.enabled = true;
            rig.isKinematic = true;
            for (int i = 0; i < colliders.Count; i++)
                colliders[i].isTrigger = true;
            SetRenderer(HeldState.normal);
            LifeCallBack();
            sortMaxIndex++;
            selfSortIndex = sortMaxIndex;
        }
    }



    /// <summary>
    ///子类重写仪器被拿起时的回调
    /// </summary>
    /// <param name="hand"></param>
    virtual public void HeldCallBack(GameObject hand)
    {
        HeldingHand = hand;
    }

    /// <summary>
    ///子类重写仪器放下状态完成后的回调
    /// </summary>
    virtual public void LifeCallBack() { }

    /// <summary>
    ///子类重写仪器吸附的回调
    /// </summary>
    virtual public void AdsorbCallBack() { }

    /// <summary>
    /// 仪器下落的回调
    /// </summary>
    virtual public void FallCourse() { }

    /// <summary>
    ///仪器在场景中被玩家选中时
    /// </summary>
    /// <param name="collider"></param>
    private void SelectedInsturment(Collider collider)
    {
        //Debug.Log("选中: " + transform.name);
        if (mState == State.life)
        {
            if (collider == null)
            {
                SetOutLine(false);
            }
            else if (collider.transform.GetComponentInParent<Instrument>() == this)
            {
                SetOutLine(true);
            }
            else
            {
                SetOutLine(false);
            }
        }
        else
        {
            SetOutLine(false);
        }
    }


    /// <summary>
    ///设置描边
    /// </summary>
    /// <param name="b"></param>
    private void SetOutLine(bool b)
    {
        if (curOutLine == b)
            return;
        curOutLine = b;
        if (b)
            outLineTargetComponent.SetColor(Color.green);
        else
            outLineTargetComponent.SetColor(Color.black);
    }

    public float GetOffsetZ()
    {
        return offsetZ;
    }

    /// <summary>
    ///返回被持有时的颜色状态
    /// </summary>
    /// <returns></returns>
    public HeldState GetHeldState()
    {
        return mHeldState;
    }

    /// <summary>
    ///设置被持有时Z坐标偏移量
    /// </summary>
    /// <param name="f"></param>
    public void SetOffsetZChange(float f)
    {
        offsetZ += f;
        if (offsetZ > MaxOffsetZ)
            offsetZ = MaxOffsetZ;
        if (offsetZ < MinOffsetZ)
            offsetZ = MinOffsetZ;
    }

    public bool GetIsHasF()
    {
        return isHasF;
    }

    //判断是否是组合仪器
    public bool IsGroupInstrument()
    {
        return groupInstrumentList != null && groupInstrumentList.Count > 0;
    }

    /// <summary>
    /// 仪器颜色
    /// </summary>
    /// <param name="heldState"></param>
    public void SetRenderer(HeldState heldState)
    {
        mHeldState = heldState;
        if (heldState == HeldState.normal)
        {
            //Debug.Log("默认材质");
            for (int i = 0; i < renderers.Length; i++)
                renderers[i].materials = materials[i];
        }
        if (heldState == HeldState.green)
        {
            //Debug.Log("绿色材质");
            for (int i = 0; i < renderers.Length; i++)
                renderers[i].material = greenMaterial;
        }
        if (heldState == HeldState.red)
        {
            //Debug.Log("红色材质");
            for (int i = 0; i < renderers.Length; i++)
                renderers[i].material = redMaterial;
        }
        Instrument[] temp = transform.GetComponentsInChildren<Instrument>();
        for (int i = 1; i < temp.Length; i++)
        {
            temp[i].SetRenderer(heldState);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (mState == State.held /*&& player*/)
        {
            if (other.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                isInWall = other.transform;
            }
            else 
            {                
                //Debug.Log("碰撞到的物体" + other.gameObject.name);
                Instrument adsorbInstrument = other.gameObject.GetComponentInParent<Instrument>();
                //Debug.Log(adsorbInstrument);
                //碰到的对象不是仪器,变红
                if (adsorbInstrument == null)
                {
                    if (other.gameObject.layer == 12)
                    {
                        return;
                    }
                    SetRenderer(HeldState.red);
                    isHeldCollision = true;
                }
                else
                {//碰到的对象是仪器
                    if (groupInstrumentList != null && groupInstrumentList.Contains(adsorbInstrument))
                    {
                        //是自己的内部仪器,什么都不做
                    }
                    else
                    {
                        isHeldCollision = true;
                        //不是自己的内部仪器
                        //判断是否可以吸附
                        if (adsorbTypeList != null && adsorbInstrument.adsorbCollider == other && adsorbTypeList.Contains(adsorbInstrument.type))
                        {
                            //可以吸附
                            curAdsorbInstrument = adsorbInstrument;
                            SetRenderer(HeldState.green);
                        }
                        else
                        {
                            //不可以吸附
                            SetRenderer(HeldState.red);
                        }
                    }
                }
            } 
        }
    }

    private void OnTriggerStay(Collider other)
    {
        OnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        isInWall = null;
        if (mState == State.held /*&& player*/)
        {
            isHeldCollision = false;
            curAdsorbInstrument = null;
            SetRenderer(HeldState.green);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (mState == State.drop /*&& player*/)
            {
                SetState(State.life);
            }
        }

    }
}

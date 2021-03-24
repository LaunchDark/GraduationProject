using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRoot : MonoBehaviour
{
    protected static UIRoot instance;
    public static UIRoot Instance
    {
        get
        {
            if (instance == null)
            {
                instance = UITool.Instantiate("UI/UIRoot").GetComponent<UIRoot>();
            }
            return instance;
        }
    }

    public float distance = 0.1f;
    [HideInInspector] public Transform Left;
    [HideInInspector] public Transform Right;
    [HideInInspector] public Transform Top;

    [HideInInspector] public Dictionary<int, Packsack> AllPacksack;

    protected List<mButton> mButtons;

    [SerializeField] protected mButton PlayBtn;
    [SerializeField] protected mButton ExitBtn;
    [HideInInspector] public bool isPlaying = false;
    public void Init()
    {
        //HideUIRoot();
        Left = transform.Find("Left");
        Right = transform.Find("Right");
        Top = transform.Find("Top");

        AllPacksack = new Dictionary<int, Packsack>();
        foreach (InstrumentTypeEnum value in InstrumentTypeEnum.GetValues(typeof(InstrumentTypeEnum)))
        {
            Packsack packsack = UITool.Instantiate("Packsack/Packsack", Right.gameObject).GetComponent<Packsack>();
            AllPacksack.Add((int)value, packsack);
            packsack.Init(value.ToString());
        }

        Left.Find("Viewport/Content").gameObject.AddComponent<TypeSelect>().Init(AllPacksack.Count);

        AddFunctionalButton();

        Left.gameObject.SetActive(false);
        Right.gameObject.SetActive(false);

        PlayBtn.clickCallBack = Play;
        ExitBtn.clickCallBack = Exit;
        Vector3 pos = Valve.VR.InteractionSystem.Player.instance.transform.position;
        ShowUIRoot(new Vector3(pos.x, pos.y + 1, pos.z + 0.5f));
    }

    private void Update()
    {
        transform.LookAt(transform.position + (transform.position - Valve.VR.InteractionSystem.Player.instance.transform.GetComponentInChildren<Camera>().transform.position).normalized * 1.0f);
        //Debug.Log(transform.localEulerAngles.x);
        if (transform.localEulerAngles.x > 15 && transform.localEulerAngles.x < 180)
        {
            transform.localEulerAngles = new Vector3(15, transform.localEulerAngles.y, 0);
        }
        else if (transform.localRotation.x > 180 && transform.localEulerAngles.x < 345)
        {
            transform.localEulerAngles = new Vector3(345, transform.localEulerAngles.y, 0);
        }
    }

    public void ShowUIRoot(Vector3 pos)
    {
        //transform.position = LeftHand.Instance.transform.position + (LeftHand.Instance.transform.position - Valve.VR.InteractionSystem.Player.instance.transform.GetComponentInChildren<Camera>().transform.position).normalized * distance;
        transform.position = pos + (pos - Valve.VR.InteractionSystem.Player.instance.transform.GetComponentInChildren<Camera>().transform.position).normalized * distance;
        gameObject.SetActive(true);
    }

    public void HideUIRoot()
    {
        if (isPlaying == true)
            gameObject.SetActive(false);
    }

    /// <summary>
    /// 添加功能性按键
    /// </summary>
    protected void AddFunctionalButton()
    {
        mButtons = new List<mButton>();

        for (int i = 0; i < 4; i++)
        {
            mButton btn = UITool.Instantiate("UI/Base/Button", Left.Find("Viewport/Content").gameObject).GetComponent<mButton>();
            mButtons.Add(btn);
        }

        mButtons[0].Init("截取屏幕", () => TipsCanvas.Instance.CountDown());
        mButtons[1].Init("收回所有家具", () => InstrumentMgr.Instance.DeleteSceneAllInstrument());
        mButtons[2].Init("切换默认灯光", () => ClosePlayerLight());
        mButtons[3].Init("返回", () => Back());
    }

    protected void ClosePlayerLight()
    {
        Valve.VR.InteractionSystem.Player.instance.transform.Find("SteamVRObjects/BodyCollider/Point Light").gameObject.
            SetActive(!Valve.VR.InteractionSystem.Player.instance.transform.Find("SteamVRObjects/BodyCollider/Point Light").gameObject.activeSelf);
    }

    /// <summary>
    /// 开始
    /// </summary>
    protected void Play()
    {
        isPlaying = true;
        Left.gameObject.SetActive(true);
        Right.gameObject.SetActive(true);
        Top.gameObject.SetActive(false);
    }

    protected void Back()
    {
        PlayBtn.SetText("继续");
        Left.gameObject.SetActive(false);
        Right.gameObject.SetActive(false);
        Top.gameObject.SetActive(true);
    }

    protected void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

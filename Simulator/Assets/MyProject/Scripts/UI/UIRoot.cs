using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    public Transform Left;
    public Transform Right;
    public Transform Top;
    public Transform Save;


    [HideInInspector] public Dictionary<int, Packsack> AllPacksack;

    protected List<mButton> mButtons;

    [SerializeField] protected mButton PlayBtn;
    [SerializeField] protected mButton ExitBtn;
    [SerializeField] protected mButton SaveBtn;
    [SerializeField] protected mButton LoadBtn;
    [SerializeField] protected mButton LoadReturnBtn;

    protected mButton[] AllSave;
    public void Init()
    {
        //HideUIRoot();
        //Left = transform.Find("Left");
        //Right = transform.Find("Right");
        //Top = transform.Find("Top");
        //Save = transform.Find("Save");

        AllPacksack = new Dictionary<int, Packsack>();
        foreach (InstrumentTypeEnum value in InstrumentTypeEnum.GetValues(typeof(InstrumentTypeEnum)))
        {
            Packsack packsack = UITool.Instantiate("Packsack/Packsack", Right.gameObject).GetComponent<Packsack>();
            AllPacksack.Add((int)value, packsack);
            packsack.Init(value.ToString());
        }

        Left.Find("Viewport/Content").gameObject.AddComponent<TypeSelect>().Init(AllPacksack.Count);

        AddFunctionalButton();

        PlayBtn.clickCallBack = Play;
        ExitBtn.clickCallBack = Exit;
        SaveBtn.clickCallBack = SaveMgr.Instance.SaveGame;
        LoadBtn.clickCallBack = SavePanel;
        LoadReturnBtn.clickCallBack = LoadReturn;

        Vector3 pos = Valve.VR.InteractionSystem.Player.instance.transform.position;
        ShowUIRoot(new Vector3(pos.x, pos.y + 1, pos.z + 0.5f));

        AllSave = Save.Find("Viewport/Content").GetComponentsInChildren<mButton>(true);

        Left.gameObject.SetActive(false);
        Right.gameObject.SetActive(false);
        Save.gameObject.SetActive(false);
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
        Left.gameObject.SetActive(true);
        Right.gameObject.SetActive(true);
        Top.gameObject.SetActive(false);
        Save.gameObject.SetActive(false);
    }

    protected void Back()
    {
        PlayBtn.SetText("继续");
        Left.gameObject.SetActive(false);
        Right.gameObject.SetActive(false);
        Top.gameObject.SetActive(true);
        Save.gameObject.SetActive(false);
    }

    protected void SavePanel()
    {
        Left.gameObject.SetActive(false);
        Right.gameObject.SetActive(false);
        Top.gameObject.SetActive(false);
        Save.gameObject.SetActive(true);

        CreateSaveButton();
    }

    protected void LoadReturn()
    {
        Left.gameObject.SetActive(false);
        Right.gameObject.SetActive(false);
        Top.gameObject.SetActive(true);
        Save.gameObject.SetActive(false);
    }


    public void CreateSaveButton()
    {
        DirectoryInfo root = new DirectoryInfo(SaveMgr.Instance.SavePath);

        int need = root.GetFiles().Length - AllSave.Length;

        for (int i = 0; i < need; i++)
        {
            Instantiate(AllSave[0].gameObject, Save.transform.Find("Viewport/Content"));
        }

        AllSave = Save.transform.Find("Viewport/Content").GetComponentsInChildren<mButton>(true);
        for (int i = 0; i < AllSave.Length; i++)
        {
            AllSave[i].clickCallBack = null;
        }

        for (int i = 0; i < AllSave.Length; i++)
        {
            if (i < root.GetFiles().Length)
            {
                string name = root.GetFiles()[i].Name;
                AllSave[i].Init(root.GetFiles()[i].Name, () => {
                    //Debug.Log(root.GetFiles()[i].Name);
                    SaveMgr.Instance.LoadGame(name);
                        });
            }
            AllSave[i].gameObject.SetActive(i < root.GetFiles().Length);
        }

        
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

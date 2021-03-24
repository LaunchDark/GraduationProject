using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCanvas : MonoBehaviour
{
    protected static ChangeCanvas instance;
    public static ChangeCanvas Instance
    {
        get
        {
            if (instance == null)
            {
                instance = UITool.Instantiate("UI/ChangeCanvas").GetComponent<ChangeCanvas>();
            }
            return instance;
        }
    }

    public float distance = 0.1f;
    protected MaterialGroup MaterialsContent;
    public Instrument instrument;

    public Text Name;

    public List<Material> WallMaterials;
    public List<Material> TopMaterials;
    public List<Material> FloorMaterials;

    public void Init()
    {
        MaterialsContent = transform.Find("Materials/Viewport/Content").gameObject.AddComponent<MaterialGroup>();
        MaterialsContent.callFun += ChangeMaterial;
        HideUI();
    }


    private void Update()
    {
        transform.LookAt(transform.position + (transform.position - Valve.VR.InteractionSystem.Player.instance.transform.GetComponentInChildren<Camera>().transform.position).normalized * 1.0f);
        
        if (transform.localEulerAngles.x > 15 && transform.localEulerAngles.x < 180)
        {
            transform.localEulerAngles = new Vector3(15, transform.localEulerAngles.y, 0);
        }
        else if (transform.localRotation.x > 180 && transform.localEulerAngles.x < 345)
        {
            transform.localEulerAngles = new Vector3(345, transform.localEulerAngles.y, 0);
        }
        if (instrument)
        {
            if (!instrument.gameObject.activeSelf)
            {
                HideUI();
            }
            else if(instrument.mState == Instrument.State.drop || instrument.mState == Instrument.State.held || instrument.mState == Instrument.State.normal)
            {
                HideUI();
            }
        }
    }

    public void ShowUI(Vector3 pos, Instrument select)
    {
        if (!UIRoot.Instance.isPlaying) return;
        if (select.type == InstrumentEnum.墙)
        {
            if (WallMaterials.Count == 0) return;
            instrument = select;
            MaterialsContent.Init(WallMaterials.Count);
            transform.position = pos + (pos - Valve.VR.InteractionSystem.Player.instance.transform.GetComponentInChildren<Camera>().transform.position).normalized * distance;
            gameObject.SetActive(true);
        }
        else if (select.type == InstrumentEnum.地板)
        {
            if (FloorMaterials.Count == 0) return;
            instrument = select;
            MaterialsContent.Init(FloorMaterials.Count);
            transform.position = pos + (pos - Valve.VR.InteractionSystem.Player.instance.transform.GetComponentInChildren<Camera>().transform.position).normalized * distance;
            gameObject.SetActive(true);
        }
        else if (select.type == InstrumentEnum.天花板)
        {
            if (FloorMaterials.Count == 0) return;
            instrument = select;
            MaterialsContent.Init(TopMaterials.Count);
            transform.position = pos + (pos - Valve.VR.InteractionSystem.Player.instance.transform.GetComponentInChildren<Camera>().transform.position).normalized * distance;
            gameObject.SetActive(true);
        }
        //else if (select.type != InstrumentEnum.墙 && select.type != InstrumentEnum.地板 && select.type != InstrumentEnum.天花板)
        else
        {
            if (select.CanChange.Count == 0) return;
            instrument = select;
            MaterialsContent.Init(instrument.EachMaterials.Count / instrument.CanChange.Count);
            transform.position = pos + (pos - Valve.VR.InteractionSystem.Player.instance.transform.GetComponentInChildren<Camera>().transform.position).normalized * distance;
            gameObject.SetActive(true);
        }
        if (instrument != null)
        {
            Name.text = instrument.type.ToString();
        }
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }

    public void ChangeMaterial(int i)
    {
        if (instrument)
        {
            instrument.ChangeMaterial(i);
        }
    }

}

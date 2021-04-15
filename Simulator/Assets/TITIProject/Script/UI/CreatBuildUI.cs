using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;
using DG.Tweening;
using UnityEngine.Events;

public class CreatBuildUI : MonoBehaviour
{
    //public GameObject BuildInfoUI;

    public GameObject BuildUI;

    public GameObject BuildInfoUI;

    public GameObject selectBuildUI;

    public Text selectBuildName;

    private int BuildNum = 0;
    private int ColorNum = 0;

    protected List<mButton> mButtons;

    public Building UIBuilding;

    

    [SerializeField] protected mButton CreatBuildingBtn;
    [SerializeField] protected mButton CreatAllBulidingBtn;
    [SerializeField] protected mButton CreatBuild1Btn;
    [SerializeField] protected mButton CreatBuild2Btn;
    [SerializeField] protected mButton DestroyAllBuildingBtn;


    private void Start()
    {
        mButtons = new List<mButton>();
        CreatBuildingBtn.clickCallBack = NewBuild;
        CreatAllBulidingBtn.clickCallBack = CreatBuilding;
        CreatBuild1Btn.clickCallBack = CreatBuild1;
        CreatBuild2Btn.clickCallBack = CreatBuild2;
        DestroyAllBuildingBtn.clickCallBack = DestroyAllBuilding;
        selectBuildName.text = "";
    }

    protected void NewBuild()
    {
        //Debug.Log("Run");
        
        GameObject NewBuilding = (GameObject)Instantiate(BuildUI, this.transform);

        NewBuilding.name = "Building" + BuildNum;

        BuildNum++;

        NewBuilding.GetComponent<BuildUICtr>().UpdateMessages();

        NewBuilding.transform.parent = CreatBuildingBtn.transform.parent.parent;

        switch(ColorNum)
        {
            case 0:
                NewBuilding.GetComponent<BuildUICtr>().bg.color = Color.red;
                break;
            case 1:
                NewBuilding.GetComponent<BuildUICtr>().bg.color = Color.blue;
                break;
            case 2:
                NewBuilding.GetComponent<BuildUICtr>().bg.color = Color.green;
                break;
            case 3:
                NewBuilding.GetComponent<BuildUICtr>().bg.color = Color.yellow;
                break;
            case 4:
                NewBuilding.GetComponent<BuildUICtr>().bg.color = Color.white;
                break;
            case 5:
                NewBuilding.GetComponent<BuildUICtr>().bg.color = Color.gray;
                break;
        }
        ColorNum++;
        if(ColorNum>5)
        {
            ColorNum = 0;
        }
        mButtons.Add(NewBuilding.GetComponent<mButton>());
        UIBuilding = NewBuilding.GetComponent<BuildUICtr>().buildinfo;
        selectBuildUI = NewBuilding;
        OpenBuildInfoUI();
    }

   
    public void OpenBuildInfoUI()
    {
        BuildInfoUI.GetComponent<BuildInfoUI>().UIBuild = UIBuilding;
        BuildInfoUI.GetComponent<BuildInfoUI>().UpdateUIWalls();
        BuildInfoUI.GetComponent<BuildInfoUI>().UpdateInfo();
        BuildInfoUI.GetComponent<BuildInfoUI>().UpdateWallInfo();
        selectBuildName.text = selectBuildUI.name;
    }

    public void UpdateSelectBuilding()
    {
        selectBuildUI.GetComponent<BuildUICtr>().buildinfo = UIBuilding;
        selectBuildUI.GetComponent<BuildUICtr>().UpdateMessages();
    }

    public void CreatBuilding()
    {
        InstrumentMgr.Instance.DeleteSceneAllInstrument(false);
        CreatManager.Instance.DestroyAllBuilding();
        foreach (var building in mButtons)
        {
            //BuildingInfo.Instance.ClearLists();
            BuildingInfo.Instance.buildinfo.Add(building.gameObject.GetComponent<Building>());
            Destroy(building.gameObject);
        }
        mButtons.Clear();
        CreatManager.Instance.CreatAllBuilding();
        UIRoot.Instance.CreatDown();
        selectBuildName.text = "";
    }

    public void CreatBuild1()
    {
        InstrumentMgr.Instance.DeleteSceneAllInstrument(false);
        CreatManager.Instance.DestroyAllBuilding();
        //DestroyAllBuilding();
        BuildingInfo.Instance.Building1Info();
        CreatManager.Instance.CreatAllBuilding();
        UIRoot.Instance.CreatDown();
        selectBuildName.text = "";
    }

    public void CreatBuild2()
    {
        InstrumentMgr.Instance.DeleteSceneAllInstrument(false);
        CreatManager.Instance.DestroyAllBuilding();
        //DestroyAllBuilding();
        BuildingInfo.Instance.Building2Info();
        CreatManager.Instance.CreatAllBuilding();
        UIRoot.Instance.CreatDown();
        selectBuildName.text = "";
    }

    public void DestroyAllBuilding()
    {
        CreatManager.Instance.DestroyAllBuilding();
        foreach (var building in mButtons)
        {
            //BuildingInfo.Instance.ClearLists();
            //BuildingInfo.Instance.buildinfo.Add(building.gameObject.GetComponent<Building>());
            Destroy(building.gameObject);
        }
        mButtons.Clear();
        selectBuildName.text = "";
    }
}

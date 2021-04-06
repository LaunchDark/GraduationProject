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

    private int BuildNum = 0;
    private int ColorNum = 0;

    protected List<mButton> mButtons;

    public Building UIBuilding;

    

    [SerializeField] protected mButton CreatBuildingBtn;
    [SerializeField] protected mButton CreatAllBulidingBtn;
    [SerializeField] protected mButton CreatBuild1Btn;
    [SerializeField] protected mButton CreatBuild2Btn;


    private void Start()
    {
        mButtons = new List<mButton>();
        CreatBuildingBtn.clickCallBack = NewBuild;
        CreatAllBulidingBtn.clickCallBack = CreatBuilding;
        CreatBuild1Btn.clickCallBack = CreatBuild1;
        CreatBuild2Btn.clickCallBack = CreatBuild2;
    }

    protected void NewBuild()
    {
        Debug.Log("Run");
        
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
    }

   
    public void OpenBuildInfoUI()
    {
        BuildInfoUI.GetComponent<BuildInfoUI>().UIBuild = UIBuilding;
        BuildInfoUI.GetComponent<BuildInfoUI>().UpdateInfo();
        BuildInfoUI.GetComponent<BuildInfoUI>().UpdateWallInfo();
    }

    public void UpdateSelectBuilding()
    {
        selectBuildUI.GetComponent<BuildUICtr>().buildinfo = UIBuilding;
        selectBuildUI.GetComponent<BuildUICtr>().UpdateMessages();
    }

    public void CreatBuilding()
    {
        foreach(var building in mButtons)
        {
            BuildingInfo.Instance.buildinfo.Add(building.gameObject.GetComponent<Building>());
            CreatManager.Instance.CreatAllBuilding();
        }
        UIRoot.Instance.CreatDown();
            
    }

    public void CreatBuild1()
    {
        BuildingInfo.Instance.Building1Info();
        CreatManager.Instance.CreatAllBuilding();
        UIRoot.Instance.CreatDown();
    }

    public void CreatBuild2()
    {
        BuildingInfo.Instance.Building2Info();
        CreatManager.Instance.CreatAllBuilding();
        UIRoot.Instance.CreatDown();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{
    protected GameObject mDoors;
    protected List<GameObject> AllDoor;
    protected GameObject mWindows; 
    protected List<GameObject> AllWindow;


    private void Awake()
    {
        var packsackMgr = PacksackMgr.Instance;//初始化背包管理器
        var instrumentMgr = InstrumentMgr.Instance;//初始化家具管理器   
        var saveMgr = SaveMgr.Instance;

        UITool.Instantiate("MyPlayer/Player");

        instrumentMgr.CreateInstrument(InstrumentEnum.音响);
        instrumentMgr.DeleteSceneAllInstrument();
    }

    /// <summary>
    /// 初始化建筑、人物
    /// </summary>
    public void Init()
    {

        mDoors = new GameObject("门框");
        mWindows = new GameObject("窗台");

        //生成门框
        AllDoor = new List<GameObject>();
        if (BuildingInfo.Instance.Doors.Count > 0)
        {
            for (int i = 0; i < BuildingInfo.Instance.Doors.Count; i++)
            {
                AllDoor.Add(InstrumentMgr.Instance.CreateInstrument(InstrumentEnum.门框));
                AllDoor[i].transform.parent = mDoors.transform;
                AllDoor[i].transform.position = BuildingInfo.Instance.Doors[i].position;
                AllDoor[i].transform.eulerAngles = BuildingInfo.Instance.Doors[i].eulerAngles;
            }
        }

        //生成窗台
        AllWindow = new List<GameObject>();
        if (BuildingInfo.Instance.Windows.Count > 0)
        {
            for (int i = 0; i < BuildingInfo.Instance.Windows.Count; i++)
            {
                AllWindow.Add(InstrumentMgr.Instance.CreateInstrument(InstrumentEnum.窗台));
                AllWindow[i].transform.parent = mWindows.transform;
                AllWindow[i].transform.position = BuildingInfo.Instance.Windows[i].position;
                AllWindow[i].transform.eulerAngles = BuildingInfo.Instance.Windows[i].eulerAngles;
            }
        }

        //更改墙、地板、天花板层级
        if (BuildingInfo.Instance.Walls.Count > 0)
        {
            foreach (var wall in BuildingInfo.Instance.Walls)
            {
                foreach (var item in wall.GetComponentsInChildren<Transform>())
                {
                    //如果是空物体，不添加脚本
                    if (item.GetComponent<BoxCollider>())
                    {
                        item.gameObject.AddComponent<WallMaterial>();
                    }
                    item.gameObject.layer = LayerMask.NameToLayer("Wall");
                }
            }
        }
        if (BuildingInfo.Instance.Floors.Count > 0)
        {
            foreach (var floor in BuildingInfo.Instance.Floors)
            {
                foreach (var item in floor.GetComponentsInChildren<Transform>())
                {
                    //如果是空物体，不添加脚本
                    if (item.GetComponent<BoxCollider>())
                    {
                        item.gameObject.AddComponent<FloorMaterial>();
                    }
                    item.gameObject.layer = LayerMask.NameToLayer("Ground");
                }
            }
        }
        if (BuildingInfo.Instance.Tops.Count > 0)
        {
            foreach (var top in BuildingInfo.Instance.Tops)
            {
                foreach (var item in top.GetComponentsInChildren<Transform>())
                {
                    //如果是空物体，不添加脚本
                    if (item.GetComponent<BoxCollider>())
                    {
                        item.gameObject.AddComponent<TopMaterial>();
                    }
                    item.gameObject.layer = LayerMask.NameToLayer("TopWall");
                }
            }
        }
    }

    /// <summary>
    /// 测试
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            SaveMgr.Instance.SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            //SaveMgr.Instance.LoadGame();
        }
    }

}

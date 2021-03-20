using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{
    public List<Transform> trans;

    protected GameObject mDoors;
    protected List<GameObject> AllDoor;
    protected GameObject mWindows; 
    protected List<GameObject> AllWindow;


    private void Awake()
    {
        var packsackMgr = PacksackMgr.Instance;//初始化背包系统
        var instrumentMgr = InstrumentMgr.Instance;//初始化仪器系统

        UITool.Instantiate("MyPlayer/Player");

        mDoors = new GameObject("门框");
        mWindows = new GameObject("窗台");

        AllDoor = new List<GameObject>();
        //for (int i = 0; i < trans.Count; i++)
        //{
        //    AllDoor.Add(instrumentMgr.CreateInstrument(InstrumentEnum.门框));
        //    AllDoor[i].transform.parent = mDoors.transform;
        //    AllDoor[i].transform.position = trans[i].position;
        //    AllDoor[i].transform.eulerAngles = trans[i].eulerAngles;
        //}

        AllWindow = new List<GameObject>();
        for (int i = 0; i < trans.Count; i++)
        {
            AllWindow.Add(instrumentMgr.CreateInstrument(InstrumentEnum.窗台));
            AllWindow[i].transform.parent = mWindows.transform;
            AllWindow[i].transform.position = trans[i].position;
            AllWindow[i].transform.eulerAngles = trans[i].eulerAngles;
        }
        
    }

}

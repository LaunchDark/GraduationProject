using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatManager : MonoBehaviour
{
    static CreatManager instance;

    public static CreatManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("CreatManager").AddComponent<CreatManager>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    public List<Building> Buildings = new List<Building>();
    void Start()
    {
        //gameObject.AddComponent<BuildingInfo>();
        //BuildingInfo.Instance.Init();

        //BuildingInfo.Instance.Building1Info();
        //CreatAllBuilding();

    }

        //if(Input.GetKeyDown(KeyCode.H))
        //{
        //    BuildingManager.Instance.saveBuilding();
        //    Debug.Log("Save");
        //}
        //if(Input.GetKeyDown(KeyCode.J))
        //{
        //    DestroyAllBuilding();
        //}
    

    public void CreatAllBuilding()
    {
        int num = 0;
        foreach(var build in BuildingInfo.Instance.buildinfo)
        {
            build.CreatBuilding(num);
            num++;
        }
        Scene.instance.Init();
    }

    /// <summary>
    /// 删除所有建筑
    /// </summary>
    public void DestroyAllBuilding()
    {
        foreach(var wall in BuildingInfo.Instance.Walls)
        {
            GameObject.Destroy(wall.gameObject);
        }
        foreach (var _wall in BuildingInfo.Instance._Walls)
        {
            GameObject.Destroy(_wall.gameObject);
        }
        foreach (var top in BuildingInfo.Instance.Tops)
        {
            GameObject.Destroy(top.gameObject);
        }
        foreach (var floor in BuildingInfo.Instance.Floors)
        {
            GameObject.Destroy(floor.gameObject);
        }

        foreach (var Window in BuildingInfo.Instance.Windows)
        {
            if (Window.GetComponentInChildren<Instrument>())
            {
                break;
            }
            GameObject.Destroy(Window.gameObject);
        }

        foreach (var door in BuildingInfo.Instance.Doors)
        {
            if (door.GetComponentInChildren<Instrument>())
            {
                break;
            }
            GameObject.Destroy(door.gameObject);
        }


        BuildingInfo.Instance.ClearLists();
    }

}

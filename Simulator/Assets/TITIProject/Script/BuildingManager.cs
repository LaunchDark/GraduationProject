using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class BuildingManager : MonoBehaviour
{
    static BuildingManager instance;
    public static BuildingManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("BuildSaveMgr").AddComponent<BuildingManager>();
            }
            return instance;
        }
    }
    public string UserPath;
    public string BuildingPath;

    private void Awake()
    {
        //Debug.Log(Environment.CurrentDirectory);
        UserPath = Environment.CurrentDirectory + $"/UserData";
        BuildingPath = UserPath + $"/SaveData/Building";
        if (!Directory.Exists(UserPath))
        {
            Directory.CreateDirectory(UserPath);
        }
        if (!Directory.Exists(BuildingPath))
        {
            Directory.CreateDirectory(BuildingPath);
        }

    }

    public void saveBuilding()
    {
        BuildingSave save = CreatBuildingSave();

        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(BuildingPath + "/" + $"{System.DateTime.Now:yyyy-MM-dd_HH-mm-ss}" + ".save");

        bf.Serialize(file, save);
        file.Close();

        Debug.Log("存档");
    }

    public void loadBuilding(string name)
    {
        Debug.Log(BuildingPath + "/" + name);


        if(File.Exists(BuildingPath + "/" + name))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(BuildingPath + "/" + name, FileMode.Open);

            BuildingSave save = (BuildingSave)bf.Deserialize(file);

            file.Close();

            foreach(var build in save.GetSave())
            {
                BuildingInfo.Instance.buildinfo.Add(build);
            }
        }
        else
        {
            Debug.Log("No saved!");
        }
        
    }

    public BuildingSave CreatBuildingSave()
    {
        BuildingSave save = new BuildingSave();

        foreach(var build in BuildingInfo.Instance.buildinfo)
        {
            save.SaveBuilding(build);
        }

        return save;
    }

}

[System.Serializable]
public class BuildingSave
{
    protected List<Building> Buildings = new List<Building>();

    public void SaveBuilding(Building build)
    {
        Buildings.Add(build);
    }

    public List<Building> GetSave()
    {
        return Buildings;
    }

}

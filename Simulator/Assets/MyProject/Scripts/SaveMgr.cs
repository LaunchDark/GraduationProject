using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

/// <summary>
/// 存档
/// </summary>
public class SaveMgr : MonoBehaviour
{
    static SaveMgr instance;
    public static SaveMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("SaveMgr").AddComponent<SaveMgr>();
            }
            return instance;
        }
    }
    public string UserPath;
    public string ImagePath;
    public string SavePath;

    private void Awake()
    {
        //Debug.Log(Environment.CurrentDirectory);
        UserPath = Environment.CurrentDirectory + $"/UserData";
        ImagePath = UserPath + $"/Image";
        SavePath = UserPath + $"/SaveData";
        if (!Directory.Exists(UserPath))
        {
            Directory.CreateDirectory(UserPath);
        }
        if(!Directory.Exists(ImagePath))
        {
            Directory.CreateDirectory(ImagePath);
        }
        if(!Directory.Exists(SavePath))
        {
            Directory.CreateDirectory(SavePath);
        }

    }

    /// <summary>
    /// 存档
    /// </summary>
    public void SaveGame()
    {
        // 1 创建保存文件
        Save save = CreateSaveGameObject();

        // 2 写入文件
        BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(SavePath + "/gamesave.save");
        FileStream file = File.Create(SavePath + "/" + $"{System.DateTime.Now:yyyy-MM-dd_HH-mm-ss}" + ".save");
        bf.Serialize(file, save);
        file.Close();
        TipsCanvas.Instance.ShowTips("保存成功");
        Debug.Log("存档");
    }

    /// <summary>
    /// 创建存档文件
    /// </summary>
    /// <returns></returns>
    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        //读取场景数据
        foreach (KeyValuePair<InstrumentEnum,List<GameObject>> item in InstrumentMgr.Instance.GetInstrumentGameObjectDic())
        {
            foreach (var obj in item.Value)
            {
                save.AddInstrument(obj);
            }
        }
        //Debug.Log(save.GetAllInstrument().Count);
        //保存建筑需要按顺序
        foreach (var wall in BuildingInfo.Instance.Walls)
        {
            save.AddWall(wall);
        }
        foreach (var wall in BuildingInfo.Instance.Tops)
        {
            save.AddWall(wall);
        }
        foreach (var wall in BuildingInfo.Instance.Floors)
        {
            save.AddWall(wall);
        }

        return save;
    }

    /// <summary>
    /// 加载存档
    /// </summary>
    /// <param name="name">存档名称</param>
    public void LoadGame(string name)
    {
        Debug.Log(SavePath + "/" + name);
        // 1 查找存档文件
        //if (File.Exists(SavePath + "/gamesave.save"))
        //if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        if (File.Exists(SavePath + "/" + name))
        {
            // 2 打开文件
            BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Open(SavePath + "/gamesave.save", FileMode.Open);
            FileStream file = File.Open(SavePath + "/" + name, FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            // 3 将存档数据写到场景中
            InstrumentMgr.Instance.DeleteSceneAllInstrument(false);

            List<Instrument> hasSubInstrument = new List<Instrument>();
            List<Instrument> SubInstruments = new List<Instrument>();

            foreach (var item in save.GetAllInstrument())
            {
                if (item.active)
                {
                    GameObject obj = InstrumentMgr.Instance.CreateInstrument(item.instrumentEnum);
                    obj.transform.position = item.vector9.GetPos();
                    obj.transform.eulerAngles = item.vector9.GetRot();
                    obj.transform.localScale = item.vector9.GetSca();
                    obj.GetComponent<Instrument>().SetState((Instrument.State)Enum.ToObject(typeof(Instrument.State), item.state));
                    obj.GetComponent<Instrument>().ChangeMaterial(item.Mateial);
                    if (item.hasSubInstrument)
                    {
                        hasSubInstrument.Add(obj.GetComponent<Instrument>());
                    }
                    else if (item.CurAbsorb != InstrumentEnum.None)
                    {
                        SubInstruments.Add(obj.GetComponent<Instrument>());
                    }
                    obj.SetActive(true);
                }
            }

            //窗台、门框等恢复
            bool find = false;
            for (int i = 0; i < SubInstruments.Count; i++)
            {
                //Debug.Log(find);
                if (find)
                {
                    i = 0;
                    find = false;
                }
                for (int j = 0; j < hasSubInstrument.Count; j++)
                {
                    if (SubInstruments[i].GetAdsorbTypeList().Contains(hasSubInstrument[j].type))
                    {
                        SubInstruments[i].curAdsorbInstrument = hasSubInstrument[j];
                        SubInstruments[i].AdsorbCallBack();
                        hasSubInstrument[j].adsorbCollider.enabled = false;
                        hasSubInstrument[j].subInstrument = SubInstruments[i];
                        hasSubInstrument.Remove(hasSubInstrument[j]);
                        SubInstruments.Remove(SubInstruments[i]);

                        find = true;
                        //Debug.Log(string.Format("匹配{0},{1}",i,j));
                        i--;
                        break;
                    }
                }
            }

            //读取建筑需要按顺序
            List<Instrument> building = new List<Instrument>();
            foreach (var wall in BuildingInfo.Instance.Walls)
            {
                foreach (var item in wall.GetComponentsInChildren<Transform>())
                {
                    //如果是空物体，不添加脚本
                    if (item.GetComponent<BoxCollider>())
                    {
                        building.Add(item.gameObject.GetComponent<Instrument>());                        
                    }
                }
            }
            foreach (var top in BuildingInfo.Instance.Tops)
            {
                foreach (var item in top.GetComponentsInChildren<Transform>())
                {
                    //如果是空物体，不添加脚本
                    if (item.GetComponent<BoxCollider>())
                    {
                        building.Add(item.gameObject.GetComponent<Instrument>());
                    }
                }
            }
            foreach (var floor in BuildingInfo.Instance.Floors)
            {
                foreach (var item in floor.GetComponentsInChildren<Transform>())
                {
                    //如果是空物体，不添加脚本
                    if (item.GetComponent<BoxCollider>())
                    {
                        building.Add(item.gameObject.GetComponent<Instrument>());
                    }
                }
            }
            //Debug.Log(building.Count);
            //Debug.Log(save.GetWallMaterial().Count);
            for (int i = 0; i < building.Count; i++)
            {
                //Debug.Log(save.GetWallMaterial()[i]);
                building[i].ChangeMaterial(save.GetWallMaterial()[i]);
            }
            //Debug.Log(InstrumentMgr.Instance.GetInstrumentGameObjectDic().Count);
            TipsCanvas.Instance.ShowTips("读取完成");
            Debug.Log("读档");
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

}

/// <summary>
/// 保存文件类型
/// </summary>
[System.Serializable]
public class Save
{
    protected List<InstrumentSave> AllInstrument = new List<InstrumentSave>();
    protected List<int> WallMaterial = new List<int>();

    public void AddInstrument(GameObject obj)
    {
        InstrumentSave item = new InstrumentSave();
        item.instrumentEnum = obj.GetComponent<Instrument>().type;
        item.vector9.SaveTrans(obj.transform);
        item.active = obj.activeSelf;
        item.state = (int)obj.GetComponent<Instrument>().mState;
        item.Mateial = obj.GetComponent<Instrument>().CurMaterial;
        item.hasSubInstrument = obj.GetComponent<Instrument>().subInstrument != null;
        if(obj.GetComponent<Instrument>().curAdsorbInstrument)
            item.CurAbsorb = obj.GetComponent<Instrument>().curAdsorbInstrument.type;

        AllInstrument.Add(item);
    }

    public void AddWall(Transform list)
    {
        foreach (var item in list.GetComponentsInChildren<Transform>())
        {
            //如果是空物体，不添加脚本
            if (item.GetComponent<BoxCollider>())
            {
                WallMaterial.Add(item.gameObject.GetComponent<Instrument>().CurMaterial);
            }
        }
    }
    
    public List<InstrumentSave> GetAllInstrument()
    {
        return AllInstrument;
    }

    public List<int> GetWallMaterial()
    {
        return WallMaterial;
    }
}


/// <summary>
/// 家具存档类
/// </summary>
[System.Serializable]
public class InstrumentSave
{
    /// <summary>
    /// 家具类型
    /// </summary>
    public InstrumentEnum instrumentEnum = InstrumentEnum.None;
    /// <summary>
    /// transform属性
    /// </summary>
    public Vector9 vector9 = new Vector9();
    /// <summary>
    /// 是否显示
    /// </summary>
    public bool active = false;
    /// <summary>
    /// 当前状态
    /// </summary>
    public int state = 0;
    /// <summary>
    /// 当前材质
    /// </summary>
    public int Mateial = 0;
    /// <summary>
    /// 是否有吸附子物体
    /// </summary>
    public bool hasSubInstrument = false;
    /// <summary>
    /// 当前吸附物体
    /// </summary>
    public InstrumentEnum CurAbsorb = InstrumentEnum.None;

    public InstrumentSave()
    {

    }
}

[System.Serializable]
public class Vector9
{
    protected float posX, posY, posZ, rotX, rotY, rotZ, scaX, scaY, scaZ;

    public Vector9()
    {

    }
    
    public void SaveTrans(Transform trans)
    {
        posX = trans.position.x;
        posY = trans.position.y;
        posZ = trans.position.z;

        rotX = trans.eulerAngles.x;
        rotY = trans.eulerAngles.y;
        rotZ = trans.eulerAngles.z;

        scaX = trans.localScale.x;
        scaY = trans.localScale.y;
        scaZ = trans.localScale.z;
    }

    public Vector3 GetPos()
    {
        return new Vector3(posX, posY, posZ);
    }
    public Vector3 GetRot()
    {
        return new Vector3(rotX, rotY, rotZ);
    }
    public Vector3 GetSca()
    {
        return new Vector3(scaX, scaY, scaZ);
    }
}
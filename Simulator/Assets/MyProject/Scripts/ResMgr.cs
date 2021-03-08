using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//资源加载器
public class ResMgr : MonoBehaviour
{
    static ResMgr instance;
    public static ResMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("ResMgr").AddComponent<ResMgr>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    Dictionary<string, Object> objDic = new Dictionary<string, Object>();

    public Object LoadByCore(string path)
    {
        //return Load("Core/" + path);
        return Load(path);
    }

    public Object Load(string path)
    {
        if (objDic.ContainsKey(path))
            return objDic[path];
        Object o = Resources.Load(path);
        objDic.Add(path, o);
        return o;
    }

    public void GetFiles(string path)
    {
        string imgtype = "*.BMP|*.JPG|*.GIF|*.PNG";
        string[] ImageType = imgtype.Split('|');

        //获取指定路径下面的所有资源文件  
        if (Directory.Exists(path))
        {
            DirectoryInfo direction = new DirectoryInfo(path);
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);   //   获取所有文件
            Debug.Log("该文件夹文件总数为" + files.Length);

            for (int i = 0; i < files.Length; i++)       //获取所有文件名称
            {
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }
                Debug.Log("Name:" + files[i].Name);
                Debug.Log("FullName:" + files[i].FullName);
                //Debug.Log("DirectoryName:" + files[i].DirectoryName);
            }

            //for (int i = 0; i < ImageType.Length; i++)
            //{
            //    //获取d盘中a文件夹下所有的图片路径  
            //    string[] dirs = Directory.GetFiles(path, ImageType[i]);
            //    for (int j = 0; j < dirs.Length; j++)
            //    {
            //        if (files[j].Name.EndsWith(".meta"))
            //        {
            //            continue;
            //        }
            //        Debug.Log("文件夹图片格式的Name:" + files[j].Name);
            //    }

            //}
        }
    }

    /// <summary>
    /// 获取文件夹下所有相关文件路径
    /// </summary>
    /// <param name="path">文件夹路径</param>
    /// <param name="Type">类型（*.cs/*.txt）</param>
    /// <returns></returns>
    public List<string> GetFiles(string path, string Type)
    {
        List<string> filesNames = new List<string>();
        string[] tempValue = Directory.GetFiles(path, Type);
        foreach (var item in tempValue)
        {
            filesNames.Add(item);
        }
        string[] tempPaths = GetFolderPath(path);
        if (tempPaths.Length > 0)
        {
            for (int i = 0; i < tempPaths.Length; i++)
            {
                List<string> tempFilesNames = GetFiles(tempPaths[i], Type);
                foreach (var item in tempFilesNames)
                {
                    filesNames.Add(item);
                }
            }
        }
        //foreach (var item in filesNames)
        //{
        //    Debug.Log(item);
        //}
        return filesNames;
    }
    /// <summary>
    /// 获取文件夹下所有文件夹路径
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public string[] GetFolderPath(string path)
    {
        DirectoryInfo root = new DirectoryInfo(path);
        DirectoryInfo[] tempValue = root.GetDirectories();
        string[] tempPaths = new string[tempValue.Length];

        for (int i = 0; i < tempValue.Length; i++)
        {
            tempPaths[i] = path + @"\" + tempValue[i].Name;
        }
        return tempPaths;
    }

}

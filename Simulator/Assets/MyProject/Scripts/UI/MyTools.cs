using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MyTools
{
    /// <summary>
    /// 屏幕截图
    /// </summary>
    /// <param name="IsEnableAlpha">是否开启透明通道</param>
    public static void ScreenShot(bool IsEnableAlpha = false)
    {
        Camera m_Camera = GameObject.Find("VRCamera").GetComponent<Camera>();
        if (m_Camera == null)
        {
            Debug.LogError("<color=red>" + "没有摄像机" + "</color>");
            return;
        }

        string filePath = Application.persistentDataPath;
        if (string.IsNullOrEmpty(filePath))
        {
            Debug.LogError("<color=red>" + "没有截图保存位置" + "</color>");
            return;
        }

        CameraClearFlags m_CameraClearFlags;
        m_CameraClearFlags = m_Camera.clearFlags;
        if (IsEnableAlpha)
        {
            m_Camera.clearFlags = CameraClearFlags.Depth;
        }
        //int resolutionX = (int)Handles.GetMainGameViewSize().x;
        //int resolutionY = (int)Handles.GetMainGameViewSize().y;
        int resolutionX = Screen.width;
        int resolutionY = Screen.height;

        RenderTexture rt = new RenderTexture(resolutionX, resolutionY, 24);
        m_Camera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resolutionX, resolutionY, TextureFormat.ARGB32, false);
        m_Camera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resolutionX, resolutionY), 0, 0);
        m_Camera.targetTexture = null;
        RenderTexture.active = null;
        m_Camera.clearFlags = m_CameraClearFlags;
        //Destroy(rt);
        byte[] bytes = screenShot.EncodeToPNG();
        string fileName = filePath + "/" + $"{System.DateTime.Now:yyyy-MM-dd_HH-mm-ss}" + ".png";
        File.WriteAllBytes(fileName, bytes);
        Debug.Log("截图成功：\n" + filePath);
    }


    ////全屏截图
    ////存储路径
    //private string Path_save;
    ////读取路径
    //private string Path_read;
    //private string filepath;
    //private string destination;
    //void Start()
    //{
    //    filepath = Application.persistentDataPath + "/test.txt";
    //}


    //public static void getTexture2d()
    //{
    //    //隐藏UI

    //    //截图操作
    //    Texture2D t = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

    //    //显示UI

     
    //    t.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, true);
    //    byte[] bytes = t.EncodeToPNG();
    //    t.Compress(true);
    //    t.Apply();
    //    //img.texture = t;
    //    //t就是截到的图片我们可以在这里上传到服务器
    //    //下面是开始保存
    //    //获取系统时间
    //    System.DateTime now = new System.DateTime();
    //    now = System.DateTime.Now;
    //    string filename = string.Format("p_w_picpath{0}{1}{2}{3}.png", now.Day, now.Hour, now.Minute, now.Second);
    //    //记录每一个截图名字
    //    StreamWriter sw;
    //    FileInfo ft = new FileInfo(filepath);
    //    //la[1].text = filename;
    //    if (!ft.Exists)
    //    {
    //        sw = ft.CreateText();
    //    }
    //    else
    //    {
    //        sw = ft.AppendText();
    //    }
    //    sw.WriteLine(filename);
    //    sw.Close();
    //    sw.Dispose();
    //    //应用平台判断，路径选择
    //    if (Application.platform == RuntimePlatform.Android)
    //    {
    //        string origin = Path_save;
    //        //保存在Android相册中，如果是PC就改成Application .dataPath 的路径
    //        destination = "/sdcard/DCIM/Camera";
    //        if (!Directory.Exists(destination))
    //        {
    //            Directory.CreateDirectory(destination);
    //        }
    //        destination = destination + "/" + filename;
    //        Path_save = destination;

    //    }
    //    //保存文件
    //    File.WriteAllBytes(Path_save, bytes);
    //}

}

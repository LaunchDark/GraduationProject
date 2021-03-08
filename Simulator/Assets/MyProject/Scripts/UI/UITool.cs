using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITool
{
    private static Material _uiGrayMaterial;
    private static Material uiGrayMaterial
    {
        get
        {
            if (_uiGrayMaterial == null)
            {
                Shader shader = Shader.Find("Custom/UI-Default-Gray");
                _uiGrayMaterial = new Material(shader);
            }
            return _uiGrayMaterial;
        }
    }
    /// <summary>
    /// Image组件置灰设置
    /// </summary>
    /// <param name="img"></param>
    /// <param name="b"></param>
    public static void SetUIGray(Image img, bool b)
    {
        if (b)
            img.material = uiGrayMaterial;
        else
            img.material = null;
    }

    public static GameObject Instantiate(string path, GameObject parent = null)
    {
        GameObject go =(GameObject) GameObject.Instantiate(ResMgr.Instance.Load(path));
        if(parent) go.transform.SetParent(parent.transform,false);
        return go;
    }

    public static GameObject Instantiate(UnityEngine.Object obj, GameObject parent = null)
    {
        GameObject go = (GameObject)GameObject.Instantiate(obj);
        if (parent) go.transform.SetParent(parent.transform, false);
        return go;
    }

    /// <summary>
    /// 设置锚点锚到本身大小的四个点
    /// </summary>
    /// <param name="rect"></param>
    public static void AnchorsToCorners(RectTransform rect)
    {
        if (rect == null) return;
        RectTransform pt = rect.parent as RectTransform;
        if (rect == null || pt == null) return;
        Vector2 newAnchorsMin = new Vector2(rect.anchorMin.x + rect.offsetMin.x / pt.rect.width,rect.anchorMin.y + rect.offsetMin.y / pt.rect.height);
        Vector2 newAnchorsMax = new Vector2(rect.anchorMax.x + rect.offsetMax.x / pt.rect.width,rect.anchorMax.y + rect.offsetMax.y / pt.rect.height);
        rect.anchorMin = newAnchorsMin;
        rect.anchorMax = newAnchorsMax;
        rect.offsetMin = rect.offsetMax = new Vector2(0, 0);
    }

    public static Transform TransformFindName(Transform go,string str)
    {
        Transform[] tfs = go.GetComponentsInChildren<Transform>();
        for (int i = 0; i < tfs.Length; i++)
        {
            if(tfs[i].name == str)
            {
                return tfs[i];
            }
        }
        return null;
    }

    //将秒数转化为时分秒
    public static string SecToHms(int seconds)
    {
        TimeSpan ts = new TimeSpan(0, 0, seconds);
        string str = "";
        if (ts.Hours > 0)
        {
            str = String.Format("{0}", ts.Hours) + ":" + String.Format("{0:00}", ts.Minutes) + ":" + String.Format("{0:00}", ts.Seconds);
        }
        if (ts.Hours == 0 && ts.Minutes > 0)
        {
            str = "0:" + String.Format("{0:00}", ts.Minutes) + ":" + String.Format("{0:00}", ts.Seconds);
        }
        if (ts.Hours == 0 && ts.Minutes == 0)
        {
            str = "0:00:" + String.Format("{0:00}", ts.Seconds);
        }
        return str;
    }

}

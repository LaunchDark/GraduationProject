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
    /// <summary>
    /// 当前鼠标射线碰撞的UI
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public static bool IsRaycastUI(GameObject go)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        EventSystem.current.RaycastAll(pointerEventData, results);
        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject == go)
                return true;
        }
        return false;
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

    //public static void SetSkybox(string path)
    //{
    //    RenderSettings.skybox = (Material)ResMgr.Instance.Load(path);
    //    DynamicGI.UpdateEnvironment();
    //}

    //public static void SetAmbientLight(float r, float g, float b)
    //{
    //    Color color = new Color(r, g, b);

    //    RenderSettings.ambientLight = color;
    //}

    //环境光亮度通过代码改不了
    //public static void SetAmbientIntensity(float intensity)
    //{
    //    RenderSettings.ambientIntensity = intensity;
    //}

    public static void SetCursor(bool b)
    {
        Cursor.visible = b;
        Cursor.lockState = b?CursorLockMode.None:CursorLockMode.Locked;
    }

    /// <summary>
    /// true为直接退出,false为弹出退出提示框
    /// </summary>
    /// <param name="isExit"></param>
//    public static void ExitAPP(bool isExit)
//    {
//        if(isExit)
//            UIRoot.Instance.SetExitAppCallBack(null);
//        Application.Quit();
//#if UNITY_EDITOR
//        UnityEditor.EditorApplication.isPlaying = false;
//#endif
//    }

    public static void ExitAppCallBack()
    {
        //MessageBoxData data = new MessageBoxData();
        //data.content = "是否退出程序";
        //data.single = false;
        //data.leftBtnCallBack = () =>
        //{
        //    UITool.ExitAPP(true);
        //};
        //UIMgr.Instance.ShowMessageBox(data);
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


    /// <summary>
    /// 光标是否在输入框内
    /// </summary>
    /// <returns></returns>
    public static bool IsFocusOnInputText()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
            return false;
        if (EventSystem.current.currentSelectedGameObject.GetComponent<UnityEngine.UI.InputField>() != null)
        {
            return true;
        }
           
        return false;
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

    //public static Vector3 WorldToUI(Camera camera, GameObject world)
    //{
    //    return WorldToUI(camera, world.transform.position);
    //}

    //public static Vector3 WorldToUI(Camera camera, Vector3 position)
    //{
    //    CanvasScaler scaler = UIRoot.Instance.canvasScaler;
    //    float resolutionX = scaler.referenceResolution.x;
    //    float resolutionY = scaler.referenceResolution.y;
    //    Vector3 viewportPos = camera.WorldToViewportPoint(position);
    //    Vector3 uiPos = new Vector3(viewportPos.x * resolutionX - resolutionX * 0.5f,
    //        viewportPos.y * resolutionY - resolutionY * 0.5f, 0);
    //    return uiPos;
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.U2D;

public class UIMgr : MonoBehaviour
{
    private static UIMgr instance;
    public static UIMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("UIMgr").AddComponent<UIMgr>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    private Dictionary<string, GameObject> panelDic = new Dictionary<string, GameObject>();
    private List<GameObject> dialogList = new List<GameObject>();

    private SpriteAtlas coreAtlas;

    //private List<MessageBox> messageBoxList = new List<MessageBox>();
    //private HttpMask httpMask;

    public GameObject ShowPanel(string path, float maskAlpha = 0.5f, bool isFromAb = false)
    {
        GameObject go = null;
        if (panelDic.ContainsKey(path))
            go = panelDic[path];
        else
        {
            if (!isFromAb)
            {
                go = (GameObject)GameObject.Instantiate(ResMgr.Instance.Load(path));
            }
            else
                go = (GameObject)GameObject.Instantiate(ResMgr.Instance.LoadByResPackage(path));
            panelDic.Add(path, go);
        }
        ShowPanel(go, maskAlpha);
        return go;
    }

    private void ShowPanel(GameObject go, float maskAlpha)
    {
        //go.transform.SetParent(UIRoot.Instance.panel, false);
        //UIRoot.Instance.panelMask.transform.SetAsLastSibling();
        go.transform.SetAsLastSibling();
        go.SetActive(true);
        //UIRoot.Instance.panelMask.gameObject.SetActive(true);
        //UIRoot.Instance.panelMask.color = Color.black * maskAlpha;
        Messenger.Broadcast(GlobalEvent.Enter_UI);
    }

    public void HidePanel(string path)
    {
        if (panelDic.ContainsKey(path))
            HidePanel(panelDic[path]);
    }

    public void DestroyPanel(string path)
    {
        if (panelDic.ContainsKey(path))
        {
            HidePanel(panelDic[path]);
            GameObject.Destroy(panelDic[path]);
            panelDic.Remove(path);
        }
    }

    public void DestroyAllPanel()
    {
        List<string> keys = new List<string>();
        foreach (var item in panelDic)
            keys.Add(item.Key);
        for (int i = 0; i < keys.Count; i++)
            DestroyPanel(keys[i]);
    }

    public void HidePanel(GameObject go)
    {
        if (!go.activeSelf)
            return;
        go.SetActive(false);
        go.transform.SetAsFirstSibling();
        //UIRoot.Instance.panelMask.transform.SetAsLastSibling();
        //UIRoot.Instance.panelMask.gameObject.SetActive(false);
        //int index = UIRoot.Instance.panelMask.transform.GetSiblingIndex();
        //if (UIRoot.Instance.panel.GetChild(index - 1).gameObject.activeSelf)
        //{
        //    UIRoot.Instance.panel.GetChild(index - 1).SetAsLastSibling();
        //    UIRoot.Instance.panelMask.gameObject.SetActive(true);
        //    return;
        //}
        Messenger.Broadcast(GlobalEvent.Exit_UI);
    }

    public void HideAllPanel()
    {
        foreach (var item in panelDic)
        {
            HidePanel(item.Value);
        }
    }

    public void ShowDialog(GameObject go, float maskAlpha = 0.5f)
    {
        //go.transform.SetParent(UIRoot.Instance.dialog, false);
        //UIRoot.Instance.dialogMask.transform.SetAsLastSibling();
        go.transform.SetAsLastSibling();
        go.SetActive(true);
        //UIRoot.Instance.dialogMask.gameObject.SetActive(true);
        //UIRoot.Instance.dialogMask.color = Color.black * maskAlpha;
        Messenger.Broadcast(GlobalEvent.Enter_UI);
        if (!dialogList.Contains(go))
            dialogList.Add(go);
    }

    public void HideDialog(GameObject go)
    {
        if (!go.activeSelf)
            return;
        go.SetActive(false);
        //go.transform.SetAsFirstSibling();
        //UIRoot.Instance.dialogMask.transform.SetAsLastSibling();
        //UIRoot.Instance.dialogMask.gameObject.SetActive(false);
        //int index = UIRoot.Instance.dialogMask.transform.GetSiblingIndex();
        //if (UIRoot.Instance.dialog.GetChild(index - 1).gameObject.activeSelf)
        //{
        //    UIRoot.Instance.dialog.GetChild(index - 1).SetAsLastSibling();
        //    UIRoot.Instance.dialogMask.gameObject.SetActive(true);
        //    return;
        //}
        //if (UIRoot.Instance.panelMask.gameObject.activeSelf == false)
        //    Messenger.Broadcast(GlobalEvent.Exit_UI);
    }

    public void DestroyAllDialog()
    {
        for (int i = dialogList.Count - 1; i >= 0; i--)
        {
            HideDialog(dialogList[i]);
            GameObject.Destroy(dialogList[i]);
        }
        dialogList.Clear();
        //messageBoxList.Clear();
    }

    //飘字提示
    public void ShowTips(string str, string colorStr = "#FF0000")
    {
        //Tips.Instance.ShowTips(str, colorStr);
    }

    public void ShowHttpMask(bool b)
    {
        //if (httpMask == null)
        //{
        //    httpMask = UITool.Instantiate("Core/Prefab/HttpMask", UIRoot.Instance.top.gameObject).GetComponentInChildren<HttpMask>();
        //}
        //if(b == false)
        //{
        //    HttpClient.instance.httpMaskNum = 0;
        //}
        //httpMask.Show(b);
    }

    //弹窗提示(标题,内容,左按钮,右按钮,中按钮)
    //public void ShowMessageBox(MessageBoxData data)
    //{
    //    MessageBox go = null;
    //    for (int i = 0; i < messageBoxList.Count; i++)
    //    {
    //        if (!messageBoxList[i].gameObject.activeSelf)
    //        {
    //            go = messageBoxList[i];
    //            break;
    //        }
    //    }
    //    if (go == null)
    //    {
    //        go = UITool.Instantiate("Core/Prefab/MessageBox", UIRoot.Instance.dialog.gameObject).GetComponent<MessageBox>();
    //        messageBoxList.Add(go);
    //    }
    //    go.SetData(data);
    //    ShowDialog(go.gameObject);
    //}

    /// <summary>
    /// 获取核心资源库的UI图集
    /// </summary>
    /// <returns></returns>
    public SpriteAtlas GetCoreAtlas()
    {
        if (coreAtlas == null)
            coreAtlas = (SpriteAtlas)ResMgr.Instance.LoadByCore("Prefab/CoreAtlas");
        return coreAtlas;
    }

    //public bool IsHasPanel()
    //{
    //    return UIRoot.Instance.panelMask || UIRoot.Instance.dialogMask;
    //}

    public GameObject GetCurrentUI()
    {
        //if (UIRoot.Instance.dialogMask.gameObject.activeSelf)
        //{
        //    int index = UIRoot.Instance.dialogMask.transform.GetSiblingIndex();
        //    return UIRoot.Instance.dialogMask.transform.parent.GetChild(index + 1).gameObject;
        //}
        //if (UIRoot.Instance.panelMask.gameObject.activeSelf)
        //{
        //    int index = UIRoot.Instance.panelMask.transform.GetSiblingIndex();
        //    return UIRoot.Instance.panelMask.transform.parent.GetChild(index + 1).gameObject;
        //}
        return null;
    }
}

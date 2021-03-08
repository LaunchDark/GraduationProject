using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TipsCanvas : MonoBehaviour
{
    protected static TipsCanvas instance;
    public static TipsCanvas Instance
    {
        get
        {
            if (instance == null)
            {
                instance = UITool.Instantiate("UI/TipsCanvas").GetComponent<TipsCanvas>();
            }
            return instance;
        }
    }

    protected Coroutine coroutine;
    /// <summary>
    /// 提示
    /// </summary>
    protected Text Tips;

    private void Awake()
    {
        transform.GetComponent<Canvas>().worldCamera = GameObject.Find("VRCamera").GetComponent<Camera>();
        transform.GetComponent<Canvas>().planeDistance = 0.5f;

        Tips = transform.Find("Tips").GetComponent<Text>();
        Tips.gameObject.SetActive(false);
        
    }

    public void CountDown()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        Tips.text = 5.ToString();
        Tips.gameObject.SetActive(true);
        coroutine = StartCoroutine(CountDownTime(5));
    }

    public IEnumerator CountDownTime(int i)
    {
        i++;
        while (i >= 0)
        {
            i--;
            Tips.text = i.ToString();
            if(i == 0)
            {
                Tips.gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(1);
        }
        
        ScreenShot();

        yield return 0;

        coroutine = StartCoroutine(ShowTips("截图成功"));
    }

    public IEnumerator ShowTips(string tip)
    {
        Tips.text = tip;
        Tips.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        Tips.gameObject.SetActive(false);
    }

    /// <summary>
    /// 屏幕截图
    /// </summary>
    /// <param name="IsEnableAlpha">是否开启透明通道</param>
    private void ScreenShot(bool IsEnableAlpha = false)
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

        int resolutionX = (int)Handles.GetMainGameViewSize().x;
        int resolutionY = (int)Handles.GetMainGameViewSize().y;
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

}

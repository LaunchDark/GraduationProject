using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// 镜头UI，跟随头部移动
/// </summary>
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

    public Coroutine coroutine;
    /// <summary>
    /// 提示
    /// </summary>
    protected Text Tips;

    private void Awake()
    {
        transform.GetComponent<Canvas>().worldCamera = GameObject.Find("VRCamera").GetComponent<Camera>();
        transform.GetComponent<Canvas>().planeDistance = 0.4f;

        Tips = transform.Find("Tips").GetComponent<Text>();
        Tips.gameObject.SetActive(false);
        
    }

    /// <summary>
    /// 截图倒计时
    /// </summary>
    public void CountDown(int i = 5)
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(DelayToDo(() => { MyTools.ScreenShot(); }, 5, () => { ShowTips("截屏成功"); }));
    }
        
    /// <summary>
    /// 显示提示
    /// </summary>
    /// <param name="str">提示内容</param>
    /// <param name="i">提示存在时间</param>
    public void ShowTips(string str,int i = 1)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(StartShowTips(str,i));
    }

    /// <summary>
    /// 显示提示
    /// </summary>
    /// <param name="tip">提示显示时长</param>
    /// <returns></returns>
    protected IEnumerator StartShowTips(string tip,int time = 1)
    {
        Tips.text = tip;
        Tips.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        Tips.gameObject.SetActive(false);
    }

    /// <summary>
    /// 倒计时执行
    /// </summary>
    /// <param name="callback1">倒计时结束执行的程序</param>
    /// <param name="callback2">程序结束后执行的程序</param>
    /// <param name="i">倒计时间</param>
    /// <returns></returns>
    protected IEnumerator DelayToDo(Callback callback1,int i = 5, Callback callback2 = null)
    {
        Tips.text = i.ToString();
        Tips.gameObject.SetActive(true);
        i++;
        while (i >= 0)
        {
            i--;
            Tips.text = i.ToString();
            if (i == 0)
            {
                Tips.gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(1);
        }
        
        callback1.Invoke();        

        yield return 0;

        if (callback2 != null)
        {
            callback2.Invoke();
        }
    }

}

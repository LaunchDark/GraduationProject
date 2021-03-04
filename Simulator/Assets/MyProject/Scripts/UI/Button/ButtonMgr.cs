using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// mButton管理类
/// </summary>
public class ButtonMgr : MonoBehaviour
{
    protected static ButtonMgr instance;
    public static ButtonMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("ButtonMgr").AddComponent<ButtonMgr>();
            }
            return instance;
        }
    }

    /// <summary>
    /// 上一个按键
    /// </summary>
    public mButton lastBtn;
    /// <summary>
    /// 当前按键
    /// </summary>
    public mButton curBtn;

    private void Update()
    {
        //不存在上一个按键
        if (lastBtn == null)
        {
            if (curBtn)
            {
                curBtn.HandEnter();
            }
        }
        //存在上一个按键
        else
        {
            //如果已经离开了按键，执行按键的退出方法
            if (curBtn != lastBtn)
            {
                lastBtn.HandExit();
                lastBtn = null;
            }
        }
    }
}

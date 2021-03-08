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

}

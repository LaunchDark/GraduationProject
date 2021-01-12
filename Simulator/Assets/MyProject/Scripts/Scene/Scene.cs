using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{
    private void Awake()
    {
        var packsackMgr = PacksackMgr.Instance;//初始化背包系统
        var instrumentMgr = InstrumentMgr.Instance;//初始化仪器系统
    }
}

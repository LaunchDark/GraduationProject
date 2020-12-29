using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyMgr : MonoBehaviour
{
    static InputKeyMgr instance;
    public static InputKeyMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("InputKeyMgr").AddComponent<InputKeyMgr>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    private List<InputKeyInterface> inputKeyInterfaces = new List<InputKeyInterface>();



}

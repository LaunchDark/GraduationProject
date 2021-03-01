using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRoot : MonoBehaviour
{
    protected static UIRoot instance;
    public static UIRoot Instance
    {
        get
        {
            if(instance == null)
            {
                instance = UITool.Instantiate("UI/UIRoot", LeftHand.Instance.gameObject).GetComponent<UIRoot>();
            }
            return instance;
        }
    }

    public void Init()
    {
        
    }

    private void Update()
    {
        transform.position = LeftHand.Instance.transform.position;
        transform.LookAt(Valve.VR.InteractionSystem.Player.instance.transform);
    }
}

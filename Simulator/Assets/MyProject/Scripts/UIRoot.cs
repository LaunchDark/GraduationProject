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

    public float distance = 0.1f;
    [HideInInspector] public Transform Left;
    [HideInInspector] public Transform Right;
    [HideInInspector] public Transform Top;
    [HideInInspector] public Transform Else;


    public void Init()
    {
        HideUIRoot();
        Left = transform.Find("Left");
        Right = transform.Find("Right");
        Else = transform.Find("Else");
        Top = transform.Find("Top");
    }

    private void Update()
    {
        transform.position = LeftHand.Instance.transform.position + (LeftHand.Instance.transform.position - Valve.VR.InteractionSystem.Player.instance.transform.GetComponentInChildren<Camera>().transform.position).normalized * distance;
        transform.LookAt(transform.position + (transform.position - Valve.VR.InteractionSystem.Player.instance.transform.GetComponentInChildren<Camera>().transform.position).normalized * 1.0f);
    }
    
    public void ShowUIRoot()
    {
        gameObject.SetActive(true);
    }

    public void HideUIRoot()
    {
        gameObject.SetActive(false);

    }

}

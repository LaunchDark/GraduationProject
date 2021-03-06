﻿using System.Collections;
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
                instance = UITool.Instantiate("UI/UIRoot").GetComponent<UIRoot>();
            }
            return instance;
        }
    }

    public float distance = 0.1f;
    [HideInInspector] public Transform Left;
    [HideInInspector] public Transform Right;
    [HideInInspector] public Transform Top;

    [HideInInspector] public Dictionary<int, Packsack> AllPacksack;


    public void Init()
    {
        HideUIRoot();
        Left = transform.Find("Left");
        Right = transform.Find("Right");
        Top = transform.Find("Top");

        AllPacksack = new Dictionary<int, Packsack>();
        foreach (InstrumentTypeEnum value in InstrumentTypeEnum.GetValues(typeof(InstrumentTypeEnum)))
        {
            Packsack packsack = UITool.Instantiate("Packsack/Packsack", Right.gameObject).GetComponent<Packsack>();
            AllPacksack.Add((int)value, packsack);
            packsack.Init(value.ToString());
        }
    }

    private void Update()
    {
        transform.LookAt(transform.position + (transform.position - Valve.VR.InteractionSystem.Player.instance.transform.GetComponentInChildren<Camera>().transform.position).normalized * 1.0f);
        //Debug.Log(transform.localEulerAngles.x);
        if (transform.localEulerAngles.x > 15 && transform.localEulerAngles.x < 180)
        {
            transform.localEulerAngles = new Vector3(15, transform.localEulerAngles.y, 0);
        }
        else if (transform.localRotation.x >180 && transform.localEulerAngles.x < 345)
        {
            transform.localEulerAngles = new Vector3(345, transform.localEulerAngles.y, 0);
        }
    }
    
    public void ShowUIRoot(Vector3 pos)
    {
        //transform.position = LeftHand.Instance.transform.position + (LeftHand.Instance.transform.position - Valve.VR.InteractionSystem.Player.instance.transform.GetComponentInChildren<Camera>().transform.position).normalized * distance;
        transform.position = pos + (pos - Valve.VR.InteractionSystem.Player.instance.transform.GetComponentInChildren<Camera>().transform.position).normalized * distance;
        gameObject.SetActive(true);
    }

    public void HideUIRoot()
    {
        gameObject.SetActive(false);

    }

}

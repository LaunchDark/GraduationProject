﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PacksackItem : MonoBehaviour
{
    public mButton btn;
    public SpriteAtlas atlas;
    public GameObject mask;
    public GameObject select;
    public Image icon;
    public Text itemName;
    public Text num;
    
    private PacksackItemData mData;

    private void Awake()
    {
        btn.clickCallBack = Click;
        btn.exitCallBack = OnPointerExit;
        btn.enterCallBack = OnPointerEnter;

        select.SetActive(false);
        Messenger.AddListener(GlobalEvent.Packsack_Item_Change,()=> { 
            if(gameObject.activeSelf && mData!=null)
            {   
                SetData(mData);
            }
        });
    }

    private void OnEnable()
    {
        SetData(mData);
    }

    public void SetData(PacksackItemData data)
    {
        mData = data;
        if (data == null) return;
        mask.SetActive(data.num == 0);
        btn.SetIsTween(!(data.num == 0));
        icon.sprite = atlas.GetSprite(data.icon);
        
        num.text = data.num >= 0 ? data.num.ToString() : "";
        if(data.num >= 0)
        {
            num.transform.parent.gameObject.SetActive(false);
            num.text = data.num.ToString();
        }
        else
        {
            num.transform.parent.gameObject.SetActive(false);
        }

        itemName.text = data.name;
        select.SetActive(false);
    }

    public void Click()
    {
        if (mData.callback != null)
            mData.callback.Invoke(mData, false);
    }

    public void OnPointerEnter()
    {
        if (btn.button.interactable && !mask.activeSelf)
        {
            select.SetActive(true);
        }
    }

    public void OnPointerExit()
    {
        if (btn.button.interactable)
        {
            select.SetActive(false);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PacksackItem : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    //public CButton btn;
    public SpriteAtlas atlas;
    public GameObject mask;
    public GameObject select;
    public Text selectText;
    public Image icon;
    public Text itemName;
    public Text num;
    
    private PacksackItemData mData;

    private void Awake()
    {
        //btn.clickCallBack = Click;
        select.SetActive(false);
        selectText.gameObject.SetActive(false);
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
        icon.sprite = atlas.GetSprite(data.icon);
        num.text = data.num >= 0 ? data.num.ToString() : "";
        if(data.num >= 0)
        {
            num.transform.parent.gameObject.SetActive(true);
            num.text = data.num.ToString();
        }
        else
        {
            num.transform.parent.gameObject.SetActive(false);
        }
        itemName.text = data.name;
        selectText.text = data.key;
        select.SetActive(false);
        selectText.gameObject.SetActive(false);
    }

    public void Click()
    {
        if (mData.callback != null)
            mData.callback.Invoke(mData, false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //if (btn.button.interactable && !mask.activeSelf)
        //{
        //    select.SetActive(true);
        //    selectText.gameObject.SetActive(true);
        //}
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //if (btn.button.interactable)
        //{
        //    select.SetActive(false);
        //    selectText.gameObject.SetActive(false);
        //}
    }

}

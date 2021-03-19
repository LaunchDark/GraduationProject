using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScrollControl : MonoBehaviour
{
    public mButton UpBtn;
    public mButton DownBtn;
    public Transform content;
    public RectTransform Mask;

    protected virtual void Awake()
    {
        UpBtn = transform.Find("Scrollbar Button/Up").GetComponent<mButton>();
        DownBtn = transform.Find("Scrollbar Button/Down").GetComponent<mButton>();
        content = transform.Find("Viewport/Content").transform;
        Mask = transform.Find("Viewport").GetComponent<RectTransform>();

        UpBtn.clickCallBack = () => { MoveContent((int)content.GetComponent<RectTransform>().anchoredPosition.y - 100); };
        UpBtn.enterCallBack = () =>
        {
            if (UpBtn.transform.Find("Image/Enter"))
                UpBtn.transform.Find("Image/Enter").gameObject.SetActive(true);
        };
        UpBtn.exitCallBack = () =>
        {
            if (UpBtn.transform.Find("Image/Enter"))
                UpBtn.transform.Find("Image/Enter").gameObject.SetActive(false);
        };

        DownBtn.clickCallBack = () => { MoveContent((int)content.GetComponent<RectTransform>().anchoredPosition.y + 100); };
        DownBtn.enterCallBack = () =>
        {
            if (DownBtn.transform.Find("Image/Enter"))
                DownBtn.transform.Find("Image/Enter").gameObject.SetActive(true);
        };
        DownBtn.exitCallBack = () =>
        {

            if (DownBtn.transform.Find("Image/Enter"))
                DownBtn.transform.Find("Image/Enter").gameObject.SetActive(false);
        };
    }

    protected virtual void MoveContent(int distance)
    {
        if (distance < 0)
        {
            distance = 0;
        }
        //内容位移大于遮罩
        else if(distance > content.GetComponent<RectTransform>().sizeDelta.y - Mask.rect.height
            && content.GetComponent<RectTransform>().sizeDelta.y > Mask.rect.height)
        {
            distance = (int)content.GetComponent<RectTransform>().sizeDelta.y - (int)Mask.rect.height;
        }
        //内容太少，不用移动
        else if(content.GetComponent<RectTransform>().sizeDelta.y <= Mask.rect.height)
        {
            distance = 0;
        }

        content.DOLocalMoveY(distance,0.2f);
    }

}

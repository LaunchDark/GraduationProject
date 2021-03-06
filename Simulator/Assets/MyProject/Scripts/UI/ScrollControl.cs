using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScrollControl : MonoBehaviour
{
    public mButton UpBtn;
    public mButton DownBtn;
    public Transform content;

    protected virtual void Awake()
    {
        UpBtn = transform.Find("Scrollbar Button/Up").GetComponent<mButton>();
        DownBtn = transform.Find("Scrollbar Button/Down").GetComponent<mButton>();
        content = transform.Find("Viewport/Content").transform;

        UpBtn.clickCallBack = () => { MoveContent((int)content.GetComponent<RectTransform>().anchoredPosition.y - 100); };
        DownBtn.clickCallBack = () => { MoveContent((int)content.GetComponent<RectTransform>().anchoredPosition.y + 100); };
    }

    protected virtual void MoveContent(int distance)
    {
        if (distance < 0)
        {
            distance = 0;
        }
        //500 是遮罩的大小
        else if(distance > content.GetComponent<RectTransform>().sizeDelta.y - 500 && content.GetComponent<RectTransform>().sizeDelta.y > 500)
        {
            distance = (int)content.GetComponent<RectTransform>().sizeDelta.y - 500;
        }
        else
        {
            distance = 0;
        }

        content.DOLocalMoveY(distance,0.2f);
    }

}

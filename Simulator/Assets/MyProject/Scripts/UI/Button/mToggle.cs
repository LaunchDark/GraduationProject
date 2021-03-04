using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(Toggle))]
public class mToggle : UIElement
{
    [HideInInspector] public Toggle toggle = null;

    private Vector3 originScale = Vector3.one;
    private Sequence tw = null;
    public bool isTween = true;

    protected float zoom = 1.1f;
    protected Ease ease = Ease.Linear;

    protected new BoxCollider collider;

    private UnityAction<bool> m_callFun;
    public UnityAction<bool> callFun
    {
        set
        {
            m_callFun = value;
            m_callFun(selected);
        }
        get
        {
            return m_callFun;
        }
    }

    public bool selected
    {
        set
        {
            toggle.isOn = value;
        }
        get
        {
            return toggle.isOn;
        }
    }

    protected override void Awake()
    {
        toggle = transform.GetComponent<Toggle>();
        toggle.onValueChanged.AddListener((a) =>
        {
            //ButtonClick();
            OnButtonClick();
        });
        originScale = transform.localScale; 
        collider = transform.Find("Collider").GetComponent<BoxCollider>();
        collider.size = new Vector3(transform.GetComponent<RectTransform>().sizeDelta.x, transform.GetComponent<RectTransform>().sizeDelta.y, 15.0f);

    }

    private void OnEnable()
    {
        if (!isTween) return;
        if (tw != null) tw.Kill();
        transform.localScale = originScale;
    }

    /// <summary>
    /// 手部选中
    /// </summary>
    /// <param name="hand"></param>
    protected override void OnHandHoverBegin(Hand hand)
    {
        currentHand = hand;
        InputModule.instance.HoverBegin(gameObject);
        //ControllerButtonHints.ShowButtonHint(hand, hand.uiInteractAction);
        HandEnter();
    }

    protected override void HandHoverUpdate(Hand hand)
    {
        if (hand.uiInteractAction != null && hand.uiInteractAction.GetStateDown(hand.handType))
        {
            InputModule.instance.Submit(gameObject);
            //ControllerButtonHints.HideButtonHint(hand, hand.uiInteractAction);
        }
    }

    /// <summary>
    /// 手部离开
    /// </summary>
    /// <param name="hand"></param>
    protected override void OnHandHoverEnd(Hand hand)
    {
        InputModule.instance.HoverEnd(gameObject);
        //ControllerButtonHints.HideButtonHint(hand, hand.uiInteractAction);
        currentHand = null;
        HandExit();
    }

    protected override void OnButtonClick()
    {
        //onHandClick.Invoke(currentHand);
        ButtonClick();
    }


    /// <summary>
    /// 进入按键动画
    /// </summary>
    public virtual void HandEnter()
    {
        //Debug.Log("Enter");
        if (!isTween) return;
        if (!toggle.interactable) return;
        if (tw != null) tw.Kill();
        tw.Append(transform.DOScale(originScale * zoom, 0.2f).SetEase(ease));
    }

    /// <summary>
    /// 退出按键动画
    /// </summary>
    public virtual void HandExit()
    {
        //Debug.Log("Exit");
        if (!isTween) return;
        if (!toggle.interactable) return;
        if (tw != null) tw.Kill();
        tw.Append(transform.DOScale(originScale, 0.2f).SetEase(ease));
    }

    /// <summary>
    /// 点击按键动画
    /// </summary>
    public virtual void HandClickDown()
    {
        //Debug.Log("Down");
        if (!isTween) return;
        //if (!toggle.interactable) return;
        if (tw != null) tw.Kill();

        tw.Append(transform.DOScale(originScale, 0.1f).SetEase(ease));
        tw.Append(transform.DOScale(originScale * zoom, 0.1f).SetEase(ease));
    }

    public virtual void ButtonClick()
    {
        Debug.Log("ToggleClick");
        HandClickDown();
    }
}

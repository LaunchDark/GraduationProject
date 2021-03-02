﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
using Valve.VR.InteractionSystem;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class mButton : UIElement
{
    [HideInInspector]
    public Button button = null;

    public UnityAction clickCallBack = null;
    public UnityAction enterCallBack = null;
    public UnityAction exitCallBack = null;

    private Vector3 originScale = Vector3.one;
    private Tweener tw = null;
    public bool isTween = true;

    protected float zoom = 1.1f;
    protected Ease ease = Ease.Linear;

    protected override void Awake()
    {
        button = transform.GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            //ButtonClick();
            OnButtonClick();
        });
        originScale = transform.localScale;
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
        if (!button.interactable) return;
        if (tw != null) tw.Kill();
        tw = transform.DOScale(originScale * zoom, 0.2f).SetEase(ease);
        //执行进入回调
        if (enterCallBack != null)
            enterCallBack.Invoke();
    }

    /// <summary>
    /// 退出按键动画
    /// </summary>
    public virtual void HandExit()
    {
        //Debug.Log("Exit");
        if (!isTween) return;
        if (!button.interactable) return;
        if (tw != null) tw.Kill();
        tw = transform.DOScale(originScale, 0.2f).SetEase(ease);
        //执行退出回调
        if (exitCallBack != null)
            exitCallBack.Invoke();
    }

    /// <summary>
    /// 点击按键动画
    /// </summary>
    public virtual void HandClickDown()
    {
        //Debug.Log("Down");
        if (!isTween) return;
        if (!button.interactable) return;
        if (tw != null) tw.Kill();
        tw = transform.DOScale(originScale, 0.2f).SetEase(ease);
    }

    public virtual void ButtonClick()
    {
        //Debug.Log("Click");
        HandClickDown();
        //执行点击回调
        if (clickCallBack != null)
            clickCallBack.Invoke();
    }
}
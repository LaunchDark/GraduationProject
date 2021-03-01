using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Button))]
public class mButton : UIElement
{
    [HideInInspector]
    public Button button = null;

    public UnityAction clickCallBack = null;
    public UnityAction enterCallBack = null;
    public UnityAction exitCallBack = null;

    protected override void Awake()
    {
        button = transform.GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if (clickCallBack != null)
                clickCallBack.Invoke();
        });
    }

    /// <summary>
    /// 手部选中
    /// </summary>
    /// <param name="hand"></param>
    protected override void OnHandHoverBegin(Hand hand)
    {
        base.OnHandHoverBegin(hand);
    }

    protected override void HandHoverUpdate(Hand hand)
    {
        base.HandHoverUpdate(hand);
    }

    /// <summary>
    /// 手部离开
    /// </summary>
    /// <param name="hand"></param>
    protected override void OnHandHoverEnd(Hand hand)
    {
        base.OnHandHoverEnd(hand);
    }

    protected override void OnButtonClick()
    {
        ButtonClick();
    }

    public virtual void HandEnter()
    {

    }

    public virtual void HandExit()
    {

    }

    public virtual void HandClickDown()
    {

    }

    public virtual void ButtonClick()
    {
        button.onClick.Invoke();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
using Valve.VR.InteractionSystem;
using DG.Tweening;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Interactable))]
public class mButton : UIElement
{
    [HideInInspector] public Button button = null;

    public UnityAction clickCallBack = null;
    public UnityAction enterCallBack = null;
    public UnityAction exitCallBack = null;

    private Vector3 originScale = Vector3.one;
    //private Tweener tw = null;
    private Sequence tw = null;
    public bool isTween = true;

    protected float zoom = 1.1f;
    protected Ease ease = Ease.Linear;

    protected new BoxCollider collider;

    protected override void Awake()
    {
        button = transform.GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            //ButtonClick();
            OnButtonClick();
        });
        originScale = transform.localScale;
        collider = transform.Find("Collider").GetComponent<BoxCollider>();
        //collider.size = new Vector3(transform.GetComponent<RectTransform>().sizeDelta.x, transform.GetComponent<RectTransform>().sizeDelta.y, collider.size.z);
    }

    /// <summary>
    /// 初始化按键
    /// </summary>
    /// <param name="name"></param>
    public virtual void Init(string name,UnityAction click = null)
    {
        transform.Find("Name").GetComponent<Text>().text = name;
        clickCallBack = click;
    }

    protected virtual void OnEnable()
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
        if (currentHand)
        {
            currentHand.gameObject.GetComponent<HandBase>().SetTriggerDown(true);
            ButtonClick();
        }
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
        tw.Append(transform.DOScale(originScale * zoom, 0.2f).SetEase(ease));
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
        tw.Append(transform.DOScale(originScale, 0.2f).SetEase(ease));
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

        tw.Append(transform.DOScale(originScale, 0.1f).SetEase(ease));
        tw.Append(transform.DOScale(originScale * zoom, 0.1f).SetEase(ease));

        //tw = transform.DOScale(originScale, 0.2f).SetEase(ease);
    }

    public virtual void ButtonClick()
    {
        //Debug.Log("Click");
        HandClickDown();
        //执行点击回调
        if (currentHand)
        {
            if (clickCallBack != null && !currentHand.gameObject.GetComponent<HandBase>().GetGripDown())
            {
                clickCallBack.Invoke();
            }
        }
    }

    /// <summary>
    /// 设置碰撞
    /// </summary>
    /// <param name="vector3"></param>
    public virtual void SetCollider(Vector3 vector3)
    {
        collider.size = vector3;
    }

    public virtual void SetText(string str)
    {
        transform.Find("Name").GetComponent<Text>().text = str;
    }

    public virtual void SetIsTween(bool b)
    {
        isTween = b;
        if (!isTween)
        {
            tw.Append(transform.DOScale(originScale, 0.2f).SetEase(ease));
        }
    }

    public virtual string GetButtonName()
    {
        return transform.Find("Name").GetComponent<Text>().text;
    }
}
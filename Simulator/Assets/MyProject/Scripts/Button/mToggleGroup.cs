using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleGroup))]
public class mToggleGroup : MonoBehaviour
{
    [HideInInspector]
    public mToggle[] items;
    //[HideInInspector]
    public int count = 3;
    [HideInInspector]
    public int index = -1;
    private ToggleGroup toggleGroup;

    private UnityAction<int> m_callFun;
    public UnityAction<int> callFun
    {
        set
        {
            m_callFun = value;
            m_callFun(index);
        }
        get
        {
            return m_callFun;
        }
    }

    private void Awake()
    {
        index = -1;
        items = transform.GetComponentsInChildren<mToggle>();
        toggleGroup = transform.GetComponent<ToggleGroup>();
    }

    private void Start()
    {
        Init(count);
    }

    public void Init(int num)
    {
        int need = num - items.Length;
        for (int i = 0; i < need; i++)
        {
            Instantiate(items[0].gameObject, transform);
        }
        items = transform.GetComponentsInChildren<mToggle>(true);
        for (int i = 0; i < items.Length; i++)
            items[i].toggle.onValueChanged.RemoveAllListeners();
        for (int i = 0; i < items.Length; i++)
        {
            items[i].gameObject.name = i.ToString();
            items[i].selected = false;
            items[i].gameObject.SetActive(i < num);
            items[i].toggle.group = toggleGroup;
            items[i].toggle.onValueChanged.AddListener(ToggleClick);
        }
        SetIndex(-1);
    }

    private void ToggleClick(bool b)
    {
        if (b)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].selected)
                {
                    index = i;
                    items[i].toggle.interactable = false;
                    if (callFun != null) callFun(index);
                }
                else
                {
                    items[i].toggle.interactable = true;
                    items[i].HandExit();
                }
            }
        }
    }

    public void SetIndex(int i)
    {
        index = i;
        if (i < 0)
        {
            toggleGroup.SetAllTogglesOff();
            if (callFun != null) callFun(index);
            for (int p = 0; p < items.Length; p++)
                items[p].toggle.interactable = true;
        }
        else
            items[i].selected = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 根据选项，显示对应的背包面板
/// </summary>
public class TypeSelect : mToggleGroup
{
    protected UnityAction<string> SelectFun;
    protected override void Start()
    {        
        Init(UIRoot.Instance.AllPacksack.Count);     
        
    }

    /// <summary>
    /// 初始化选项
    /// </summary>
    /// <param name="num">所有选项个数</param>
    public override void Init(int num)
    {
        int need = num - items.Length;
        for (int i = 0; i < need; i++)
        {
            Instantiate(items[0].gameObject, transform);
        }
        items = transform.GetComponentsInChildren<mToggle>(true);
        for (int i = 0; i < items.Length; i++)
        {
            items[i].toggle.onValueChanged.RemoveAllListeners();
        }
        for (int i = 0; i < items.Length; i++)
        {
            items[i].gameObject.name = UIRoot.Instance.AllPacksack[i].name;
            items[i].transform.Find("Label").GetComponent<Text>().text = UIRoot.Instance.AllPacksack[i].name;
            items[i].selected = false;
            items[i].gameObject.SetActive(i < num);
            items[i].toggle.group = toggleGroup;
            items[i].toggle.onValueChanged.AddListener(ToggleClick);
        }
        SetIndex(-1);
    }

    protected override void ToggleClick(bool b)
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
                    ShowPanel(i);
                }
                else
                {
                    items[i].toggle.interactable = true;
                    items[i].HandExit();
                }
            }
        }
    }

    /// <summary>
    /// 显示当前面板
    /// </summary>
    /// <param name="name">显示的面板名称</param>
    public void ShowPanel(int i)
    {
        foreach (var item in UIRoot.Instance.AllPacksack)
        {
            if(item.Key == i)
            {
                item.Value.gameObject.SetActive(true);
            }
            else
            {
                item.Value.gameObject.SetActive(false);
            }
        }
    }
}

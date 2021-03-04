using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeSelect : mToggleGroup
{
    protected ScrollRect[] allPanel;
    protected override void Start()
    {
        allPanel = UIRoot.Instance.transform.Find("Right").GetComponentsInChildren<ScrollRect>();
        Init(allPanel.Length);

        for (int i = 0; i < items.Length; i++)
        {
            callFun += ShowPanel;
        }
        
    }

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
            items[i].gameObject.name = i.ToString();
            items[i].transform.Find("Label").GetComponent<Text>().text = i.ToString();
            items[i].selected = false;
            items[i].gameObject.SetActive(i < num);
            items[i].toggle.group = toggleGroup;
            items[i].toggle.onValueChanged.AddListener(ToggleClick);
        }
        SetIndex(-1);
    }

    public void ShowPanel(int index)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if(i == index)
            {
                allPanel[i].gameObject.SetActive(true);
            }
            else
            {
                allPanel[i].gameObject.SetActive(false);
            }
        }
    }
}

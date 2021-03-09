using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialGroup : mToggleGroup
{
    protected override void Start()
    {
        
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
            items[i].gameObject.name = string.Format("款式{0}",i + 1);
            items[i].transform.Find("Label").GetComponent<Text>().text = string.Format("款式{0}", i + 1);
            items[i].selected = false;
            items[i].gameObject.SetActive(i < num);
            items[i].toggle.group = toggleGroup;
            items[i].toggle.onValueChanged.AddListener(ToggleClick);
        }
        SetIndex(ChangeCanvas.Instance.instrument.CurMaterial);
    }


}

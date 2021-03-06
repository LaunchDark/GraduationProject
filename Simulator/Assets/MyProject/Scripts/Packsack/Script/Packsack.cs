using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Packsack : MonoBehaviour
{
    public Transform content;

    public virtual void Init(string name)
    {
        gameObject.name = name;
        content = transform.Find("Viewport/Content");
        CreateItem();
    }

    public virtual void CreateItem()
    {
        //循环所有类型
        for (int i = 0; i < PacksackMgr.Instance.packsackItemDataList.Count; i++)
        {
            //如果类型对应，则生成一个选项
            if ((int)(InstrumentTypeEnum)Enum.Parse(typeof(InstrumentTypeEnum), gameObject.name) == PacksackMgr.Instance.packsackItemDataList[i].Type)
            {
                if (PacksackMgr.Instance.packsackItemDataList[i].isAtPacksack)
                {
                    UITool.Instantiate("Packsack/PacksackItem", content.gameObject).GetComponent<PacksackItem>().SetData(PacksackMgr.Instance.packsackItemDataList[i]);
                }
            } 
            else if(gameObject.name == "All")
            {
                if (PacksackMgr.Instance.packsackItemDataList[i].isAtPacksack)
                {
                    UITool.Instantiate("Packsack/PacksackItem", content.gameObject).GetComponent<PacksackItem>().SetData(PacksackMgr.Instance.packsackItemDataList[i]);
                }
            }
        }
    }

    public virtual void OnEnable()
    {

    }

    public virtual void OnDisable()
    {

    }
}

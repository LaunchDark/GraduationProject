using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Packsack : MonoBehaviour
{
    //public CButton closeBtn;
    public Transform content;

    //private PacksackInputKeyInterface packsackInputKeyInterface;
    //private PacksackLocation packsackLocation;

    private void Awake()
    {
        //closeBtn.clickCallBack = CloseBtnClick;
        //packsackInputKeyInterface = gameObject.AddComponent<PacksackInputKeyInterface>();
        //packsackLocation = transform.GetComponentInChildren<PacksackLocation>();
        CreateItem();
        //closeBtn.enterCallBack = () => closeBtn.transform.GetChild(0).gameObject.SetActive(true);
        //closeBtn.exitCallBack = () => closeBtn.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void CreateItem()
    {
        for (int i = 0; i < PacksackMgr.Instance.packsackItemDataList.Count; i++)
        {
            if(PacksackMgr.Instance.packsackItemDataList[i].isAtPacksack)
                UITool.Instantiate("Packsack/PacksackItem", content.gameObject).GetComponent<PacksackItem>().SetData(PacksackMgr.Instance.packsackItemDataList[i]);
        }
    }

    private void OnEnable()
    {
        //InputKeyMgr.Instance.Register(packsackInputKeyInterface);
    }

    private void OnDisable()
    {
        //InputKeyMgr.Instance.Remove(packsackInputKeyInterface);
    }

    private void CloseBtnClick()
    {
        UIMgr.Instance.HidePanel("Packsack/Packsack");
    }
}

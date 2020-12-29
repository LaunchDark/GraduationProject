using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PacksackLocation : MonoBehaviour
{
    public Transform mContent;
    public GameObject mText;

    private List<PacksackLocationItem> mItemList = new List<PacksackLocationItem>();

    private void Awake()
    {
        Messenger.AddListener(GlobalEvent.Packsack_Item_Change, () =>
        {
            if (gameObject.activeSelf)
            {
                for (int i = 0; i < mContent.childCount; i++)
                {
                    if (mContent.GetChild(i).gameObject.activeSelf)
                        return;
                }
                mText.gameObject.SetActive(true);
            }
        });
    }

    private PacksackLocationItem GetItem()
    {
        PacksackLocationItem item = null;
        for (int i = 0; i < mItemList.Count; i++)
        {
            if (mItemList[i].gameObject.activeSelf == false)
            {
                item = mItemList[i];
                item.transform.SetAsLastSibling();
            }
        }
        if(item == null)
        {
            item = UITool.Instantiate("Packsack/PacksackLocationItem", mContent.gameObject).GetComponent<PacksackLocationItem>();
            mItemList.Add(item);
        }
        return item;
    }

    private void OnEnable()
    {
        List<Instrument> mapInstrumentList = PacksackMgr.Instance.GetMapInstrumentList(new List<InstrumentEnum>() { InstrumentEnum.Nail },true);
        for (int i = 0; i < mapInstrumentList.Count; i++)
        {
            GetItem().SetTarget(mapInstrumentList[i]);
        }
        mText.gameObject.SetActive(mapInstrumentList.Count == 0);
    }

    private void OnDisable()
    {
        for (int i = 0; i < mItemList.Count; i++)
        {
            mItemList[i].gameObject.SetActive(false);
        }
    }


}

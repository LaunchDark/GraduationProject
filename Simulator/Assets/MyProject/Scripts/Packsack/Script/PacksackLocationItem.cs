using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PacksackLocationItem : MonoBehaviour
{
    public SpriteAtlas atlas;
    public Image mIcon;
    public Text mName;
    //public CButton mMoveBtn;
    //public CButton mDeleteBtn;

    public Instrument mTarget;

    private void Awake()
    {
        //mMoveBtn.clickCallBack = MoveBtnClick;
        //mDeleteBtn.clickCallBack = DeleteBtnClick;
        //mMoveBtn.enterCallBack = () => mMoveBtn.transform.GetChild(0).gameObject.SetActive(true);
        //mMoveBtn.exitCallBack = () => mMoveBtn.transform.GetChild(0).gameObject.SetActive(false);
        //mDeleteBtn.enterCallBack = () => mDeleteBtn.transform.GetChild(0).gameObject.SetActive(true);
        //mDeleteBtn.exitCallBack = () => mDeleteBtn.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void SetTarget(Instrument target)
    {
        mTarget = target;
        PacksackItemData packsackItemData;
        if(target.IsGroupInstrument())
            packsackItemData = PacksackMgr.Instance.GetPacksackItemData(target.groupInstrumentType);
        else
            packsackItemData = PacksackMgr.Instance.GetPacksackItemData(target.type);
        mName.text = packsackItemData.name;
        mIcon.sprite = atlas.GetSprite(packsackItemData.icon);
        gameObject.SetActive(true);
    }

    private void MoveBtnClick()
    {
        UIMgr.Instance.HidePanel("Packsack/Packsack");
        //Player.Instance.Transfer(mTarget.transform);
    }

    private void DeleteBtnClick()
    {
        gameObject.SetActive(false);
        //InstrumentMgr.Instance.DeleteSceneInstrumentStorageInfo(mTarget);
        InstrumentMgr.Instance.DeleteInstrument(mTarget);
        mTarget = null;
    }
}

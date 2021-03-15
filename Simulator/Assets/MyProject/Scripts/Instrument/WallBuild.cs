using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBuild : Instrument
{
    void Start()
    {
        isHasR = false;
        type = InstrumentEnum.墙;
        MaxOffsetZ = 3f;
        MinOffsetZ = 0.5f;
        canDropDis = 0f;
        adsorbTypeList = new List<InstrumentEnum>();
        adsorbTypeList.Add(InstrumentEnum.门);
        SetAdsorbCollider();

    }

    virtual protected void SetAdsorbCollider()
    {
        Destroy(adsorbCollider);
        GameObject col = new GameObject("门");
        col.transform.SetParent(transform);
        col.transform.localPosition = Vector3.zero;
        col.transform.localEulerAngles = Vector3.zero;
        col.transform.localScale = Vector3.one;
        adsorbCollider = col.AddComponent<SphereCollider>();
        adsorbCollider.isTrigger = true;
        (adsorbCollider as SphereCollider).center = new Vector3(0, -0.08f, 0);
        (adsorbCollider as SphereCollider).radius = 0.15f;
        col.layer = LayerMask.NameToLayer("Instrument");
    }
}

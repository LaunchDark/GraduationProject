using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildUICtr : MonoBehaviour
{
    public Image bg;
    public BoxCollider col;
    public Text Title;
    public mButton btn;
    public Building buildinfo;

    private void Awake()
    {
        buildinfo.newWall();
        buildinfo.SetWall();
        btn.clickCallBack = SetBuildInfo;
        UpdateMessages();
    }

    public void UpdateMessages()
    {
        Vector2 Pos = new Vector2(buildinfo.pos.x * 50, buildinfo.pos.z * 50);
        this.gameObject.GetComponent<RectTransform>().anchoredPosition = Pos;

        Vector2 size = new Vector2(buildinfo.lenght * 50, buildinfo.weight * 50);
        bg.rectTransform.sizeDelta = size;
        col.size = new Vector3(size.x, size.y, 45);
        Title.text = gameObject.name;
    }

    protected void SetBuildInfo()
    {
        transform.parent.parent.GetComponent<CreatBuildUI>().UIBuilding = buildinfo;
        transform.parent.parent.GetComponent<CreatBuildUI>().selectBuildUI = this.gameObject;
        transform.parent.parent.GetComponent<CreatBuildUI>().OpenBuildInfoUI();

    }
}

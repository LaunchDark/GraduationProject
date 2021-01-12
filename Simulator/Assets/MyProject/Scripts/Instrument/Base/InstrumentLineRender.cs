using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentLineRender : MonoBehaviour
{
    [HideInInspector] public LineRenderer lineRender;
    //[HideInInspector] public SpriteRenderer sprite;
    Vector3 lineRenderV3 = Vector3.zero;

    private Transform origin;

    private void Awake()
    {
        lineRender = GetComponent<LineRenderer>();
        //sprite = GetComponentInChildren<SpriteRenderer>();
        origin = transform;
    }


    public void SetOriginPoint(float value)
    {
        lineRender.SetPosition(0, new Vector3(0,0,value));
    }
    
    void LateUpdate()
    {
        Ray ray = new Ray(origin.position, transform.forward);
        RaycastHit hitInfo;
        
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            lineRender.enabled = true;
            //sprite.enabled = true;
            lineRenderV3.z = hitInfo.distance;
            lineRender.SetPosition(1, lineRenderV3);
            //lineRenderV3.z -= 0.01f;
            //sprite.transform.localPosition = lineRenderV3;
        }
        else
        {
            lineRender.enabled = false;
            //sprite.enabled = false;
        }

    }
}

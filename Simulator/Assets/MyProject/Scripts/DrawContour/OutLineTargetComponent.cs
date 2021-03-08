using UnityEngine;

[ExecuteInEditMode]
public class OutLineTargetComponent : MonoBehaviour
{
    public Material material { set; get; }

    private void Start()
    {

    }

    void OnEnable()
    {
        Camera[] allCameras = Camera.allCameras;
        for (int i = 0; i < allCameras.Length; i++)
        {
            if (allCameras[i].GetComponent<OutLineCameraComponent>() != null)
            {
                allCameras[i].GetComponent<OutLineCameraComponent>().AddTarget(this);
            }
        }
    }
    private void Update()
    {
               
    }

    void OnDisable()
    {
        Camera[] allCameras = Camera.allCameras;
        for (int i = 0; i < allCameras.Length; i++)
        {
            if (allCameras[i].GetComponent<OutLineCameraComponent>() != null)
            {
                allCameras[i].GetComponent<OutLineCameraComponent>().RemoveTarget(this);
            }
        }
    }

    public void SetColor(Color color)
    {
        //Debug.Log("SetColor: " + color);
        //Debug.Log(material);
        if (material != null)
        {
            material.SetColor("_OutLineColor", color);
        }
    }
}
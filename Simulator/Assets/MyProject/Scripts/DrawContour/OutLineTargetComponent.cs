using UnityEngine;

[ExecuteInEditMode]
public class OutLineTargetComponent : MonoBehaviour
{
    //public Color color = Color.green;

    public Material material { set; get; }
    private float time;
    private float sumtime;
    private void Start()
    {
        sumtime = 5;
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
   
        //if (color.Equals(Color.green))
        //    time += UnityEngine.Time.deltaTime;
        //if (time >= sumtime) 
      
       
    }
    public void ResetTime() {
        time = 0;
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
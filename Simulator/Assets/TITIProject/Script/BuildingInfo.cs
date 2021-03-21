using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingInfo : MonoBehaviour
{
    static BuildingInfo instance;

   public static BuildingInfo Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new GameObject("BuildingInfo").AddComponent<BuildingInfo>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    public List<Transform> Windows = new List<Transform>();
    public List<Transform> Doors = new List<Transform>();

    public List<Transform> Walls = new List<Transform>();
    public List<Transform> Top = new List<Transform>();
    public List<Transform> Floor = new List<Transform>();


    //public void Init()
    //{
    //    Doors = new List<Transform>();
    //    Walls = new List<Transform>();
    //    Windows = new List<Transform>();
        
    //}
}

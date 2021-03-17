using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public List<Transform> Doors = new List<Transform>();
    public List<Transform> Walls = new List<Transform>();
    public List<Transform> Windows = new List<Transform>();
}

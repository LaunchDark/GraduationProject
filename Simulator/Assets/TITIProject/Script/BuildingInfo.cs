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

    public List<Transform> Windows;
    public List<Transform> Doors;

    public List<Transform> Walls;
    public List<Transform> Tops;
    public List<Transform> Floors;


    public void Init()
    {
        Doors = new List<Transform>();
        Walls = new List<Transform>();
        Windows = new List<Transform>();
        Tops = new List<Transform>();
        Floors = new List<Transform>();
    }
}

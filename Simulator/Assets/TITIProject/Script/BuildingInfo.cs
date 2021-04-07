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

    public List<Building> buildinfo = new List<Building>();

    public List<Transform> Windows = new List<Transform>();
    public List<Transform> Doors = new List<Transform>();

    public List<Transform> _Walls = new List<Transform>();
    public List<Transform> Walls = new List<Transform>();
    public List<Transform> Tops = new List<Transform>();
    public List<Transform> Floors = new List<Transform>();


    //public void Init()
    //{
    //    buildinfo = new List<Building>();

    //    Buildings = new List<Transform>();

    //    Doors = new List<Transform>();
    //    _Walls = new List<Transform>();
    //    Walls = new List<Transform>();
    //    Windows = new List<Transform>();
    //    Tops = new List<Transform>();
    //    Floors = new List<Transform>();
    //}

    public void ClearLists()
    {
        buildinfo.Clear();

        Doors.Clear();
        Windows.Clear();
        _Walls.Clear();
        Walls.Clear();
        Tops.Clear();
        Floors.Clear();
    }

    public void Building1Info()
    {
        Vector3 LivingRoomPos1 = new Vector3(0, 0, 0);
        Vector3 LivingRoomPos2 = new Vector3(-4.9f, 0, 1.6f);
        Vector3 KitchenPos = new Vector3(-5.9f, 0, -0.5f);
        Vector3 BathRoomPos = new Vector3(-6.6f, 0, 3.25f);
        Vector3 BedRoom1Pos = new Vector3(-3.675f, 0, 3.75f);
        Vector3 BedRoom2Pos = new Vector3(0.075f, 0, 3.75f);
        Vector3 BedRoom3Pos = new Vector3(4.15f, 0, 3.75f);

        Building livingRoom = new Building(BuildingType.both, 2.5f, 4.2f, 8.4f, 0.1f, LivingRoomPos1);
        livingRoom.newWall();
        livingRoom.SetWall();
        livingRoom.walls[2].SetHole(HoleType.Door, new Vector2(0, 0));
        livingRoom.isWall[0] = false;
        livingRoom.isWall[1] = false;
        livingRoom.isWall[2] = true;
        livingRoom.isWall[3] = true;
        BuildingInfo.Instance.buildinfo.Add(livingRoom);

        Building LivingRoom1 = new Building(BuildingType.none, 2.5f, 1.0f, 1.4f, 0.1f, LivingRoomPos2);
        LivingRoom1.newWall();
        LivingRoom1.SetWall();
        LivingRoom1.isWall[0] = false;
        LivingRoom1.isWall[1] = false;
        LivingRoom1.isWall[2] = false;
        LivingRoom1.isWall[3] = false;
        BuildingInfo.Instance.buildinfo.Add(LivingRoom1);

        Building Kitchen = new Building(BuildingType.both, 2.5f, 3.2f, 3.4f, 0.1f, KitchenPos);
        Kitchen.newWall();
        Kitchen.SetWall();
        Kitchen.walls[2].SetHole(HoleType.Windows, new Vector2(0.5f, 0.9f));
        Kitchen.walls[1].SetHole(HoleType.Windows, new Vector2(0.0f, 0.9f));
        Kitchen.walls[0].SetHole(HoleType.Door, new Vector2(1.0f, 0.0f));
        Kitchen.isWall[0] = true;
        Kitchen.isWall[1] = true;
        Kitchen.isWall[2] = true;
        Kitchen.isWall[3] = true;
        BuildingInfo.Instance.buildinfo.Add(Kitchen);

        Building BathRoom = new Building(BuildingType.both, 2.5f, 4.3f, 2.0f, 0.1f, BathRoomPos);
        BathRoom.newWall();
        BathRoom.SetWall();
        BathRoom.walls[3].SetHole(HoleType.Door, new Vector2(-1.60f, 0.0f));
        BathRoom.walls[1].SetHole(HoleType.Windows, new Vector2(0.0f, 0.9f));
        BathRoom.walls[0].SetHole(HoleType.Windows, new Vector2(0.0f, 0.9f));
        BathRoom.isWall[0] = true;
        BathRoom.isWall[1] = true;
        BathRoom.isWall[2] = false;
        BathRoom.isWall[3] = true;
        BuildingInfo.Instance.buildinfo.Add(BathRoom);

        Building BedRoom1 = new Building(BuildingType.both, 2.5f, 3.3f, 3.85f, 0.1f, BedRoom1Pos);
        BedRoom1.newWall();
        BedRoom1.SetWall();
        BedRoom1.walls[2].SetHole(HoleType.Door, new Vector2(1.325f, 0.0f));
        BedRoom1.walls[0].SetHole(HoleType.Windows, new Vector2(0.5f, 0.9f));
        BedRoom1.isWall[0] = true;
        BedRoom1.isWall[1] = false;
        BedRoom1.isWall[2] = true;
        BedRoom1.isWall[3] = true;
        BuildingInfo.Instance.buildinfo.Add(BedRoom1);

        Building BedRoom2 = new Building(BuildingType.both, 2.5f, 3.3f, 3.65f, 0.1f, BedRoom2Pos);
        BedRoom2.newWall();
        BedRoom2.SetWall();
        BedRoom2.walls[2].SetHole(HoleType.Door, new Vector2(1.225f, 0.0f));
        BedRoom2.walls[0].SetHole(HoleType.Windows, new Vector2(0.5f, 0.9f));
        BedRoom2.isWall[0] = true;
        BedRoom2.isWall[1] = false;
        BedRoom2.isWall[2] = true;
        BedRoom2.isWall[3] = true;
        BuildingInfo.Instance.buildinfo.Add(BedRoom2);

        Building BedRoom3 = new Building(BuildingType.both, 2.5f, 3.3f, 4.5f, 0.1f, BedRoom3Pos);
        BedRoom3.newWall();
        BedRoom3.SetWall();
        BedRoom3.walls[2].SetHole(HoleType.Door, new Vector2(-1.70f, 0.0f));
        BedRoom3.walls[0].SetHole(HoleType.Windows, new Vector2(-0.5f, 0.9f));
        BedRoom3.isWall[0] = true;
        BedRoom3.isWall[1] = false;
        BedRoom3.isWall[2] = true;
        BedRoom3.isWall[3] = true;
        BuildingInfo.Instance.buildinfo.Add(BedRoom3);
    }

    public void Building2Info()
    {
        Vector3 KitchenPos = new Vector3(0, 0, 0);
        Vector3 BathRoomPos = new Vector3(-1.6f, 0, 0);
        Vector3 livingRoomPos = new Vector3(-0.6f, 0, 4.55f);

        Building Kitchen = new Building(BuildingType.both, 2.5f, 3.1f, 2.0f, 0.1f, KitchenPos);
        Kitchen.newWall();
        Kitchen.SetWall();
        Kitchen.walls[0].SetHole(HoleType.Door, new Vector2(0.5f, 0.0f));
        Kitchen.walls[1].SetHole(HoleType.Door, new Vector2(0.95f, 0.0f));
        Kitchen.walls[2].SetHole(HoleType.Door, new Vector2(0.5f, 0.0f));
        Kitchen.isWall[0] = true;
        Kitchen.isWall[1] = true;
        Kitchen.isWall[2] = true;
        Kitchen.isWall[3] = true;
        BuildingInfo.instance.buildinfo.Add(Kitchen);

        Building BathRoom = new Building(BuildingType.both, 2.5f, 3.1f, 1.2f, 0.1f, BathRoomPos);
        BathRoom.newWall();
        BathRoom.SetWall();
        BathRoom.walls[3].SetHole(HoleType.Door, new Vector2(0.95f, 0.0f));
        BathRoom.isWall[0] = true;
        BathRoom.isWall[1] = true;
        BathRoom.isWall[2] = true;
        BathRoom.isWall[3] = false;
        BuildingInfo.instance.buildinfo.Add(BathRoom);

        Building livingRoom = new Building(BuildingType.both, 2.5f, 6.0f, 3.2f, 0.1f, livingRoomPos);
        livingRoom.newWall();
        livingRoom.SetWall();
        livingRoom.walls[0].SetHole(HoleType.Windows, new Vector2(0.0f, 0.9f));
        livingRoom.walls[2].SetHole(HoleType.Door, new Vector2(1.1f, 0.0f));
        livingRoom.isWall[0] = true;
        livingRoom.isWall[1] = true;
        livingRoom.isWall[2] = false;
        livingRoom.isWall[3] = true;
        BuildingInfo.instance.buildinfo.Add(livingRoom);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building1 : MonoBehaviour
{
    static Building1 instance;

    public void Building1Info()
    {
        Vector3 LivingRoomPos1 = new Vector3(0, 0, 0);
        Vector3 LivingRoomPos2 = new Vector3(-4.9f, 1.6f, 0);
        Vector3 KitchenPos = new Vector3(-5.9f, -0.5f, 0);
        Vector3 BathRoomPos = new Vector3(-6.6f, 3.25f, 0);

        Building livingRoom = new Building(BuildingType.both, 2.5f, 4.2f, 8.4f, 0.1f, LivingRoomPos1);
        livingRoom.walls[0].SetHole(HoleType.Door, new Vector2(0, 0));
        livingRoom.isWall[0] = true;
        livingRoom.isWall[3] = true;
        livingRoom.SetWall();
        BuildingInfo.Instance.buildinfo.Add(livingRoom);

        Building LivingRoom1 = new Building(BuildingType.none, 2.5f, 1.0f, 1.4f, 0.1f, LivingRoomPos2);
        LivingRoom1.SetWall();
        BuildingInfo.Instance.buildinfo.Add(livingRoom);

        Building Kitchen = new Building(BuildingType.both, 2.5f, 3.2f, 3.4f, 0.1f, KitchenPos);
        livingRoom.walls[0].SetHole(HoleType.Windows, new Vector2(0.5f, 0.9f));
        livingRoom.walls[1].SetHole(HoleType.Windows, new Vector2(0.0f, 0.9f));
        livingRoom.walls[2].SetHole(HoleType.Door, new Vector2(1.0f, 0.0f));
        livingRoom.isWall[0] = true;
        livingRoom.isWall[1] = true;
        livingRoom.isWall[2] = true;
        livingRoom.isWall[3] = true;
        livingRoom.SetWall();
        BuildingInfo.Instance.buildinfo.Add(Kitchen);

        Building BathRoom = new Building(BuildingType.both, 2.5f, 4.3f, 2.0f, 0.1f, BathRoomPos);
        BathRoom.walls[3].SetHole(HoleType.Door, new Vector2(-1.65f, 0.0f));
        BathRoom.walls[1].SetHole(HoleType.Windows, new Vector2(0.0f, 0.9f));
        BathRoom.walls[2].SetHole(HoleType.Windows, new Vector2(0.0f, 0.9f));
        BathRoom.isWall[0] = true;
        BathRoom.isWall[1] = true;
        BathRoom.isWall[2] = true;
        BathRoom.isWall[3] = true;
        BathRoom.SetWall();
        BuildingInfo.Instance.buildinfo.Add(Kitchen);
    }

}

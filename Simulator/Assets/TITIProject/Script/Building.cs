using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public enum BuildingType
{
    none,
    withWindow,
    withDoor,
    both
}



public class Building
{
    public BuildingType type = BuildingType.none;
    public Wall[] walls = new Wall[4];
    public float height = 2.5f;
    public float weight = 1.0f;
    public float lenght = 1.0f;
    public float thick = 0.1f;
    public Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);
    

    public Building(BuildingType Type, float Height, float Weight, float Lenght, float Thick, Vector3 Pos)
    {
        type = Type;
        height = Height;
        weight = Weight;
        lenght = Lenght;
        thick = Thick;
        this.pos = Pos;
    }

    public void SetWall()
    {
        Vector3 Pos;
        int num = 1;
        for(int x = 0; x <= 3; x++)
        {
            if(x % 2 == 0)
            {
                Pos = new Vector3(this.pos.x, this.pos.y + (this.height / 2), this.pos.z + (num * weight) / 2);             //前后墙壁的位置
                walls[x] = new Wall(this.lenght + this.thick, this.height + this.thick, this.thick, Pos, x, this.pos);
                num *= -1;
            }
            else
            {
                Pos = new Vector3(this.pos.x + (num * lenght) / 2, this.pos.y + (this.height / 2), this.pos.z);             //左右墙壁的位置
                walls[x] = new Wall(this.weight + this.thick, this.height + this.thick, this.thick, Pos, x, this.pos);
            }
        }
    }

    public void CreatWall(GameObject Parent)
    {
        for(int x = 0; x <= 3; x++)
        {
            walls[x].CreatWall("wall" + x, Parent);
        }
    }

    public void CreatTop(GameObject Parent)
    {
        Vector3 pos = new Vector3(this.pos.x, this.pos.y + height, this.pos.z);
        Vector3 scale = new Vector3(lenght, weight, thick);
        GameObject theTop = GameObject.CreatePrimitive(PrimitiveType.Cube);
        theTop.name = "Top";
        theTop.transform.localScale = scale;
        theTop.transform.position = pos;
        theTop.transform.localEulerAngles = new Vector3(90, 0, 0);


        theTop.transform.parent = Parent.transform;
        BuildingInfo.Instance.Tops.Add(theTop.transform);


    }

    public void CreatFloor(GameObject Parent)
    {
        Vector3 pos = new Vector3(this.pos.x, this.pos.y, this.pos.z);
        Vector3 scale = new Vector3(this.lenght + thick, this.thick, this.weight + thick);
        GameObject theFloor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        theFloor.transform.localScale = scale;
        theFloor.transform.position = pos;
        theFloor.name = "Floor";
        BuildingInfo.Instance.Floors.Add(theFloor.transform);
        theFloor.transform.parent = Parent.transform;
        
    }


}
  
﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HoleType
{
    Windows,
    Door
}

public class hole
{
    public HoleType Type;
    public Vector2 pos;
    public int wallNum;
    public Vector2 shape;
}

public class windows : hole
{


    public windows(int WallNum, Vector2 Shape, Vector2 Pos)
    {
        this.Type = HoleType.Windows;
        this.wallNum = WallNum;
        this.shape = Shape;
        this.pos = Pos;
    }
}

public class door : hole
{
    public door(int WallNum, Vector2 Shape, float posX)
    {
        this.Type = HoleType.Door;
        this.wallNum = WallNum;
        this.shape = Shape;
        this.pos = new Vector2(posX, Shape.y / 2);
    }
}

public class Wall
{
    public int wallNum = 0;
    public hole Hole = null;
    public Vector3 shape;
    public Vector3 pos;

    public Wall(float lenght, float weight, float thick, Vector3 pos, int wallNum)
    {
        this.wallNum = wallNum;
        shape = new Vector3(lenght, weight, thick);
        this.pos = pos;
    }

    public void CreatWall(string wallName, GameObject Parent)
    {
        if (this.Hole == null)
        {
            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.name = wallName;
            wall.transform.localScale = shape;
            wall.transform.position = this.pos;
            wall.transform.parent = Parent.transform;

        }
        else if (this.Hole.Type == HoleType.Door)
        {
            GameObject wall = new GameObject(wallName);
            wall.transform.position = new Vector3(pos.x, pos.y, pos.z);
            GameObject door0 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            door0.name = "door0";
            GameObject door1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            door1.name = "door1";
            GameObject door2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            door2.name = "door2";
            if (wallNum % 2 != 0)
            {
                door0.transform.position = new Vector3(pos.x, pos.y + this.Hole.shape.y / 2, pos.z + this.Hole.pos.x);         //计算门框位置
                door0.transform.localScale = new Vector3(shape.x, this.shape.y - this.Hole.shape.y, this.Hole.shape.x);         //大小缩放

                door1.transform.position = new Vector3(pos.x, pos.y, (pos.z - (shape.z / 2))
                    + (((this.pos.z + this.Hole.pos.x) - (this.Hole.shape.x / 2)) - (pos.z - (shape.z / 2))) / 2);

                door1.transform.localScale = new Vector3(shape.x, shape.y,
                    Math.Abs(((pos.z - (shape.z / 2)) - ((this.pos.z + this.Hole.pos.x) - (this.Hole.shape.x / 2)))));

                door2.transform.position = new Vector3(pos.x,  pos.y, (pos.z + (shape.z / 2))
                    - ((pos.z + (shape.z / 2)) - ((this.pos.z + this.Hole.pos.x) + (this.Hole.shape.x / 2))) / 2);

                door2.transform.localScale = new Vector3(shape.x, shape.y,
                    Math.Abs(((pos.z + (shape.z / 2)) - ((this.pos.z + this.Hole.pos.x) + (this.Hole.shape.x / 2)))));

                

            }
            else if (wallNum % 2 == 0)
            {
                door0.transform.position = new Vector3(pos.x + this.Hole.pos.x, pos.y + this.Hole.shape.y / 2, pos.z);
                door0.transform.localScale = new Vector3(this.Hole.shape.x, this.shape.y - this.Hole.shape.y, shape.z);

                door1.transform.position = new Vector3((pos.x - (shape.x / 2)) + 
                    (((this.pos.x + this.Hole.pos.x) - (this.Hole.shape.x / 2)) - (pos.x - (shape.x / 2))) / 2, pos.y, pos.z);

                door1.transform.localScale = new Vector3(Math.Abs(((pos.x - (shape.x / 2)) 
                    - ((this.pos.x + this.Hole.pos.x) - (this.Hole.shape.x / 2)))), shape.y, shape.z);

                door2.transform.position = new Vector3((pos.x + (shape.x / 2)) -
                    ((pos.x + (shape.x / 2)) - ((this.pos.x + this.Hole.pos.x) + (this.Hole.shape.x / 2))) / 2, pos.y, pos.z);

                door2.transform.localScale = new Vector3(Math.Abs(((pos.x + (shape.x / 2)) 
                    - ((this.pos.x + this.Hole.pos.x) + (this.Hole.shape.x / 2)))), shape.y, shape.z);
            }
            door0.transform.parent = wall.transform;
            door1.transform.parent = wall.transform;
            door2.transform.parent = wall.transform;
            wall.transform.parent = Parent.transform;
        }
        else if(this.Hole.Type == HoleType.Windows)
        {
            GameObject Wall = new GameObject(wallName);
            Wall.transform.position = new Vector3(pos.x, pos.y, pos.z);
            GameObject window0 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            window0.name = "window0";
            GameObject window1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            window1.name = "window1";
            GameObject window2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            window2.name = "window2";
            GameObject window3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            window3.name = "window3";

            if(wallNum % 2 != 0)
            {
                window0.transform.position = new Vector3(pos.x, pos.y + this.Hole.shape.y / 2, pos.z + this.Hole.pos.x);
            }
        }

    }

    public void SetHole(HoleType type, Vector2 shape, Vector2 pos)
    {
        if (type == HoleType.Door)
        {
            this.Hole = new door(wallNum, shape, pos.x);
        }
        else if (type == HoleType.Windows)
        {
            this.Hole = new windows(wallNum, shape, pos);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

public enum HoleType:int
{
    None = 0,
    Windows = 2,
    Door = 1
}
//public enum WallType:int
//{
//    AllWall = 0,
//    HalfWall = 1,
//    None = 2
//}

[System.Serializable]
public class hole
{
    public HoleType Type = HoleType.None;
    public Vector2Serializer pos;
    public int wallNum;
    public Vector2Serializer shape;

    public hole()
    {
        Type = HoleType.None;
        pos.init(new Vector2(0, 0));
    }

    public void SetWindow()
    {
        this.Type = HoleType.Windows;
        this.shape.init(new Vector2(1, 0.8f));
    }

    public void SetDoor()
    {
        this.Type = HoleType.Door;
        this.shape.init(new Vector2(0.9f, 2.0f));
    }
}
[System.Serializable]
public class windows :  hole
{
    public windows(int WallNum, Vector2 Shape, Vector2 Pos)
    {
        this.Type = HoleType.Windows;
        this.wallNum = WallNum;
        //this.shape = Shape;
        this.pos.init(Pos);
    }

    public windows(int WallNum, Vector2 Pos)
    {
        this.Type = HoleType.Windows;
        this.wallNum = WallNum;
        this.shape.init(new Vector2(1, 0.8f));
        this.pos.init(Pos);
    }

    
}

[System.Serializable]
public class door : hole
{
    //new public Vector2 shape = new Vector2(0.9f, 2.0f);
    public door(int WallNum, Vector2 Shape, float posX)
    {
        this.Type = HoleType.Door;
        this.wallNum = WallNum;
        this.shape.init(Shape);
        this.pos.init(new Vector2(posX, Shape.y / 2));
    }

    public door(int WallNum, float posX)
    {
        this.Type = HoleType.Door;
        this.wallNum = WallNum;
        this.shape.init(new Vector2(0.9f, 2.0f));
        this.pos.init(new Vector2(posX, this.shape.y / 2));
    }
}

[System.Serializable]
public class Wall
{
    public HoleType Type = 0;
    public int wallNum = 0;
    public hole Hole = null;
    public Vector3Serializer shape;
    public Vector3Serializer pos;
    public Vector3Serializer RoomPos;

    public void wall()
    {
        Hole = new hole();
    }
    public void setWall(float lenght, float weight, float thick, Vector3 pos, int wallNum, Vector3 RoomPos, HoleType type = 0)
    {
        this.Type = type;
        this.wallNum = wallNum;
        this.shape.init(new Vector3(lenght, weight, thick));
        this.pos.init(pos);
        this.RoomPos.init(RoomPos);
    }

    public void CreatWall(string wallName, GameObject Parent)
    {
        Vector3 dir = this.pos.V3 - this.RoomPos.V3;
        
        if (this.Hole.Type == HoleType.None)
        {
            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject _wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.name = wallName;
            _wall.name = "_" + wallName;

            _wall.transform.localScale = new Vector3(shape.x, shape.y, shape.z / 2);
            wall.transform.localScale = new Vector3(shape.x - 2 * shape.z, shape.y - 2 * shape.z, shape.z / 2);

            if (wallNum % 2 != 0)
            {
                if (dir.x < 0)
                {
                    wall.transform.localEulerAngles = new Vector3(0, 90, 0);
                    _wall.transform.localEulerAngles = new Vector3(0, -90, 0);

                    wall.transform.position = new Vector3(pos.x + shape.z / 4, pos.y, pos.z);
                    _wall.transform.position = new Vector3(pos.x - shape.z / 4, pos.y, pos.z);
                }
                else if (dir.x > 0)
                {
                    wall.transform.localEulerAngles = new Vector3(0, -90, 0);
                    _wall.transform.localEulerAngles = new Vector3(0, 90, 0);

                    _wall.transform.position = new Vector3(this.pos.x + shape.z / 4, this.pos.y, this.pos.z);
                    wall.transform.position = new Vector3(pos.x - shape.z / 4, pos.y, pos.z);
                }
                
            }
            else
            {
                if (dir.z < 0)
                {
                    _wall.transform.localEulerAngles = new Vector3(0, 180, 0);

                    wall.transform.position = new Vector3(this.pos.x, this.pos.y, this.pos.z + shape.z / 4);
                    _wall.transform.position = new Vector3(pos.x, pos.y, pos.z - shape.z / 4);
                }
                else if (dir.z > 0)
                {
                    wall.transform.localEulerAngles = new Vector3(0, 180, 0);
                    
                    _wall.transform.position = new Vector3(this.pos.x, this.pos.y, this.pos.z + shape.z / 4);
                    wall.transform.position = new Vector3(pos.x, pos.y, pos.z - shape.z / 4);
                }
                
            }

            
           
            wall.transform.parent = Parent.transform;
            _wall.transform.parent = Parent.transform;
            BuildingInfo.Instance.Walls.Add(wall.transform);
            BuildingInfo.Instance._Walls.Add(_wall.transform);

        }
        else if (this.Hole.Type == HoleType.Door)
        {
            this.Hole.shape.init(new Vector2(0.9f, 2.0f));
            GameObject wall = new GameObject(wallName);
            
            GameObject _wall = new GameObject("_" + wallName);
            

            GameObject thedoor = new GameObject("door" + wallNum);
            //wall.transform.position = new Vector3(pos.x, pos.y, pos.z);
            GameObject door0 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            door0.name = "door0";
            GameObject _door0 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _door0.name = "_door0";
            GameObject door1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            door1.name = "door1";
            GameObject _door1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _door1.name = "_door1";
            GameObject door2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            door2.name = "door2";
            GameObject _door2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _door2.name = "_door2";
            if (wallNum % 2 != 0)
            {
                
                thedoor.transform.position = new Vector3(pos.x, (pos.y - shape.y / 2) + this.Hole.shape.y / 2, pos.z + this.Hole.pos.x);
                //BuildingInfo.Instance.Doors.Add(thedoor.transform);
                //Vector3 dir = thedoor.transform.position - this.RoomPos;


                
                door0.transform.localScale = new Vector3(this.Hole.shape.x, this.shape.y - this.shape.z - this.Hole.shape.y, this.shape.z / 2);         //大小缩放
                _door0.transform.localScale = new Vector3(this.Hole.shape.x, this.shape.y - this.Hole.shape.y, this.shape.z / 2);



                //door0.transform.position = new Vector3(pos.x, pos.y + this.Hole.shape.y / 2, pos.z + this.Hole.pos.x);         //计算门框位置
                //_door0.transform.localScale = new 

                door1.transform.localScale = new Vector3(Math.Abs(((pos.z - (shape.x / 2)) - ((this.pos.z + this.Hole.pos.x) - (this.Hole.shape.x / 2)))) - shape.z,
                    shape.y - shape.z * 2, shape.z / 2);
                _door1.transform.localScale = new Vector3(Math.Abs(((pos.z - (shape.x / 2)) - ((this.pos.z + this.Hole.pos.x) - (this.Hole.shape.x / 2)))), 
                    shape.y, shape.z / 2);


                //door1.transform.position = new Vector3(pos.x, pos.y, (pos.z - (shape.x / 2))
                //+ (((this.pos.z + this.Hole.pos.x) - (this.Hole.shape.x / 2)) - (pos.z - (shape.x / 2))) / 2);

                door2.transform.localScale = new Vector3(Math.Abs(((pos.z + (shape.x / 2)) - ((this.pos.z + this.Hole.pos.x) + (this.Hole.shape.x / 2)))) - shape.z,
                    shape.y - shape.z * 2, shape.z / 2);

                _door2.transform.localScale = new Vector3(Math.Abs(((pos.z + (shape.x / 2)) - ((this.pos.z + this.Hole.pos.x) + (this.Hole.shape.x / 2)))),
                    shape.y, shape.z / 2);

                //door2.transform.position = new Vector3(pos.x,  pos.y, (pos.z + (shape.x / 2))
                //    - ((pos.z + (shape.x / 2)) - ((this.pos.z + this.Hole.pos.x) + (this.Hole.shape.x / 2))) / 2);

                if (dir.x < 0)
                {
                    wall.transform.localEulerAngles = new Vector3(0, 90, 0);
                    _wall.transform.localEulerAngles = new Vector3(0, -90, 0);

                    door0.transform.localEulerAngles = new Vector3(0, 90, 0);
                    _door0.transform.localEulerAngles = new Vector3(0, -90, 0);
                    door1.transform.localEulerAngles = new Vector3(0, 90, 0);
                    _door1.transform.localEulerAngles = new Vector3(0, -90, 0);
                    door2.transform.localEulerAngles = new Vector3(0, 90, 0);
                    _door2.transform.localEulerAngles = new Vector3(0, -90, 0);
                    thedoor.transform.localEulerAngles = new Vector3(0, 90, 0);

                    wall.transform.position = new Vector3(pos.x + shape.z / 4, pos.y, pos.z);
                    _wall.transform.position = new Vector3(pos.x - shape.z / 4, pos.y, pos.z);

                    _door0.transform.position = new Vector3(pos.x - shape.z / 4, pos.y + this.Hole.shape.y / 2, pos.z + this.Hole.pos.x);         //计算门框位置
                    door0.transform.position = new Vector3(pos.x + shape.z / 4, pos.y + this.Hole.shape.y / 2 - shape.z / 2, pos.z + this.Hole.pos.x);         //计算门框位置


                    door1.transform.position = new Vector3(pos.x + shape.z / 4, pos.y, (pos.z - (shape.x / 2))
                        + (((this.pos.z + this.Hole.pos.x) - (this.Hole.shape.x / 2)) - (pos.z - (shape.x / 2))) / 2 + shape.z / 2);

                    _door1.transform.position = new Vector3(pos.x - shape.z / 4, pos.y, (pos.z - (shape.x / 2))
                        + (((this.pos.z + this.Hole.pos.x) - (this.Hole.shape.x / 2)) - (pos.z - (shape.x / 2))) / 2);



                    door2.transform.position = new Vector3(pos.x + shape.z / 4,  pos.y, (pos.z + (shape.x / 2))
                        - ((pos.z + (shape.x / 2)) - ((this.pos.z + this.Hole.pos.x) + (this.Hole.shape.x / 2))) / 2 - shape.z / 2);
                    _door2.transform.position = new Vector3(pos.x - shape.z / 4,  pos.y, (pos.z + (shape.x / 2))
                        - ((pos.z + (shape.x / 2)) - ((this.pos.z + this.Hole.pos.x) + (this.Hole.shape.x / 2))) / 2);
                }
                else if (dir.x > 0)
                {
                    wall.transform.localEulerAngles = new Vector3(0, -90, 0);
                    _wall.transform.localEulerAngles = new Vector3(0, 90, 0);

                    thedoor.transform.localEulerAngles = new Vector3(0, -90, 0);
                    door0.transform.localEulerAngles = new Vector3(0, -90, 0);
                    door1.transform.localEulerAngles = new Vector3(0, -90, 0);
                    door2.transform.localEulerAngles = new Vector3(0, -90, 0);
                    _door0.transform.localEulerAngles = new Vector3(0, 90, 0);
                    _door1.transform.localEulerAngles = new Vector3(0, 90, 0);
                    _door2.transform.localEulerAngles = new Vector3(0, 90, 0);

                    wall.transform.position = new Vector3(pos.x - shape.z / 4, pos.y, pos.z);
                    _wall.transform.position = new Vector3(pos.x + shape.z / 4, pos.y, pos.z);

                    _door0.transform.position = new Vector3(pos.x + shape.z / 4, pos.y + this.Hole.shape.y / 2, pos.z + this.Hole.pos.x);         //计算门框位置
                    door0.transform.position = new Vector3(pos.x - shape.z / 4, pos.y + this.Hole.shape.y / 2 - shape.z / 2, pos.z + this.Hole.pos.x);         //计算门框位置


                    door1.transform.position = new Vector3(pos.x - shape.z / 4, pos.y, (pos.z - (shape.x / 2))
                        + (((this.pos.z + this.Hole.pos.x) - (this.Hole.shape.x / 2)) - (pos.z - (shape.x / 2))) / 2 + shape.z / 2);

                    _door1.transform.position = new Vector3(pos.x + shape.z / 4, pos.y, (pos.z - (shape.x / 2))
                        + (((this.pos.z + this.Hole.pos.x) - (this.Hole.shape.x / 2)) - (pos.z - (shape.x / 2))) / 2);



                    door2.transform.position = new Vector3(pos.x - shape.z / 4,  pos.y, (pos.z + (shape.x / 2))
                        - ((pos.z + (shape.x / 2)) - ((this.pos.z + this.Hole.pos.x) + (this.Hole.shape.x / 2))) / 2 - shape.z / 2);
                    _door2.transform.position = new Vector3(pos.x + shape.z / 4,  pos.y, (pos.z + (shape.x / 2))
                        - ((pos.z + (shape.x / 2)) - ((this.pos.z + this.Hole.pos.x) + (this.Hole.shape.x / 2))) / 2);

                }

            }
            else if (wallNum % 2 == 0)
            {

                
                thedoor.transform.position = new Vector3(pos.x + this.Hole.pos.x, (pos.y - shape.y / 2) + this.Hole.shape.y / 2, pos.z);
                //Vector3 dir = thedoor.transform.position - this.RoomPos;


                //door0.transform.position = new Vector3(pos.x + this.Hole.pos.x, pos.y + this.Hole.shape.y / 2, pos.z);
                door0.transform.localScale = new Vector3(this.Hole.shape.x, this.shape.y - this.Hole.shape.y - shape.z, shape.z / 2);
                _door0.transform.localScale = new Vector3(this.Hole.shape.x, this.shape.y - this.Hole.shape.y, shape.z / 2);


                door1.transform.localScale = new Vector3(Math.Abs(((pos.x - (shape.x / 2)) 
                    - ((this.pos.x + this.Hole.pos.x) - (this.Hole.shape.x / 2)))) - shape.z, shape.y - shape.z * 2, shape.z / 2);
                _door1.transform.localScale = new Vector3(Math.Abs(((pos.x - (shape.x / 2))
                    - ((this.pos.x + this.Hole.pos.x) - (this.Hole.shape.x / 2)))), shape.y, shape.z / 2);

                door2.transform.localScale = new Vector3(Math.Abs(((pos.x + (shape.x / 2))
                    - ((this.pos.x + this.Hole.pos.x) + (this.Hole.shape.x / 2)))) - shape.z, shape.y - shape.z * 2, shape.z / 2);
                _door2.transform.localScale = new Vector3(Math.Abs(((pos.x + (shape.x / 2)) 
                    - ((this.pos.x + this.Hole.pos.x) + (this.Hole.shape.x / 2)))), shape.y, shape.z / 2);

                if (dir.z < 0)
                {
                    _wall.transform.localEulerAngles = new Vector3(0, 180, 0);

                    _door0.transform.localEulerAngles = new Vector3(0, 180, 0);
                    _door1.transform.localEulerAngles = new Vector3(0, 180, 0);
                    _door2.transform.localEulerAngles = new Vector3(0, 180, 0);

                    wall.transform.position = new Vector3(pos.x, pos.y, pos.z + shape.z / 4);
                    _wall.transform.position = new Vector3(pos.x, pos.y, pos.z - shape.z / 4);

                    door0.transform.position = new Vector3(pos.x + this.Hole.pos.x, pos.y + this.Hole.shape.y / 2 - shape.z / 2, pos.z + shape.z / 4);

                    door1.transform.position = new Vector3((pos.x - (shape.x / 2)) +
                    (((this.pos.x + this.Hole.pos.x) - (this.Hole.shape.x / 2)) - (pos.x - (shape.x / 2))) / 2 + shape.z / 2, pos.y, pos.z + shape.z / 4);

                    door2.transform.position = new Vector3((pos.x + (shape.x / 2)) -
                    ((pos.x + (shape.x / 2)) - ((this.pos.x + this.Hole.pos.x) + (this.Hole.shape.x / 2))) / 2 - shape.z / 2, pos.y, pos.z + shape.z / 4);

                    _door0.transform.position = new Vector3(pos.x + this.Hole.pos.x, pos.y + this.Hole.shape.y / 2, pos.z - shape.z / 4);

                    _door1.transform.position = new Vector3((pos.x - (shape.x / 2)) +
                    (((this.pos.x + this.Hole.pos.x) - (this.Hole.shape.x / 2)) - (pos.x - (shape.x / 2))) / 2, pos.y, pos.z - shape.z / 4);

                    _door2.transform.position = new Vector3((pos.x + (shape.x / 2)) -
                    ((pos.x + (shape.x / 2)) - ((this.pos.x + this.Hole.pos.x) + (this.Hole.shape.x / 2))) / 2, pos.y, pos.z - shape.z / 4);


                }
                else if (dir.z > 0)
                {
                    wall.transform.localEulerAngles = new Vector3(0, 180, 0);
                    

                    thedoor.transform.localEulerAngles = new Vector3(0, 180, 0);
                    door0.transform.localEulerAngles = new Vector3(0, 180, 0);
                    door1.transform.localEulerAngles = new Vector3(0, 180, 0);
                    door2.transform.localEulerAngles = new Vector3(0, 180, 0);

                    wall.transform.position = new Vector3(pos.x, pos.y, pos.z - shape.z / 4);
                    _wall.transform.position = new Vector3(pos.x, pos.y, pos.z + shape.z / 4);

                    _door0.transform.position = new Vector3(pos.x + this.Hole.pos.x, pos.y + this.Hole.shape.y / 2, pos.z + shape.z / 4);

                    _door1.transform.position = new Vector3((pos.x - (shape.x / 2)) +
                    (((this.pos.x + this.Hole.pos.x) - (this.Hole.shape.x / 2)) - (pos.x - (shape.x / 2))) / 2, pos.y, pos.z + shape.z / 4);

                    _door2.transform.position = new Vector3((pos.x + (shape.x / 2)) -
                    ((pos.x + (shape.x / 2)) - ((this.pos.x + this.Hole.pos.x) + (this.Hole.shape.x / 2))) / 2, pos.y, pos.z + shape.z / 4);

                    door0.transform.position = new Vector3(pos.x + this.Hole.pos.x, pos.y + this.Hole.shape.y / 2 - shape.z / 2, pos.z - shape.z / 4);

                    door1.transform.position = new Vector3((pos.x - (shape.x / 2)) +
                    (((this.pos.x + this.Hole.pos.x) - (this.Hole.shape.x / 2)) - (pos.x - (shape.x / 2))) / 2 + shape.z / 2, pos.y, pos.z - shape.z / 4);

                    door2.transform.position = new Vector3((pos.x + (shape.x / 2)) -
                    ((pos.x + (shape.x / 2)) - ((this.pos.x + this.Hole.pos.x) + (this.Hole.shape.x / 2))) / 2 - shape.z / 2, pos.y, pos.z - shape.z / 4);
                }
            }

            BuildingInfo.Instance.Doors.Add(thedoor.transform);
            thedoor.transform.parent = Parent.transform;

            door0.transform.parent = wall.transform;
            door1.transform.parent = wall.transform;
            door2.transform.parent = wall.transform;
            _door0.transform.parent = _wall.transform;
            _door1.transform.parent = _wall.transform;
            _door2.transform.parent = _wall.transform;
            wall.transform.parent = Parent.transform;
            _wall.transform.parent = Parent.transform;
            BuildingInfo.Instance.Walls.Add(wall.transform);
            BuildingInfo.Instance._Walls.Add(_wall.transform);
        }
        else if(this.Hole.Type == HoleType.Windows)
        {
            this.Hole.shape.init(new Vector2(1.0f, 0.8f));

            GameObject wall = new GameObject(wallName);
            GameObject _wall = new GameObject("_" + wallName);

            GameObject theWindow = new GameObject("window" + wallNum);

            GameObject window0 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            window0.name = "window0";
            GameObject _window0 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _window0.name = "_window0";
            GameObject window1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            window1.name = "window1";
            GameObject _window1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _window1.name = "_window1";
            GameObject window2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            window2.name = "window2";
            GameObject _window2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _window2.name = "_window2";
            GameObject window3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            window3.name = "window3";
            GameObject _window3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _window3.name = "_window3";

            if (wallNum % 2 != 0)
            {
                
                theWindow.transform.position = new Vector3(pos.x, (pos.y - shape.y / 2) + this.Hole.pos.y, pos.z + this.Hole.pos.x);
                //Vector3 dir = theWindow.transform.position - this.RoomPos;
              
                _window0.transform.localScale = new Vector3(this.Hole.shape.x,
                     Math.Abs(((this.pos.y - this.shape.y / 2) + this.Hole.pos.y - this.Hole.shape.y / 2) - (pos.y - shape.y / 2)), shape.z / 2);

                window0.transform.localScale = new Vector3(this.Hole.shape.x,
                     Math.Abs(((this.pos.y - this.shape.y / 2) + this.Hole.pos.y - this.Hole.shape.y / 2) - (pos.y - shape.y / 2)) - shape.z, shape.z / 2);

                _window1.transform.localScale = new Vector3(this.Hole.shape.x,
                    Math.Abs((pos.y + shape.y / 2) - ((this.pos.y - this.shape.y / 2) + this.Hole.pos.y + this.Hole.shape.y / 2)), shape.z / 2);

                window1.transform.localScale = new Vector3(this.Hole.shape.x,
                    Math.Abs((pos.y + shape.y / 2) - ((this.pos.y - this.shape.y / 2) + this.Hole.pos.y + this.Hole.shape.y / 2)) - shape.z, shape.z / 2);

                _window2.transform.localScale = new Vector3(Math.Abs(((pos.x - (shape.x / 2))
                    - ((this.pos.x + this.Hole.pos.x) - (this.Hole.shape.x / 2)))), shape.y, shape.z / 2);

                window2.transform.localScale = new Vector3(Math.Abs(((pos.x - (shape.x / 2))
                    - ((this.pos.x + this.Hole.pos.x) - (this.Hole.shape.x / 2)))) - shape.z, shape.y - shape.z * 2, shape.z / 2);

                _window3.transform.localScale = new Vector3(Math.Abs(((pos.x + (shape.x / 2))
                    - ((this.pos.x + this.Hole.pos.x) + (this.Hole.shape.x / 2)))), shape.y, shape.z / 2);

                window3.transform.localScale = new Vector3(Math.Abs(((pos.x + (shape.x / 2))
                    - ((this.pos.x + this.Hole.pos.x) + (this.Hole.shape.x / 2)))) - shape.z, shape.y - shape.z * 2, shape.z / 2);

                if (dir.x < 0)
                {
                    theWindow.transform.localEulerAngles = new Vector3(0, 90, 0);
                    _wall.transform.localEulerAngles = new Vector3(0, -90, 0);
                    wall.transform.localEulerAngles = new Vector3(0, 90, 0);

                    window0.transform.localEulerAngles = new Vector3(0, 90, 0);
                    //_window0.transform.localEulerAngles = new Vector3(0, 90, 0);
                    window1.transform.localEulerAngles = new Vector3(0, 90, 0);
                    window2.transform.localEulerAngles = new Vector3(0, 90, 0);
                    window3.transform.localEulerAngles = new Vector3(0, 90, 0);
                    _window0.transform.localEulerAngles = new Vector3(0, -90, 0);
                    _window1.transform.localEulerAngles = new Vector3(0, -90, 0);
                    _window2.transform.localEulerAngles = new Vector3(0, -90, 0);
                    _window3.transform.localEulerAngles = new Vector3(0, -90, 0);

                    wall.transform.position = new Vector3(pos.x + shape.z / 4, pos.y, pos.z);
                    _wall.transform.position = new Vector3(pos.x - shape.z / 4, pos.y, pos.z);

                    _window0.transform.position = new Vector3(pos.x - shape.z / 4, (pos.y - shape.y / 2) +
                    (((this.pos.y - this.shape.y / 2) + this.Hole.pos.y - this.Hole.shape.y / 2) - (pos.y - shape.y / 2)) / 2, pos.z + this.Hole.pos.x);

                    window0.transform.position = new Vector3(pos.x + shape.z / 4, (pos.y - shape.y / 2) +
                    (((this.pos.y - this.shape.y / 2) + this.Hole.pos.y - this.Hole.shape.y / 2) - (pos.y - shape.y / 2)) / 2 + shape.z / 2, pos.z + this.Hole.pos.x);

                    _window1.transform.position = new Vector3(pos.x - shape.z / 4, (pos.y + shape.y / 2) -
                    ((pos.y + shape.y / 2) - ((this.pos.y - this.shape.y / 2) + this.Hole.pos.y + this.Hole.shape.y / 2)) / 2, pos.z + this.Hole.pos.x);

                    window1.transform.position = new Vector3(pos.x + shape.z / 4, (pos.y + shape.y / 2) -
                    ((pos.y + shape.y / 2) - ((this.pos.y - this.shape.y / 2) + this.Hole.pos.y + this.Hole.shape.y / 2)) / 2 - shape.z / 2, pos.z + this.Hole.pos.x);

                    _window2.transform.position = new Vector3(pos.x - shape.z / 4, pos.y, (pos.z - (shape.x / 2))
                    + (((this.pos.z + this.Hole.pos.x) - (this.Hole.shape.x / 2)) - (pos.z - (shape.x / 2))) / 2);

                    window2.transform.position = new Vector3(pos.x + shape.z / 4, pos.y, (pos.z - (shape.x / 2))
                    + (((this.pos.z + this.Hole.pos.x) - (this.Hole.shape.x / 2)) - (pos.z - (shape.x / 2))) / 2 + shape.z / 2);

                    _window3.transform.position = new Vector3(pos.x - shape.z / 4, pos.y, (pos.z + (shape.x / 2))
                    - ((pos.z + (shape.x / 2)) - ((this.pos.z + this.Hole.pos.x) + (this.Hole.shape.x / 2))) / 2);

                    window3.transform.position = new Vector3(pos.x + shape.z / 4, pos.y, (pos.z + (shape.x / 2))
                   - ((pos.z + (shape.x / 2)) - ((this.pos.z + this.Hole.pos.x) + (this.Hole.shape.x / 2))) / 2 - shape.z / 2);
                }
                else if (dir.x > 0)
                {
                    theWindow.transform.localEulerAngles = new Vector3(0, -90, 0);
                    wall.transform.localEulerAngles = new Vector3(0, -90, 0);
                    _wall.transform.localEulerAngles = new Vector3(0, 90, 0);
                    window0.transform.localEulerAngles = new Vector3(0, -90, 0);
                    _window0.transform.localEulerAngles = new Vector3(0, 90, 0);
                    window1.transform.localEulerAngles = new Vector3(0, -90, 0);
                    window2.transform.localEulerAngles = new Vector3(0, -90, 0);
                    window3.transform.localEulerAngles = new Vector3(0, -90, 0);
                    _window1.transform.localEulerAngles = new Vector3(0, 90, 0);
                    _window2.transform.localEulerAngles = new Vector3(0, 90, 0);
                    _window3.transform.localEulerAngles = new Vector3(0, 90, 0);

                    wall.transform.position = new Vector3(pos.x - shape.z / 4, pos.y, pos.z);
                    _wall.transform.position = new Vector3(pos.x + shape.z / 4, pos.y, pos.z);

                    _window0.transform.position = new Vector3(pos.x + shape.z / 4, (pos.y - shape.y / 2) +
                    (((this.pos.y - this.shape.y / 2) + this.Hole.pos.y - this.Hole.shape.y / 2) - (pos.y - shape.y / 2)) / 2, pos.z + this.Hole.pos.x);

                    window0.transform.position = new Vector3(pos.x - shape.z / 4, (pos.y - shape.y / 2) +
                    (((this.pos.y - this.shape.y / 2) + this.Hole.pos.y - this.Hole.shape.y / 2) - (pos.y - shape.y / 2)) / 2 + shape.z / 2, pos.z + this.Hole.pos.x);

                    _window1.transform.position = new Vector3(pos.x + shape.z / 4, (pos.y + shape.y / 2) -
                    ((pos.y + shape.y / 2) - ((this.pos.y - this.shape.y / 2) + this.Hole.pos.y + this.Hole.shape.y / 2)) / 2, pos.z + this.Hole.pos.x);

                    window1.transform.position = new Vector3(pos.x - shape.z / 4, (pos.y + shape.y / 2) -
                    ((pos.y + shape.y / 2) - ((this.pos.y - this.shape.y / 2) + this.Hole.pos.y + this.Hole.shape.y / 2)) / 2 - shape.z / 2, pos.z + this.Hole.pos.x);

                    _window2.transform.position = new Vector3(pos.x + shape.z / 4, pos.y, (pos.z - (shape.x / 2))
                    + (((this.pos.z + this.Hole.pos.x) - (this.Hole.shape.x / 2)) - (pos.z - (shape.x / 2))) / 2);

                    window2.transform.position = new Vector3(pos.x - shape.z / 4, pos.y, (pos.z - (shape.x / 2))
                    + (((this.pos.z + this.Hole.pos.x) - (this.Hole.shape.x / 2)) - (pos.z - (shape.x / 2))) / 2 + shape.z / 2);

                    _window3.transform.position = new Vector3(pos.x + shape.z / 4, pos.y, (pos.z + (shape.x / 2))
                    - ((pos.z + (shape.x / 2)) - ((this.pos.z + this.Hole.pos.x) + (this.Hole.shape.x / 2))) / 2);

                    window3.transform.position = new Vector3(pos.x - shape.z / 4, pos.y, (pos.z + (shape.x / 2))
                    - ((pos.z + (shape.x / 2)) - ((this.pos.z + this.Hole.pos.x) + (this.Hole.shape.x / 2))) / 2 - shape.z / 2);
                }
            }
            else if(wallNum % 2 == 0)
            {


                
                theWindow.transform.position = new Vector3(pos.x + this.Hole.pos.x, (pos.y - shape.y / 2) + this.Hole.pos.y, pos.z);
              

                window0.transform.localScale = new Vector3(this.Hole.shape.x,
                    Math.Abs(((this.pos.y - this.shape.y / 2) + this.Hole.pos.y - this.Hole.shape.y / 2) - (pos.y - shape.y / 2)) - shape.z, shape.z / 2);

                _window0.transform.localScale = new Vector3(this.Hole.shape.x,
                    Math.Abs(((this.pos.y - this.shape.y / 2) + this.Hole.pos.y - this.Hole.shape.y / 2) - (pos.y - shape.y / 2)), shape.z / 2);

                window1.transform.localScale = new Vector3(this.Hole.shape.x, 
                    Math.Abs((pos.y + shape.y / 2) - ((this.pos.y - this.shape.y / 2) + this.Hole.pos.y + this.Hole.shape.y / 2)) - shape.z, shape.z / 2);

                _window1.transform.localScale = new Vector3(this.Hole.shape.x,
                    Math.Abs((pos.y + shape.y / 2) - ((this.pos.y - this.shape.y / 2) + this.Hole.pos.y + this.Hole.shape.y / 2)), shape.z / 2);

                window2.transform.localScale = new Vector3(Math.Abs(((pos.x - (shape.x / 2))
                    - ((this.pos.x + this.Hole.pos.x) - (this.Hole.shape.x / 2)))) - shape.z, shape.y - shape.z * 2, shape.z / 2);

                _window2.transform.localScale = new Vector3(Math.Abs(((pos.x - (shape.x / 2))
                    - ((this.pos.x + this.Hole.pos.x) - (this.Hole.shape.x / 2)))), shape.y, shape.z / 2);

                window3.transform.localScale = new Vector3(Math.Abs(((pos.x + (shape.x / 2))
                    - ((this.pos.x + this.Hole.pos.x) + (this.Hole.shape.x / 2)))) - shape.z, shape.y - shape.z * 2, shape.z / 2);

                _window3.transform.localScale = new Vector3(Math.Abs(((pos.x + (shape.x / 2))
                    - ((this.pos.x + this.Hole.pos.x) + (this.Hole.shape.x / 2)))), shape.y, shape.z / 2);

                if (dir.z < 0)
                {
                    _window0.transform.localEulerAngles = new Vector3(0, 180, 0);
                    _window1.transform.localEulerAngles = new Vector3(0, 180, 0);
                    _window2.transform.localEulerAngles = new Vector3(0, 180, 0);
                    _window3.transform.localEulerAngles = new Vector3(0, 180, 0);
                    _wall.transform.localEulerAngles = new Vector3(0, 180, 0);

                    wall.transform.position = new Vector3(this.pos.x, this.pos.y, this.pos.z + shape.z / 4);
                    _wall.transform.position = new Vector3(pos.x, pos.y, pos.z - shape.z / 4);

                    window0.transform.position = new Vector3(pos.x + this.Hole.pos.x, (pos.y - shape.y / 2) +
                    (((this.pos.y - this.shape.y / 2) + this.Hole.pos.y - this.Hole.shape.y / 2) - (pos.y - shape.y / 2)) / 2 + shape.z / 2, pos.z + shape.z / 4);

                    window1.transform.position = new Vector3(pos.x + this.Hole.pos.x, (pos.y + shape.y / 2) -
                    ((pos.y + shape.y / 2) - ((this.pos.y - this.shape.y / 2) + this.Hole.pos.y + this.Hole.shape.y / 2)) / 2 - shape.z / 2, pos.z + shape.z / 4);

                    window2.transform.position = new Vector3((pos.x - (shape.x / 2)) +
                   (((this.pos.x + this.Hole.pos.x) - (this.Hole.shape.x / 2)) - (pos.x - (shape.x / 2))) / 2 + shape.z / 2, pos.y, pos.z + shape.z / 4);

                    window3.transform.position = new Vector3((pos.x + (shape.x / 2)) -
                    ((pos.x + (shape.x / 2)) - ((this.pos.x + this.Hole.pos.x) + (this.Hole.shape.x / 2))) / 2 - shape.z / 2, pos.y, pos.z + shape.z / 4);

                    _window0.transform.position = new Vector3(pos.x + this.Hole.pos.x, (pos.y - shape.y / 2) +
                    (((this.pos.y - this.shape.y / 2) + this.Hole.pos.y - this.Hole.shape.y / 2) - (pos.y - shape.y / 2)) / 2, pos.z - shape.z / 4);

                    _window1.transform.position = new Vector3(pos.x + this.Hole.pos.x, (pos.y + shape.y / 2) -
                    ((pos.y + shape.y / 2) - ((this.pos.y - this.shape.y / 2) + this.Hole.pos.y + this.Hole.shape.y / 2)) / 2, pos.z - shape.z / 4);

                    _window2.transform.position = new Vector3((pos.x - (shape.x / 2)) +
                   (((this.pos.x + this.Hole.pos.x) - (this.Hole.shape.x / 2)) - (pos.x - (shape.x / 2))) / 2, pos.y, pos.z - shape.z / 4);

                    _window3.transform.position = new Vector3((pos.x + (shape.x / 2)) -
                    ((pos.x + (shape.x / 2)) - ((this.pos.x + this.Hole.pos.x) + (this.Hole.shape.x / 2))) / 2, pos.y, pos.z - shape.z / 4);
                }
                else if (dir.z > 0)
                {
                    theWindow.transform.localEulerAngles = new Vector3(0, 180, 0);
                    window0.transform.localEulerAngles = new Vector3(0, 180, 0);
                    window1.transform.localEulerAngles = new Vector3(0, 180, 0);
                    window2.transform.localEulerAngles = new Vector3(0, 180, 0);
                    window3.transform.localEulerAngles = new Vector3(0, 180, 0);
                    

                    wall.transform.localEulerAngles = new Vector3(0, 180, 0);
                    

                    _wall.transform.position = new Vector3(this.pos.x, this.pos.y, this.pos.z + shape.z / 4);
                    wall.transform.position = new Vector3(pos.x, pos.y, pos.z - shape.z / 4);

                    window0.transform.position = new Vector3(pos.x + this.Hole.pos.x, (pos.y - shape.y / 2) +
                    (((this.pos.y - this.shape.y / 2) + this.Hole.pos.y - this.Hole.shape.y / 2) - (pos.y - shape.y / 2)) / 2 + shape.z / 2, pos.z - shape.z / 4);

                    _window0.transform.position = new Vector3(pos.x + this.Hole.pos.x, (pos.y - shape.y / 2) +
                    (((this.pos.y - this.shape.y / 2) + this.Hole.pos.y - this.Hole.shape.y / 2) - (pos.y - shape.y / 2)) / 2, pos.z + shape.z / 4);

                    window1.transform.position = new Vector3(pos.x + this.Hole.pos.x, (pos.y + shape.y / 2) -
                    ((pos.y + shape.y / 2) - ((this.pos.y - this.shape.y / 2) + this.Hole.pos.y + this.Hole.shape.y / 2)) / 2 - shape.z / 2, pos.z - shape.z / 4);

                    _window1.transform.position = new Vector3(pos.x + this.Hole.pos.x, (pos.y + shape.y / 2) -
                    ((pos.y + shape.y / 2) - ((this.pos.y - this.shape.y / 2) + this.Hole.pos.y + this.Hole.shape.y / 2)) / 2, pos.z + shape.z / 4);

                    window2.transform.position = new Vector3((pos.x - (shape.x / 2)) +
                   (((this.pos.x + this.Hole.pos.x) - (this.Hole.shape.x / 2)) - (pos.x - (shape.x / 2))) / 2 + shape.z / 2, pos.y, pos.z - shape.z / 4);

                    _window2.transform.position = new Vector3((pos.x - (shape.x / 2)) +
                   (((this.pos.x + this.Hole.pos.x) - (this.Hole.shape.x / 2)) - (pos.x - (shape.x / 2))) / 2, pos.y, pos.z + shape.z / 4);

                    window3.transform.position = new Vector3((pos.x + (shape.x / 2)) -
                    ((pos.x + (shape.x / 2)) - ((this.pos.x + this.Hole.pos.x) + (this.Hole.shape.x / 2))) / 2 - shape.z / 2, pos.y, pos.z - shape.z / 4);

                    _window3.transform.position = new Vector3((pos.x + (shape.x / 2)) -
                    ((pos.x + (shape.x / 2)) - ((this.pos.x + this.Hole.pos.x) + (this.Hole.shape.x / 2))) / 2, pos.y, pos.z + shape.z / 4);
                }
            }

            BuildingInfo.Instance.Windows.Add(theWindow.transform);
            theWindow.transform.parent = Parent.transform;

            window0.transform.parent = wall.transform;
            window1.transform.parent = wall.transform;
            window2.transform.parent = wall.transform;
            window3.transform.parent = wall.transform;

            _window0.transform.parent = _wall.transform;
            _window1.transform.parent = _wall.transform;
            _window2.transform.parent = _wall.transform;
            _window3.transform.parent = _wall.transform;

            _wall.transform.parent = Parent.transform;
            wall.transform.parent = Parent.transform;
            BuildingInfo.Instance.Walls.Add(wall.transform);
            BuildingInfo.Instance._Walls.Add(_wall.transform);
        }

    }

    public void SetHole(HoleType type, Vector2 shape, Vector2 pos)
    {
        if ((HoleType)type == HoleType.Door)
        {
            this.Hole = new door(wallNum, shape, pos.x);
        }
        else if ((HoleType)type == HoleType.Windows)
        { 
            this.Hole = new windows(wallNum, shape, pos);
        }
    }

    public void SetHole(HoleType type, Vector2 pos)
    {
        if ((HoleType)type == HoleType.Door)
        {
            this.Hole = new door(wallNum, pos.x);
        }
        else if ((HoleType)type == HoleType.Windows)
        {
            this.Hole = new windows(wallNum, pos);
        }
    }
}

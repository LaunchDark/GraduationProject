using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

[System.Serializable]
public struct Vector2Serializer
{
    public float x;
    public float y;
    

    public void init(Vector2 v2)
    {
        x = v2.x;
        y = v2.y;
      
    }

    public Vector2 V2
    { get { return new Vector2(x, y); } }
}

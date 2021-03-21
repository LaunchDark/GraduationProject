using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleInfo : MonoBehaviour
{
    public Transform transform;
    public Vector2 dir;

    public void setInfo(Transform transform, Vector2 dir)
    {
        this.transform = transform;
        this.dir = dir;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PacksackItemData
{
    public int id;
    public int Type;
    public int num;
    public string name;
    public string icon;

    public Func<PacksackItemData, bool, GameObject> callback;

    public bool isAtPacksack = true;
}

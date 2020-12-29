using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PacksackItemData
{
    public int id;
    public int num;
    public string name;
    public string icon;
    public bool isGroupInstrument = false;
    public List<InstrumentEnum> instrumentEnumList = new List<InstrumentEnum>();

    public Func<PacksackItemData, bool, GameObject> callback;

    public bool isAtPacksack = true;

    public string key = "";

    public PacksackItemData(int _id, string _name, string _icon, int _num, string _key = "", Func<PacksackItemData, bool, GameObject> _callback = null, bool _isAtPacksack = true)
    {
        id = _id;
        num = _num;
        name = _name;
        icon = _icon;
        callback = _callback;
        isAtPacksack = _isAtPacksack;
        key = _key;
    }
}

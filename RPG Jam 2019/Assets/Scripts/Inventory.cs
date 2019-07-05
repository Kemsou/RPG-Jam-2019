using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Inventory 
{
    public List<IItem> content {get;set;}
    public int capacity {get;set;}
}

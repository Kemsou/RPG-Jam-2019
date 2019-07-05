using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Consomable : IItem
{
    #region Item

    public string name { get; set; }
    public string decs { get; set; }
    public int price { get; set; }
    public int lvl { get; set; }

    #endregion
}

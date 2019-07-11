using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GamesStates
{
    public string FileName { get; set; }

    public Hashtable entities { get; set; }

    public GamesStates()
    {

        entities = new Hashtable();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GamesStates
{
    public string FileName { get; set; }
    public Dictionary<string, int> all;

    // public Hashtable entities { get; set; }

    //public List<Character> characters { get; set; }

    public float overWorldPositionX { get; set; }
    public float overWorldPositionY { get; set; }
    public float overWorldPositionZ { get; set; }
    public GamesStates()
    {

        //  entities = new Hashtable();
        //characters = new List<Character>();
    }

}

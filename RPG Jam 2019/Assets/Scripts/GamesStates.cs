using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GamesStates
{
    public string FileName { get; set; }

    public List<Character> entitys { get; set; }

    public GamesStates()
    {

        entitys = new List<Character>();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Character : IEntity
{

    #region Entity 
    public string name { get; set; }
    public string desc { get; set; }
    public int life { get; set; }
    public int strength { get; set; }
    public int dexterity { get; set; }
    public int intellect { get; set; }
    public int accuracy { get; set; }

    #endregion

    #region  Character
    public Character(String name)
    {
        this.name = name;
    }
    public int lvl { get; set; }
    public Inventory inventory { get; set; }

    public Suitable Armor { get; set; }

    public Weapon weapon { get; set; }


    #endregion

}

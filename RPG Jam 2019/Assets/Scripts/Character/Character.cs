using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    public new string name;
    public string desc;
    public int life;
    public int strength;
    public int dexterity;
    public int intellect;
    public int accuracy;
    public int lvl;

    public Inventory inventory { get; set; }

    public Armor Armor { get; set; }

    public Weapon weapon { get; set; }

}

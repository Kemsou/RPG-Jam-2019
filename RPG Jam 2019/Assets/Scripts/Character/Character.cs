using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    public new string name;
    public string desc;
    public int lvl;

    public int maxHealth;
    public int currentHealth;

    public int maxMana;
    public int currentMana;

    public int strength;
    public int dexterity;
    public int intellect;
    public int accuracy;

    public List<Item> inventory { get; set; }
    public Armor armor { get; set; }
    public Weapon weapon { get; set; }
    public int gold;

}

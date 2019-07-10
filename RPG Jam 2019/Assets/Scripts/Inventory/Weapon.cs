using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public new string name;
    public string desc;
    public int price;
    public int lvl;

    public int baseDamage;

    public int lifeBonus;
    public int strengthBonus;
    public int dexterityBonus;
    public int intellectBonus;
    public int accuracyBonus;

    public int strengthModificator;
    public int dexterityModificator;
    public int intellectModificator;
    public int accuracyModificator;

    public int defense;
}

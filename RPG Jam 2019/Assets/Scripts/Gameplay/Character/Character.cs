using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
[System.Serializable]
public class Character : ScriptableObject
{
    public new string name;
    public string desc;
    public int lvl;
    public bool isAlly = false;

    //public int maxHealth; 
    public int currentHealth; //Max Health can be calculated with caracteristics

    //public int maxMana;
    public int currentMana; //Max Mana can be calculated with caracteristics

    public int strength;
    public int dexterity;
    public int intellect;
    public int accuracy;
    public int speed;

    public List<Item> inventory;
    public Armor armor;
    public Weapon weapon;
    public int gold;

    public int getTotalCharacWithoutBuff(Characteristic charac) {
        int ret = 0;
        switch (charac) {
            case Characteristic.Speed:
                ret += speed;
                break;
            case Characteristic.Strength:
                ret += strength;
                break;
            case Characteristic.Intelligence:
                ret += intellect;
                break;
            case Characteristic.Dexterity:
                ret += dexterity;
                break;
            case Characteristic.Accuracy:
                ret += accuracy;
                break;
            default:
                Debug.Log("ERROR: " + charac.ToString() + " doesn't have a line in getTotalCharacWithoutBuff() method");
                break;
        }
        foreach(CharacBonus bonus in armor.characBonus) {
            if (bonus.charac == charac)
                ret += bonus.bonus;
        }
        foreach (CharacBonus bonus in weapon.characBonus) {
            if (bonus.charac == charac)
                ret += bonus.bonus;
        }
        return ret < 0 ? 0 : ret;
    }

    //TODO: How to calculate maxHealth ?
    public int getMaxHealth() {
        return 100 + strength * 2;
    }
}

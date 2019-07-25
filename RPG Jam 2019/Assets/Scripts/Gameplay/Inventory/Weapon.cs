using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
[System.Serializable]
public class Weapon : ScriptableObject
{
    public new string name;
    public string desc;
    public int price;
    public int lvl;
    
    [Space(10)]
    public Range range;
    public Target target;

    [Space(10)]
    public List<Damage> damages;
    public List<CharacBonus> characBonus;
    public List<CharacMultiplier> characMultiplier;
    public List<Alteration> alterations;

    [Space(10)]
    public List<Damage> damagesResistances;
    public List<Alteration> alterationsResistances;

    public int getDamage(DamageType wantedDamage) {
        foreach (Damage damage in damages) {
            if (damage.type == wantedDamage) {

                return damage.getDamage();
            }
        }
        return 0;
    }

    public Multiplier getDamageMultiplier(Characteristic wantedCharac) {
        foreach(CharacMultiplier multiplier in characMultiplier) {
            if(multiplier.charac == wantedCharac) {
                return multiplier.multiplier;
            }
        }
        return Multiplier.None;
    }
}

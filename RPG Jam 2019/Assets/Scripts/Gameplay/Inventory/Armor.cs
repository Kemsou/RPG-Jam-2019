using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Armor")][System.Serializable]
public class Armor : ScriptableObject
{
    public new string name { get; set; }
    public string decs { get; set; }
    public int price { get; set; }
    public int lvl { get; set; }
    
    public List<CharacBonus> characBonus;

    public List<Damage> damagesResistances;
    public List<Alteration> alterationsResistances;

    public int getArmorResistance(DamageType wantedResistance) {
        foreach (Damage resistance in damagesResistances) {
            if (resistance.type == wantedResistance) {
                return resistance.getDamage();
            }
        }
        return 0;
    }
}

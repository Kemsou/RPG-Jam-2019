using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Const {
    public const float MULTIPLIER_S = 2f;
    public const float MULTIPLIER_A = 1.4f;
    public const float MULTIPLIER_B = 1f;
    public const float MULTIPLIER_C = 0.75f;
    public const float MULTIPLIER_D = 0.5f;

}

public static class Utility {
    //way to calculate the rating from a carac. This is used to determine the amount of bonus damage a weapon has, depending on its multiplier (exactly the same way as in Dark Souls) https://darksouls.fandom.com/wiki/Parameter_Bonus
    public static float getRating(int characLevel) {
        if (characLevel <= 10) {
            return 0.05f;
        } else if (characLevel <= 30) {
            return 0.05f + ((characLevel - 10) * 0.0175f);
        } else if (characLevel <= 70) {
            return 0.4f + ((characLevel - 30) * 0.01125f);
        } else {
            return 0.85f + ((characLevel - 70) * 0.00125f);
        }
    }

    public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag) where T : Component {
        Transform t = parent.transform;
        foreach (Transform tr in t) {
            if (tr.tag == tag) {
                return tr.GetComponent<T>();
            }
        }
        return null;
    }

    public static int getBonusDamageFromMultiplier(int damage, int charac, Multiplier multiplier) {
        switch (multiplier) {
            case Multiplier.D:
                return Mathf.RoundToInt(damage * getRating(charac) * Const.MULTIPLIER_D);
            case Multiplier.C:
                return Mathf.RoundToInt(damage * getRating(charac) * Const.MULTIPLIER_C);
            case Multiplier.B:
                return Mathf.RoundToInt(damage * getRating(charac) * Const.MULTIPLIER_B);
            case Multiplier.A:
                return Mathf.RoundToInt(damage * getRating(charac) * Const.MULTIPLIER_A);
            case Multiplier.S:
                return Mathf.RoundToInt(damage * getRating(charac) * Const.MULTIPLIER_S);
            case Multiplier.None:
                return 0;
            default:
                Debug.LogWarning("WARNING: Multiplier not treated: " + multiplier);
                return 0;
        }
    }

    public static Steps getProgressionSteps() {
        return (Steps) Resources.Load("World/Steps");
    }

    //return the corresponding damages info inflicted by a character
    public static DamagesInfo getDamagesInfo(List<Damage> damages, List<CharacMultiplier> multipliers, List<Alteration> alterations, Character caster, Character target) {
        Dictionary<DamageType, int> returnDamages = new Dictionary<DamageType, int>();
        Dictionary<AlterationType, int> returnAlterations = new Dictionary<AlterationType, int>();
        foreach(Damage damage in damages) {
            int tmpDamage = damage.getDamage();
            int retDamage = tmpDamage;
            foreach (CharacMultiplier multiplier in multipliers) {
                retDamage += getBonusDamageFromMultiplier(tmpDamage, caster.getTotalCharacWithoutBuff(multiplier.charac), multiplier.multiplier);//TODO: take buffs into account
            }
            returnDamages.Add(damage.type, retDamage);
        }
        foreach(Alteration alteration in alterations) {
            returnAlterations.Add(alteration.type, alteration.power);//TODO: apply modifier on alteration's power
        }
        return new DamagesInfo(returnDamages, returnAlterations, target);
    }

    public static Character loadNewCharacter(string characterName) {
        return (Character) Resources.Load("Characters/ScriptableObjects/" + characterName);
    }

}

public enum Multiplier {None, S, A, B, C, D};

public enum Range { Unique, All }
public enum Target { Ennemy, Ally, Any }
public enum Characteristic { Strength, Dexterity, Intelligence, Accuracy, Speed, MaxHealth, MaxMana }
public enum DamageType { Physical, Fire, Water, Ice, Electricity, Heal }
public enum AlterationType { Blind, Mute, Poisoned, Frozen, Confused }

[System.Serializable]
public class CharacBonus {
    public Characteristic charac;
    public int bonus;
}

[System.Serializable]
public class CharacMultiplier {
    public Characteristic charac;
    public Multiplier multiplier;
}

[System.Serializable]
public class Damage {
    public DamageType type;
    public int amountMin;
    public int amountMax;

    //return random damage between amountMin and AmountMax
    public int getDamage() {
        return Random.Range(amountMin, amountMax);
    }

    //return fixed amount of damage
    public int getStaticDamage() {
        return (amountMin + amountMax) / 2;
    }
}

[System.Serializable]
public class Alteration {
    public AlterationType type;
    public int power;//the higher the power the greater the chance to inflict the alteration (attack) / to resist the alteration (defense)
}
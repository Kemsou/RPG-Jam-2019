using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Property", menuName = "Skill Property")]
public class SkillProperty : ScriptableObject
{
    public new string name;
    public string desc;

    [Space(10)]
    public Range range;
    public Target target;

    [Header("Physical Damage")]
    public int physicalDamage;
    public Multiplier strengthPhysicalMultiplier;
    public Multiplier dexterityPhysicalMultiplier;
    public Multiplier intellectPhysicalMultiplier;

    [Header("Fire Damage")]
    public int fireDamage;
    public Multiplier strengthFireMultiplier;
    public Multiplier dexterityFireMultiplier;
    public Multiplier intellectFireMultiplier;

    [Header("Healing")]
    public int healing;
    public Multiplier strengthHealMultiplier;
    public Multiplier dexterityHealMultiplier;
    public Multiplier intellectHealMultiplier;

    [Header("Alterations")]
    [Range(0, 100)] public int poisonChance;
    [Range(0, 100)] public int sleepChance;

}

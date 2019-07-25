using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Property", menuName = "Skill Property")]
[System.Serializable]
public class SkillProperty : ScriptableObject
{
    public new string name;
    public string desc;

    [Space(10)]
    public Range range;
    public Target target;

    public List<Damage> damages;
    public List<CharacMultiplier> characMultiplier;
    public List<Alteration> alterations;

}

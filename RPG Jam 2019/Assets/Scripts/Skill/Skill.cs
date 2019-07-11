using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public class Skill : ScriptableObject{
    public int manaCost;
    public int healthCost;

    [Space(10)]
    public List<SkillProperty> properties;
}

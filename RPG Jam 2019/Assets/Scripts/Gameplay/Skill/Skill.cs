using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
[System.Serializable]
public class Skill : ScriptableObject{
    public int manaCost;
    public int healthCost;

    [Space(10)]
    public List<SkillProperty> properties;
}

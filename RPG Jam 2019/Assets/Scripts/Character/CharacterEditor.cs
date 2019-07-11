using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Character))]
public class CharacterEditor : Editor
{
    int selectedWeapon = 0;
    int selectedArmor = 0;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        Character myTarget = (Character)target;
        
        //get all the files in the weapons folder
        Object[] weaponsResource = Resources.LoadAll("Weapons", typeof(Weapon));

        //store their name
        List<string> weaponsName = new List<string>();
        foreach(Object o in weaponsResource) {
            weaponsName.Add(o.name);
        }

        //display all the weapons and let the player select one
        selectedWeapon = EditorGUILayout.Popup("Weapon", selectedWeapon, weaponsName.ToArray());

        //set the character weapon depending on the selectedIndex
        myTarget.weapon = (Weapon)weaponsResource[selectedWeapon];

        //do the same with armor
        Object[] armorResource = Resources.LoadAll("Armors", typeof(Armor));
        List<string> armorName = new List<string>();
        foreach (Object o in armorResource) {
            armorName.Add(o.name);
        }
        selectedArmor = EditorGUILayout.Popup("Armor", selectedArmor, weaponsName.ToArray());
        myTarget.armor = (Armor)armorResource[selectedArmor];
    }
}

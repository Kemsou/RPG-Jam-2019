using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*[CustomEditor(typeof(Character))]
public class CharacterEditor : Editor
{
    int selectedWeapon;
    int selectedArmor;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        Character myTarget = (Character)target;
        
        //get all the files in the weapons folder
        Object[] weaponsResource = Resources.LoadAll("Weapons", typeof(Weapon));

        int i = 0;
        //store their name
        List<string> weaponsName = new List<string>();
        foreach(Object o in weaponsResource) {
            weaponsName.Add(o.name);
            if(o.name == myTarget.weapon.name) {
                selectedWeapon = i;
            }
            i++;
        }

        //display all the weapons and let the player select one
        selectedWeapon = EditorGUILayout.Popup("Weapon", selectedWeapon, weaponsName.ToArray());

        //set the character weapon depending on the selectedIndex
        myTarget.weapon = (Weapon)weaponsResource[selectedWeapon];

        //do the same with armor
        Object[] armorResource = Resources.LoadAll("Armors", typeof(Armor));
        List<string> armorName = new List<string>();
        i = 0;
        foreach (Object o in armorResource) {
            armorName.Add(o.name);
            if (o.name == myTarget.armor.name) {
                selectedArmor = i;
            }
            i++;
        }
        selectedArmor = EditorGUILayout.Popup("Armor", selectedArmor, armorName.ToArray());
        myTarget.armor = (Armor)armorResource[selectedArmor];
    }
}
*/
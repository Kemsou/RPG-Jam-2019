using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Character))]
public class CharacterEditor : Editor
{
    int selected = 0;

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
        selected = EditorGUILayout.Popup("Weapon", selected, weaponsName.ToArray());

        //set the character weapon depending on the selectedIndex
        myTarget.weapon = (Weapon)weaponsResource[selected];
    }
}

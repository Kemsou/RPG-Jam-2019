
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using System.Reflection;
 using System;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;
	
	[Savable] public bool isTrigerred = false;
	[Savable] public Character test ;

	 private void Start() {
		test.armor = Resources.Load<Armor>("Armors/Basic Armor"); 
	}
	public void TriggerDialogue ()
	{ 
		this.isTrigerred = true;
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

}



 [System.AttributeUsage(AttributeTargets.Field)]
 public class Savable : System.Attribute {}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using System.Reflection;
 using System;

public class DialogueComponent : MonoBehaviour {

	public Dialogue dialogue;


    [Header("Optional")]
    public string reachedStep;

    private void Start() {
        GetComponent<InteractionComponent>().setCallback(TriggerDialogue);
    }

    public void TriggerDialogue ()
	{
        if (DialogueManager.i.canInteract) {
            DialogueManager.i.StartDialogue(dialogue);
        }
	}

   

}



 [System.AttributeUsage(AttributeTargets.Field)]
 public class Savable : System.Attribute {}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//This component is used for GameObjects that disappears when a certain condition is met
public class Obstacle : StepObject
{
    public override void stepIsReached(EventArgs evt) {
        base.stepIsReached(evt);

        Debug.Log("From "+ gameObject.name+" : "+ listenedStep + " reached !");

        Destroy(this);
    }
}
